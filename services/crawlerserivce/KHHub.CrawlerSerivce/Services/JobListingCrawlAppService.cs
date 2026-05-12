using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Http;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

/// <summary>
/// Preview listing crawl only (no MasterData write). Use <see cref="IJobBoardImportAppService"/> for full import.
/// </summary>
[AllowAnonymous]
public class JobListingCrawlAppService : CrawlerSerivceAppService, IJobListingCrawlAppService
{
    private readonly IJobBoardSiteHandlerResolver _handlerResolver;
    private readonly ICrawlerHtmlFetcher _htmlFetcher;

    public JobListingCrawlAppService(
        IJobBoardSiteHandlerResolver handlerResolver,
        ICrawlerHtmlFetcher htmlFetcher)
    {
        _handlerResolver = handlerResolver;
        _htmlFetcher = htmlFetcher;
    }

    public async Task<PreviewJobListingResultDto> PreviewListingAsync(PreviewJobListingInput input)
    {
        Uri seed;
        try
        {
            seed = new Uri(input.ListingUrl, UriKind.Absolute);
        }
        catch (UriFormatException ex)
        {
            throw new Volo.Abp.UserFriendlyException(
                $"ListingUrl is not a valid absolute URL: {ex.Message}");
        }

        var handler = _handlerResolver.ResolveForListing(seed);
        var result = new PreviewJobListingResultDto { SiteKey = handler.SiteKey };
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        for (var page = 1; page <= input.MaxPages; page++)
        {
            var pageUri = handler.BuildListingPageUri(seed, page);
            var fetchResult = await CrawlerHtmlFetchRetry.GetStringWithRetriesAsync(_htmlFetcher, pageUri);
            if (!fetchResult.Success)
            {
                result.Warnings.Add(
                    $"Page {page} ({pageUri}): failed after {fetchResult.AttemptsMade} attempt(s) — {fetchResult.ErrorMessage}");
                break;
            }

            var html = fetchResult.Html!;

            var rows = await handler.ParseListingHtmlAsync(html, pageUri);
            foreach (var row in rows)
            {
                if (!seen.Add(row.SourceAbsoluteUrl))
                {
                    continue;
                }

                result.Items.Add(Map(handler.SiteKey, row));
            }
        }

        return result;
    }

    private static CrawledJobListItemDto Map(string siteKey, CrawledJobListItem row)
    {
        return new CrawledJobListItemDto
        {
            SiteKey = siteKey,
            SourceAbsoluteUrl = row.SourceAbsoluteUrl,
            ExternalJobId = row.ExternalJobId,
            Title = row.Title,
            CompanyName = row.CompanyName,
            EmploymentTypeLabel = row.EmploymentTypeLabel,
            SalaryDisplayText = row.SalaryDisplayText,
            PublishedAt = row.PublishedAt,
            ApplicationDeadline = row.ApplicationDeadline
        };
    }
}
