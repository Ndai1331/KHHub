using System;
using KHHub.CrawlerSerivce.Crawling.Abstractions;

namespace KHHub.CrawlerSerivce.Crawling.Abstractions;

public interface IPlaceDirectorySiteHandlerResolver
{
    IPlaceDirectorySiteHandler ResolveForListing(Uri listingSeedUri);

    IPlaceDirectorySiteHandler ResolveForPlaceDetail(Uri placeDetailUri);
}
