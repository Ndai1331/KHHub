using System;
using System.Threading;
using System.Threading.Tasks;

namespace KHHub.CrawlerSerivce.Crawling.Http;

/// <summary>
/// Retries HTML GET for listing pages (transient network / remote timeouts).
/// </summary>
public static class CrawlerHtmlFetchRetry
{
    public const int DefaultListingMaxAttempts = 3;

    /// <summary>
    /// Tries up to <paramref name="maxAttempts"/> times; returns failure after last attempt.
    /// </summary>
    public static async Task<CrawlerHtmlFetchRetryResult> GetStringWithRetriesAsync(
        ICrawlerHtmlFetcher fetcher,
        Uri uri,
        int maxAttempts = DefaultListingMaxAttempts,
        CancellationToken cancellationToken = default)
    {
        if (maxAttempts < 1)
        {
            maxAttempts = 1;
        }

        Exception? last = null;
        for (var attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                var html = await fetcher.GetStringAsync(uri, cancellationToken);
                return new CrawlerHtmlFetchRetryResult(true, html, null, attempt);
            }
            catch (Exception ex)
            {
                last = ex;
                if (attempt < maxAttempts)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(400 * attempt), cancellationToken);
                }
            }
        }

        return new CrawlerHtmlFetchRetryResult(
            false,
            null,
            last?.Message ?? "Unknown error",
            maxAttempts);
    }
}

public readonly record struct CrawlerHtmlFetchRetryResult(
    bool Success,
    string? Html,
    string? ErrorMessage,
    int AttemptsMade);
