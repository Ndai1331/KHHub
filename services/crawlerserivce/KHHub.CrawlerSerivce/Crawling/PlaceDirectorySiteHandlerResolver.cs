using System;
using System.Collections.Generic;
using System.Linq;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling;

public class PlaceDirectorySiteHandlerResolver : IPlaceDirectorySiteHandlerResolver, ITransientDependency
{
    private readonly IEnumerable<IPlaceDirectorySiteHandler> _handlers;

    public PlaceDirectorySiteHandlerResolver(IEnumerable<IPlaceDirectorySiteHandler> handlers)
    {
        _handlers = handlers;
    }

    public IPlaceDirectorySiteHandler ResolveForListing(Uri listingSeedUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsListingPage(listingSeedUri));
        if (handler == null)
        {
            throw new BusinessException("CrawlerSerivce:UnknownPlaceListingSite")
                .WithData("Uri", listingSeedUri.ToString());
        }

        return handler;
    }

    public IPlaceDirectorySiteHandler ResolveForPlaceDetail(Uri placeDetailUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsPlaceDetailPage(placeDetailUri));
        if (handler == null)
        {
            throw new BusinessException("CrawlerSerivce:UnknownPlaceDetailSite")
                .WithData("Uri", placeDetailUri.ToString());
        }

        return handler;
    }
}
