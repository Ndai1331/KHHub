using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling.Http;

public class CrawlerHtmlFetcher : ICrawlerHtmlFetcher, ITransientDependency
{
    public const string HttpClientName = "CrawlerSerivce.Html";

    private readonly IHttpClientFactory _httpClientFactory;

    public CrawlerHtmlFetcher(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<string> GetStringAsync(Uri uri, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient(HttpClientName);
        return await client.GetStringAsync(uri, cancellationToken);
    }
}
