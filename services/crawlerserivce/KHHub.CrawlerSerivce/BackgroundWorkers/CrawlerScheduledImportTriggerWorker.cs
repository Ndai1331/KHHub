using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Cronos;
using KHHub.CrawlerSerivce.Configuration;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DistributedLocking;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace KHHub.CrawlerSerivce.BackgroundWorkers;

/// <summary>
/// Wakes every minute, evaluates cron expressions in <see cref="CrawlerScheduledImportsOptions"/>,
/// and enqueues RabbitMQ async background jobs when a slot matches (with distributed lock per slot).
/// </summary>
public class CrawlerScheduledImportTriggerWorker : AsyncPeriodicBackgroundWorkerBase, ITransientDependency
{
    private static readonly ConcurrentDictionary<string, CronExpression?> CronCache = new();

    private readonly IBackgroundJobManager _backgroundJobManager;
    private readonly IAbpDistributedLock _distributedLock;
    private readonly IOptions<CrawlerScheduledImportsOptions> _options;
    private readonly IClock _clock;

    public CrawlerScheduledImportTriggerWorker(
        AbpAsyncTimer timer,
        IServiceScopeFactory serviceScopeFactory,
        IBackgroundJobManager backgroundJobManager,
        IAbpDistributedLock distributedLock,
        IOptions<CrawlerScheduledImportsOptions> options,
        IClock clock
    ) : base(timer, serviceScopeFactory)
    {
        _backgroundJobManager = backgroundJobManager;
        _distributedLock = distributedLock;
        _options = options;
        _clock = clock;

        Timer.Period = 60_000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        var opts = _options.Value;
        if (!opts.Enabled)
        {
            return;
        }

        TimeZoneInfo tz;
        try
        {
            tz = TimeZoneInfo.FindSystemTimeZoneById(opts.TimeZoneId);
        }
        catch (TimeZoneNotFoundException ex)
        {
            Logger.LogWarning(ex, "Crawler:ScheduledImports TimeZoneId '{TimeZoneId}' is invalid.", opts.TimeZoneId);
            return;
        }

        var utcNow = _clock.Now.ToUniversalTime();

        await TryScheduleJobBoardAsync(opts, tz, utcNow, workerContext);
        await TryScheduleArticlesAsync(opts, tz, utcNow, workerContext);
        await TrySchedulePlacesAsync(opts, tz, utcNow, workerContext);
    }

    private async Task TryScheduleJobBoardAsync(
        CrawlerScheduledImportsOptions opts,
        TimeZoneInfo tz,
        DateTime utcNow,
        PeriodicBackgroundWorkerContext workerContext)
    {
        var section = opts.Jobs;
        if (!section.Enabled)
        {
            return;
        }

        var cron = GetOrParseCron(section.Cron);
        if (cron == null || !HasOccurrenceInCurrentMinute(cron, tz, utcNow))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(section.Import.ListingUrl))
        {
            return;
        }

        var slot = FormatSlotKey(tz, utcNow);
        await using var handle = await _distributedLock.TryAcquireAsync(
            $"Crawler:Schedule:Jobs:{slot}",
            TimeSpan.FromMinutes(3));

        if (handle == null)
        {
            return;
        }

        var args = CloneJobImport(section.Import);
        await _backgroundJobManager.EnqueueAsync(args);
        Logger.LogInformation(
            "Enqueued scheduled job-board import for slot {Slot} ({ListingUrl}).",
            slot,
            args.ListingUrl);
    }

    private async Task TryScheduleArticlesAsync(
        CrawlerScheduledImportsOptions opts,
        TimeZoneInfo tz,
        DateTime utcNow,
        PeriodicBackgroundWorkerContext workerContext)
    {
        var section = opts.Articles;
        if (!section.Enabled)
        {
            return;
        }

        var cron = GetOrParseCron(section.Cron);
        if (cron == null || !HasOccurrenceInCurrentMinute(cron, tz, utcNow))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(section.Import.ListingUrl))
        {
            return;
        }

        var slot = FormatSlotKey(tz, utcNow);
        await using var handle = await _distributedLock.TryAcquireAsync(
            $"Crawler:Schedule:Articles:{slot}",
            TimeSpan.FromMinutes(3));

        if (handle == null)
        {
            return;
        }

        var args = CloneArticleImport(section.Import);
        await _backgroundJobManager.EnqueueAsync(args);
        Logger.LogInformation(
            "Enqueued scheduled article import for slot {Slot} ({ListingUrl}).",
            slot,
            args.ListingUrl);
    }

    private async Task TrySchedulePlacesAsync(
        CrawlerScheduledImportsOptions opts,
        TimeZoneInfo tz,
        DateTime utcNow,
        PeriodicBackgroundWorkerContext workerContext)
    {
        var section = opts.Places;
        if (!section.Enabled)
        {
            return;
        }

        var cron = GetOrParseCron(section.Cron);
        if (cron == null || !HasOccurrenceInCurrentMinute(cron, tz, utcNow))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(section.Import.ListingUrl))
        {
            return;
        }

        var slot = FormatSlotKey(tz, utcNow);
        await using var handle = await _distributedLock.TryAcquireAsync(
            $"Crawler:Schedule:Places:{slot}",
            TimeSpan.FromMinutes(3));

        if (handle == null)
        {
            return;
        }

        var args = ClonePlaceImport(section.Import);
        await _backgroundJobManager.EnqueueAsync(args);
        Logger.LogInformation(
            "Enqueued scheduled place import for slot {Slot} ({ListingUrl}).",
            slot,
            args.ListingUrl);
    }

    private CronExpression? GetOrParseCron(string cronText)
    {
        if (string.IsNullOrWhiteSpace(cronText))
        {
            Logger.LogWarning("Scheduled import cron expression is empty.");
            return null;
        }

        return CronCache.GetOrAdd(
            cronText,
            key =>
            {
                if (!global::Cronos.CronExpression.TryParse(key, out var parsed))
                {
                    Logger.LogWarning("Invalid cron expression: {Cron}", key);
                    return null;
                }

                return parsed;
            });
    }

    private static bool HasOccurrenceInCurrentMinute(CronExpression expression, TimeZoneInfo zone, DateTime utcNow)
    {
        var local = TimeZoneInfo.ConvertTimeFromUtc(utcNow, zone);
        var minuteStartLocal = new DateTime(local.Year, local.Month, local.Day, local.Hour, local.Minute, 0, DateTimeKind.Unspecified);
        var fromUtc = TimeZoneInfo.ConvertTimeToUtc(minuteStartLocal, zone);
        var toUtc = fromUtc.AddMinutes(1);

        return expression.GetOccurrences(fromUtc, toUtc, zone).Any();
    }

    private static string FormatSlotKey(TimeZoneInfo tz, DateTime utcNow)
    {
        var local = TimeZoneInfo.ConvertTimeFromUtc(utcNow, tz);
        var slotStart = new DateTime(local.Year, local.Month, local.Day, local.Hour, local.Minute, 0, DateTimeKind.Unspecified);
        return slotStart.ToString("yyyy-MM-ddTHH:mm", CultureInfo.InvariantCulture);
    }

    private static ImportJobListingInput CloneJobImport(ImportJobListingInput src)
    {
        return new ImportJobListingInput
        {
            ListingUrl = src.ListingUrl,
            MaxPages = src.MaxPages,
            MaxJobsToImport = src.MaxJobsToImport,
            DelayBetweenDetailRequestsMs = src.DelayBetweenDetailRequestsMs,
            ProvinceId = src.ProvinceId,
            WardId = src.WardId,
            JobCategoryId = src.JobCategoryId,
            ExtraTagNames = src.ExtraTagNames != null ? new List<string>(src.ExtraTagNames) : new List<string>()
        };
    }

    private static ImportArticleListingInput CloneArticleImport(ImportArticleListingInput src)
    {
        return new ImportArticleListingInput
        {
            ListingUrl = src.ListingUrl,
            MaxPages = src.MaxPages,
            MaxArticlesToImport = src.MaxArticlesToImport,
            DelayBetweenDetailRequestsMs = src.DelayBetweenDetailRequestsMs,
            ArticleCategoryId = src.ArticleCategoryId,
            ExtraTagNames = src.ExtraTagNames != null ? new List<string>(src.ExtraTagNames) : new List<string>()
        };
    }

    private static ImportPlaceListingInput ClonePlaceImport(ImportPlaceListingInput src)
    {
        return new ImportPlaceListingInput
        {
            ListingUrl = src.ListingUrl,
            MaxPages = src.MaxPages,
            MaxPlacesToImport = src.MaxPlacesToImport,
            DelayBetweenDetailRequestsMs = src.DelayBetweenDetailRequestsMs,
            ProvinceId = src.ProvinceId,
            WardId = src.WardId,
            PlaceCategoryId = src.PlaceCategoryId,
            ExtraTagNames = src.ExtraTagNames != null ? new List<string>(src.ExtraTagNames) : new List<string>()
        };
    }
}
