namespace KHHub.CrawlerSerivce.Crawling;

/// <summary>
/// Normalizes URI hosts for stable matching (whitespace, trailing dot from DNS-style hosts).
/// </summary>
public static class CrawlerUriHostNormalizer
{
    public static string NormalizeHost(string host)
    {
        if (string.IsNullOrWhiteSpace(host))
        {
            return host;
        }

        return host.Trim().TrimEnd('.');
    }
}
