using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Configuration;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Http;
using KHHub.CrawlerSerivce.Crawling.Mapping;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.IntegrationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

[AllowAnonymous]
public class ArticleNewsImportAppService : CrawlerSerivceAppService, IArticleNewsImportAppService
{
    private readonly IArticleNewsSiteHandlerResolver _resolver;
    private readonly ICrawlerHtmlFetcher _htmlFetcher;
    private readonly IArticleCrawlerIntegrationService _articleCrawler;
    private readonly CrawlerArticleImportDefaultsOptions _articleDefaults;

    public ArticleNewsImportAppService(
        IArticleNewsSiteHandlerResolver resolver,
        ICrawlerHtmlFetcher htmlFetcher,
        IArticleCrawlerIntegrationService articleCrawler,
        IOptions<CrawlerArticleImportDefaultsOptions> articleDefaults)
    {
        _resolver = resolver;
        _htmlFetcher = htmlFetcher;
        _articleCrawler = articleCrawler;
        _articleDefaults = articleDefaults.Value;
    }

    public async Task<ImportArticleListingResultDto> ImportFromListingAsync(ImportArticleListingInput input)
    {
        var categoryId = ResolveGuidOrDefault(input.ArticleCategoryId, _articleDefaults.ArticleCategoryId);
        if (categoryId == Guid.Empty)
        {
            throw new Volo.Abp.UserFriendlyException(
                "ArticleCategoryId is required: set Crawler:ArticleImportDefaults in appsettings or pass it in the request body.");
        }

        var status = ParseArticleStatus(_articleDefaults.DefaultStatus);

        Uri seed;
        try
        {
            seed = new Uri(input.ListingUrl, UriKind.Absolute);
        }
        catch (UriFormatException ex)
        {
            throw new Volo.Abp.UserFriendlyException($"ListingUrl is invalid: {ex.Message}");
        }

        var listingHandler = _resolver.ResolveForListing(seed);
        var result = new ImportArticleListingResultDto { SiteKey = listingHandler.SiteKey };

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var rowsQueued = new List<CrawledArticleListItem>();

        for (var page = 1; page <= input.MaxPages; page++)
        {
            if (input.MaxArticlesToImport.HasValue && rowsQueued.Count >= input.MaxArticlesToImport.Value)
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

            var rows = await listingHandler.ParseListingHtmlAsync(fetchResult.Html!, pageUri);
            foreach (var row in rows)
            {
                if (!seen.Add(row.SourceAbsoluteUrl))
                {
                    continue;
                }

                rowsQueued.Add(row);
                result.ListingRowsSeen++;

                if (input.MaxArticlesToImport.HasValue && rowsQueued.Count >= input.MaxArticlesToImport.Value)
                {
                    break;
                }
            }
        }

        foreach (var row in rowsQueued)
        {
            var detailUri = new Uri(row.SourceAbsoluteUrl);
            CrawledArticleDetailPatch detail;
            try
            {
                var detailHandler = _resolver.ResolveForArticleDetail(detailUri);
                var detailHtml = await _htmlFetcher.GetStringAsync(detailUri);
                detail = await detailHandler.ParseArticleDetailHtmlAsync(detailHtml, detailUri);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"{row.SourceAbsoluteUrl}: detail fetch/parse failed — {ex.Message}");
                continue;
            }

            var upsert = ArticleCrawlerMergeMapper.ToUpsertDto(
                row,
                detail,
                categoryId,
                string.IsNullOrWhiteSpace(_articleDefaults.DefaultAuthorName) ? "KH Hub" : _articleDefaults.DefaultAuthorName,
                status,
                input.ExtraTagNames);

            try
            {
                var upsertResult = await _articleCrawler.UpsertFromCrawlerAsync(upsert);
                result.ArticlesProcessed++;
                if (upsertResult.WasCreated)
                {
                    result.ArticlesCreated++;
                }
                else
                {
                    result.ArticlesUpdated++;
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

    private static ArticleStatus ParseArticleStatus(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return ArticleStatus.Pending;
        }

        return Enum.TryParse<ArticleStatus>(s, true, out var st) ? st : ArticleStatus.Pending;
    }
}
