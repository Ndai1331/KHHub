using System;
using System.Collections.Generic;
using System.Linq;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling;

public class ArticleNewsSiteHandlerResolver : IArticleNewsSiteHandlerResolver, ITransientDependency
{
    private readonly IEnumerable<IArticleNewsSiteHandler> _handlers;

    public ArticleNewsSiteHandlerResolver(IEnumerable<IArticleNewsSiteHandler> handlers)
    {
        _handlers = handlers;
    }

    public IArticleNewsSiteHandler ResolveForListing(Uri listingSeedUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsListingPage(listingSeedUri));
        if (handler == null)
        {
            throw new BusinessException("CrawlerSerivce:UnknownArticleListingSite")
                .WithData("Uri", listingSeedUri.ToString());
        }

        return handler;
    }

    public IArticleNewsSiteHandler ResolveForArticleDetail(Uri articleDetailUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsArticleDetailPage(articleDetailUri));
        if (handler == null)
        {
            throw new BusinessException("CrawlerSerivce:UnknownArticleDetailSite")
                .WithData("Uri", articleDetailUri.ToString());
        }

        return handler;
    }
}
