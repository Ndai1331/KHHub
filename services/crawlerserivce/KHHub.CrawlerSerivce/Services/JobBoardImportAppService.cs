using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Configuration;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Http;
using KHHub.CrawlerSerivce.Crawling.Mapping;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using KHHub.MasterDataService.IntegrationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

[AllowAnonymous]
public class JobBoardImportAppService : CrawlerSerivceAppService, IJobBoardImportAppService
{
    private readonly IJobBoardSiteHandlerResolver _boardResolver;
    private readonly ICrawlerHtmlFetcher _htmlFetcher;
    private readonly IJobCrawlerIntegrationService _jobCrawlerIntegration;
    private readonly CrawlerImportDefaultsOptions _importDefaults;

    public JobBoardImportAppService(
        IJobBoardSiteHandlerResolver boardResolver,
        ICrawlerHtmlFetcher htmlFetcher,
        IJobCrawlerIntegrationService jobCrawlerIntegration,
        IOptions<CrawlerImportDefaultsOptions> importDefaults)
    {
        _boardResolver = boardResolver;
        _htmlFetcher = htmlFetcher;
        _jobCrawlerIntegration = jobCrawlerIntegration;
        _importDefaults = importDefaults.Value;
    }

    public async Task<ImportJobListingResultDto> ImportFromListingAsync(ImportJobListingInput input)
    {
        var provinceId = ResolveGuidOrDefault(input.ProvinceId, _importDefaults.ProvinceId);
        var wardId = ResolveGuidOrDefault(input.WardId, _importDefaults.WardId);
        var jobCategoryId = ResolveGuidOrDefault(input.JobCategoryId, _importDefaults.JobCategoryId);

        if (provinceId == Guid.Empty || wardId == Guid.Empty || jobCategoryId == Guid.Empty)
        {
            throw new Volo.Abp.UserFriendlyException(
                "ProvinceId, WardId and JobCategoryId are required: set Crawler:ImportDefaults in appsettings or pass them in the request body.");
        }

        Uri seed;
        try
        {
            seed = new Uri(input.ListingUrl, UriKind.Absolute);
        }
        catch (UriFormatException ex)
        {
            throw new Volo.Abp.UserFriendlyException($"ListingUrl is invalid: {ex.Message}");
        }

        var listingHandler = _boardResolver.ResolveForListing(seed);
        var result = new ImportJobListingResultDto { SiteKey = listingHandler.SiteKey };

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var jobsQueued = new List<CrawledJobListItem>();

        for (var page = 1; page <= input.MaxPages; page++)
        {
            if (input.MaxJobsToImport.HasValue && jobsQueued.Count >= input.MaxJobsToImport.Value)
            {
                break;
            }

            var pageUri = listingHandler.BuildListingPageUri(seed, page);
            var fetchResult = await CrawlerHtmlFetchRetry.GetStringWithRetriesAsync(_htmlFetcher, pageUri);
            if (!fetchResult.Success)
            {
                result.Warnings.Add(
                    $"Listing page {page} ({pageUri}): failed after {fetchResult.AttemptsMade} attempt(s) — {fetchResult.ErrorMessage}");
                break;
            }

            var html = fetchResult.Html!;

            var rows = await listingHandler.ParseListingHtmlAsync(html, pageUri);
            foreach (var row in rows)
            {
                if (!seen.Add(row.SourceAbsoluteUrl))
                {
                    continue;
                }

                jobsQueued.Add(row);
                result.ListingRowsSeen++;

                if (input.MaxJobsToImport.HasValue && jobsQueued.Count >= input.MaxJobsToImport.Value)
                {
                    break;
                }
            }
        }

        foreach (var row in jobsQueued)
        {
            var detailUri = new Uri(row.SourceAbsoluteUrl);
            CrawledJobDetailPatch detail;
            try
            {
                var detailHandler = _boardResolver.ResolveForJobDetail(detailUri);
                var detailHtml = await _htmlFetcher.GetStringAsync(detailUri);
                detail = await detailHandler.ParseJobDetailHtmlAsync(detailHtml, detailUri);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"{row.SourceAbsoluteUrl}: detail fetch/parse failed — {ex.Message}");
                continue;
            }

            var upsert = JobCrawlerMergeMapper.ToUpsertDto(
                row,
                detail,
                provinceId,
                wardId,
                jobCategoryId,
                input.ExtraTagNames);

            try
            {
                var upsertResult = await _jobCrawlerIntegration.UpsertFromCrawlerAsync(upsert);
                result.JobsProcessed++;
                if (upsertResult.WasCreated)
                {
                    result.JobsCreated++;
                }
                else
                {
                    result.JobsUpdated++;
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add($"{row.SourceAbsoluteUrl}: MasterData upsert failed — {ex.Message}");
            }

            if (input.DelayBetweenDetailRequestsMs > 0)
            {
                await Task.Delay(input.DelayBetweenDetailRequestsMs);
            }
        }

        return result;
    }

    private static Guid ResolveGuidOrDefault(Guid? requestValue, Guid configuredDefault)
    {
        if (requestValue is { } v && v != Guid.Empty)
        {
            return v;
        }

        return configuredDefault;
    }
}
