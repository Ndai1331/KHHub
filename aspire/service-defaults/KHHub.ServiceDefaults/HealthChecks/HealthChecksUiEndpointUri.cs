using Microsoft.Extensions.Configuration;

namespace KHHub.ServiceDefaults.HealthChecks;

/// <summary>
/// Builds an absolute URL for HealthChecks.UI's in-process HTTP collector.
/// Relative paths + Kestrel listen URLs like http://[::]:80 produce http://[::]/health-status and fail DNS.
/// </summary>
public static class HealthChecksUiEndpointUri
{
    public static string Resolve(IConfiguration configuration, string healthCheckPath)
    {
        var explicitUri = configuration["App:HealthUiCheckUrl"];
        if (!string.IsNullOrWhiteSpace(explicitUri) &&
            explicitUri.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            return explicitUri;
        }

        var path = EnsureLeadingSlash(healthCheckPath);
        var urls = configuration["ASPNETCORE_URLS"];
        if (string.IsNullOrWhiteSpace(urls))
        {
            return $"http://127.0.0.1:80{path}";
        }

        var firstUrl = urls.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];
        var normalized = NormalizeLoopbackListenUrl(firstUrl);

        return $"{normalized.TrimEnd('/')}{path}";
    }

    private static string EnsureLeadingSlash(string path)
    {
        if (string.IsNullOrEmpty(path))
        {
            return "/";
        }

        return path.StartsWith('/') ? path : $"/{path}";
    }

    private static string NormalizeLoopbackListenUrl(string url)
    {
        return url
            .Replace("://+", "://127.0.0.1", StringComparison.OrdinalIgnoreCase)
            .Replace("://*", "://127.0.0.1", StringComparison.OrdinalIgnoreCase)
            .Replace("://0.0.0.0", "://127.0.0.1", StringComparison.OrdinalIgnoreCase)
            .Replace("://[::]", "://127.0.0.1", StringComparison.OrdinalIgnoreCase);
    }
}
