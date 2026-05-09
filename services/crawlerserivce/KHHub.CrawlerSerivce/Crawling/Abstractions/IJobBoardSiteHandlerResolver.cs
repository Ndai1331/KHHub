using System;

namespace KHHub.CrawlerSerivce.Crawling.Abstractions;

public interface IJobBoardSiteHandlerResolver
{
    IJobBoardSiteHandler ResolveForListing(Uri listingSeedUri);

    IJobBoardSiteHandler ResolveForJobDetail(Uri jobDetailUri);
}
