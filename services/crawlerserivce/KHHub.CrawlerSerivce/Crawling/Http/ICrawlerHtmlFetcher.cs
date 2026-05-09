using System;
using System.Threading;
using System.Threading.Tasks;
namespace KHHub.CrawlerSerivce.Crawling.Http;

public interface ICrawlerHtmlFetcher
{
    Task<string> GetStringAsync(Uri uri, CancellationToken cancellationToken = default);
}
