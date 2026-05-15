using System;
using System.Collections.Generic;
using System.Linq;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling;

public class ArticleNewsSiteHandlerResolver : IArticleNewsSiteHandlerResolver, ITransientDependency
{
    private readonly IReadOnlyList<IArticleNewsSiteHandler> _handlers;

    public ArticleNewsSiteHandlerResolver(IEnumerable<IArticleNewsSiteHandler> handlers)
    {
        _handlers = handlers.ToList();
    }

    public IArticleNewsSiteHandler ResolveForListing(Uri listingSeedUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsListingPage(listingSeedUri));
        if (handler == null)
        {
            throw CreateUnknownListingException(listingSeedUri);
        }

        return handler;
    }

    public IArticleNewsSiteHandler ResolveForArticleDetail(Uri articleDetailUri)
    {
        var handler = _handlers.FirstOrDefault(h => h.SupportsArticleDetailPage(articleDetailUri));
        if (handler == null)
        {
            throw CreateUnknownArticleDetailException(articleDetailUri);
        }

        return handler;
    }

    private BusinessException CreateUnknownListingException(Uri listingSeedUri)
    {
        var siteKeys = string.Join(", ", _handlers.Select(h => h.SiteKey));
        var host = CrawlerUriHostNormalizer.NormalizeHost(listingSeedUri.Host);
        return new BusinessException("CrawlerSerivce:UnknownArticleListingSite")
            .WithData("Uri", listingSeedUri.ToString())
            .WithData("Host", host)
            .WithData("RegisteredHandlerCount", _handlers.Count)
            .WithData("RegisteredSiteKeys", siteKeys.Length > 0 ? siteKeys : "(none)");
    }

    private BusinessException CreateUnknownArticleDetailException(Uri articleDetailUri)
    {
        var siteKeys = string.Join(", ", _handlers.Select(h => h.SiteKey));
        var host = CrawlerUriHostNormalizer.NormalizeHost(articleDetailUri.Host);
        return new BusinessException("CrawlerSerivce:UnknownArticleDetailSite")
            .WithData("Uri", articleDetailUri.ToString())
            .WithData("Host", host)
            .WithData("RegisteredHandlerCount", _handlers.Count)
            .WithData("RegisteredSiteKeys", siteKeys.Length > 0 ? siteKeys : "(none)");
    }
}
