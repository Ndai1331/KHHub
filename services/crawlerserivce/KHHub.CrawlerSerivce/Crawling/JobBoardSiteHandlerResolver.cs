using System;
using System.Collections.Generic;
using System.Linq;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling;

public class JobBoardSiteHandlerResolver : IJobBoardSiteHandlerResolver, ITransientDependency
{
    private readonly IEnumerable<IJobBoardSiteHandler> _handlers;

    public JobBoardSiteHandlerResolver(IEnumerable<IJobBoardSiteHandler> handlers)
    {
        _handlers = handlers;
    }

    public IJobBoardSiteHandler ResolveForListing(Uri listingSeedUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsListingPage(listingSeedUri));
        if (handler == null)
        {
            throw new BusinessException("CrawlerSerivce:UnknownListingSite")
                .WithData("Uri", listingSeedUri.ToString());
        }

        return handler;
    }

    public IJobBoardSiteHandler ResolveForJobDetail(Uri jobDetailUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsJobDetailPage(jobDetailUri));
        if (handler == null)
        {
            throw new BusinessException("CrawlerSerivce:UnknownJobDetailSite")
                .WithData("Uri", jobDetailUri.ToString());
        }

        return handler;
    }
}
