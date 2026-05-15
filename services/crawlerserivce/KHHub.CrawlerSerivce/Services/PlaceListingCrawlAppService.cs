using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Http;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

[AllowAnonymous]
public class PlaceListingCrawlAppService : CrawlerSerivceAppService, IPlaceListingCrawlAppService
{
    private readonly IPlaceDirectorySiteHandlerResolver _resolver;
    private readonly ICrawlerHtmlFetcher _htmlFetcher;

    public PlaceListingCrawlAppService(
        IPlaceDirectorySiteHandlerResolver resolver,
        ICrawlerHtmlFetcher htmlFetcher)
    {
        _resolver = resolver;
        _htmlFetcher = htmlFetcher;
    }

    public async Task<PreviewPlaceListingResultDto> PreviewListingAsync(PreviewPlaceListingInput input)
    {
        Uri seed;
        try
        {
            seed = new Uri(input.ListingUrl, UriKind.Absolute);
        }
        catch (UriFormatException ex)
        {
            throw new Volo.Abp.UserFriendlyException($"ListingUrl is not a valid absolute URL: {ex.Message}");
        }

        var handler = _resolver.ResolveForListing(seed);
        var result = new PreviewPlaceListingResultDto { SiteKey = handler.SiteKey };
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

            var rows = await handler.ParseListingHtmlAsync(fetchResult.Html!, pageUri);
            foreach (var row in rows)
            {
                if (!seen.Add(row.SourceAbsoluteUrl))
                {
                    continue;
                }

                result.Items.Add(
                    new CrawledPlaceListItemDto
                    {
                        SiteKey = handler.SiteKey,
                        SourceAbsoluteUrl = row.SourceAbsoluteUrl,
                        Name = row.Name,
                        ShortDescription = row.ShortDescription
                    });
            }
        }

        return result;
    }
}
