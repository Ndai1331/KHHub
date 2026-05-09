using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Crawling.Models;

namespace KHHub.CrawlerSerivce.Crawling.Abstractions;

/// <summary>
/// One implementation per external job board (listing + job detail pages).
/// </summary>
public interface IJobBoardSiteHandler
{
    string SiteKey { get; }

    bool SupportsListingPage(Uri uri);

    bool SupportsJobDetailPage(Uri uri);

    Uri BuildListingPageUri(Uri seedListingUri, int pageNumber);

    Task<IReadOnlyList<CrawledJobListItem>> ParseListingHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default);

    Task<CrawledJobDetailPatch> ParseJobDetailHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default);
}
