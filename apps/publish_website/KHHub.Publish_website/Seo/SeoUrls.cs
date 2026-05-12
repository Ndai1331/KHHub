using Microsoft.Extensions.Configuration;

namespace KHHub.Publish_website.Seo;

/// <summary>
/// Resolves the public absolute site base (no trailing slash) for canonical, Open Graph, sitemap, and robots.
/// </summary>
public static class SeoUrls
{
    /// <summary>
    /// Order: Seo:PublicSiteUrl, App:SelfUrl, then current request origin.
    /// </summary>
    public static string ResolvePublicSiteBase(HttpRequest request, IConfiguration configuration)
    {
        var configured = (configuration["Seo:PublicSiteUrl"] ?? configuration["App:SelfUrl"] ?? string.Empty).Trim();
        if (!string.IsNullOrEmpty(configured))
        {
            if (Uri.TryCreate(configured, UriKind.Absolute, out var uri))
            {
                return uri.GetLeftPart(UriPartial.Authority).TrimEnd('/');
            }

            return configured.TrimEnd('/');
        }

        return $"{request.Scheme}://{request.Host.Value}".TrimEnd('/');
    }
}
