using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Crawling.Models;

namespace KHHub.CrawlerSerivce.Crawling.Abstractions;

/// <summary>
/// One implementation per external news site (listing + article detail pages).
/// </summary>
public interface IArticleNewsSiteHandler
{
    string SiteKey { get; }

    bool SupportsListingPage(Uri uri);

    bool SupportsArticleDetailPage(Uri uri);

    Uri BuildListingPageUri(Uri seedListingUri, int pageNumber);

    Task<IReadOnlyList<CrawledArticleListItem>> ParseListingHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default);

    Task<CrawledArticleDetailPatch> ParseArticleDetailHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default);
}
