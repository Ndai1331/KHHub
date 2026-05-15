using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Crawling.Models;

namespace KHHub.CrawlerSerivce.Crawling.Abstractions;

/// <summary>
/// One implementation per external place directory (listing + detail pages).
/// </summary>
public interface IPlaceDirectorySiteHandler
{
    string SiteKey { get; }

    bool SupportsListingPage(Uri uri);

    bool SupportsPlaceDetailPage(Uri uri);

    Uri BuildListingPageUri(Uri seedListingUri, int pageNumber);

    Task<IReadOnlyList<CrawledPlaceListItem>> ParseListingHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default);

    Task<CrawledPlaceDetailPatch> ParsePlaceDetailHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default);
}
