using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoKhanhHoa;
using KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoNinhThuan;
using Microsoft.Extensions.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling;

public static class CrawlerSiteHandlersServiceCollectionExtensions
{
    public static IServiceCollection AddCrawlerArticleNewsSiteHandlers(this IServiceCollection services)
    {
        services.AddTransient<IArticleNewsSiteHandler, BaoKhanhHoaArticleSiteHandler>();
        services.AddTransient<IArticleNewsSiteHandler, BaoNinhThuanArticleSiteHandler>();
        return services;
    }
}
