using System;
namespace KHHub.CrawlerSerivce.Crawling.Abstractions;

public interface IArticleNewsSiteHandlerResolver
{
    IArticleNewsSiteHandler ResolveForListing(Uri listingSeedUri);

    IArticleNewsSiteHandler ResolveForArticleDetail(Uri articleDetailUri);
}
