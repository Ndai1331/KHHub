using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.Publish_website.Services;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; 
using System.Globalization;

namespace KHHub.Publish_website.Pages;

public class IndexModel : PageModel
{
    private const string LandingDataCacheKey = "PublishWebsite:Landing:Data";

    /// <summary>Hero banner derivative widths (webp). Stored object key pattern: stem-768.webp, stem-1280.webp, stem-1920.webp.</summary>
    private static readonly int[] HeroBannerResponsiveWidths = [768, 1280, 1920];

    private readonly PublicMasterDataCatalogClient _catalogClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;
    private readonly IMemoryCache _cache;
    private readonly TimeProvider _timeProvider;
    private readonly IWebHostEnvironment _environment;

    public IndexModel(
        PublicMasterDataCatalogClient catalogClient,
        IConfiguration configuration,
        ILogger<IndexModel> logger,
        IMemoryCache cache,
        TimeProvider timeProvider,
        IWebHostEnvironment environment)
    {   
        _catalogClient = catalogClient;
        _configuration = configuration;
        _logger = logger;
        _cache = cache;
        _timeProvider = timeProvider;
        _environment = environment;
    }

    public IReadOnlyList<HomeBannerDto> HomeBanners { get; private set; } = Array.Empty<HomeBannerDto>();

    public IReadOnlyList<PlaceWithNavigationPropertiesDto> Places { get; private set; } =
        Array.Empty<PlaceWithNavigationPropertiesDto>();

    public IReadOnlyList<PublicContentCardViewModel> Jobs { get; private set; } =
        Array.Empty<PublicContentCardViewModel>();

    public string JobsListPath => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase)
        ? "/jobs"
        : "/viec-lam";

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var dataCacheKey = $"{LandingDataCacheKey}:{CultureInfo.CurrentUICulture.Name}";
        var data = await _cache.GetOrCreateAsync(
            dataCacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = GetCacheDuration("LandingCache:DataMinutes", TimeSpan.FromMinutes(10));
                var now = _timeProvider.GetUtcNow().UtcDateTime;
                var banners = await _catalogClient.GetActiveHomeBannersAsync(8, now, cancellationToken);
                var places = await _catalogClient.GetPublishedPlacesAsync(12, cancellationToken);
                var jobs = await _catalogClient.GetLatestPublishedJobCardsAsync(10, JobsListPath, cancellationToken);

                return new LandingPageCacheItem(banners, places, jobs);
            });

        HomeBanners = data?.HomeBanners ?? Array.Empty<HomeBannerDto>();
        Places = data?.Places ?? Array.Empty<PlaceWithNavigationPropertiesDto>();
        Jobs = data?.Jobs ?? Array.Empty<PublicContentCardViewModel>();
    }

    /// <summary>
    /// Resolves stored media paths to browser-loadable URL (same behavior as apps/web).
    /// </summary>
    public string ResolveMediaUrl(string? url)
    {
        var value = (url ?? string.Empty).Trim();
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        if (value.StartsWith("//", StringComparison.Ordinal))
        {
            return value;
        }

        var baseUrl = (_configuration["MediaFiles:PublicBaseUrl"] ?? string.Empty).Trim().TrimEnd('/');
        if (string.IsNullOrEmpty(baseUrl) || !value.StartsWith('/'))
        {
            return value;
        }

        try
        {
            var uri = new Uri(baseUrl);
            return uri.GetLeftPart(UriPartial.Authority) + value;
        }
        catch (UriFormatException)
        {
            return value;
        }
    }

    public IActionResult OnGetResetCache(string? key)
    {
        var expectedKey = _configuration["LandingCache:ResetKey"];
        if (string.IsNullOrWhiteSpace(expectedKey))
        {
            if (!_environment.IsDevelopment())
            {
                return NotFound();
            }
        }
        else if (!string.Equals(expectedKey, key, StringComparison.Ordinal))
        {
            return Unauthorized();
        }

        _cache.Remove(LandingDataCacheKey);
        _cache.Remove($"{LandingDataCacheKey}:vi");
        _cache.Remove($"{LandingDataCacheKey}:en");
        _logger.LogInformation("Publish website landing cache was reset.");

        return RedirectToPage("/Index");
    }

    /// <summary>
    /// Original image URL for img fallback plus a webp srcset string for &lt;source type="image/webp"&gt;.
    /// Derivatives fallback to master URL when a variant path is absent.
    /// </summary>
    public HeroBannerResponsiveSources GetHeroBannerResponsiveSources(string? imageUrl)
    {
        var fallback = ResolveMediaUrl(imageUrl);
        if (string.IsNullOrWhiteSpace(fallback))
        {
            return HeroBannerResponsiveSources.Empty;
        }

        var parts = new List<string>(HeroBannerResponsiveWidths.Length);
        foreach (var width in HeroBannerResponsiveWidths)
        {
            var url = ResolveHeroBannerWebpUrl(imageUrl, width);
            if (string.IsNullOrWhiteSpace(url))
            {
                continue;
            }

            parts.Add($"{EscapeSrcsetUrlCandidate(url)} {width}w");
        }

        if (parts.Count == 0)
        {
            parts.Add($"{EscapeSrcsetUrlCandidate(fallback)} 1920w");
        }

        return new HeroBannerResponsiveSources(FallbackUrl: fallback, WebpSrcset: string.Join(", ", parts));
    }

    private string ResolveHeroBannerWebpUrl(string? rawImageUrl, int width)
    {
        var normalizedKey = NormalizeMediaLookupKey(rawImageUrl);
        if (string.IsNullOrWhiteSpace(normalizedKey))
        {
            return ResolveMediaUrl(rawImageUrl);
        }

        var variantPath = BuildHeroWebpVariantPublicPath(normalizedKey, width);
        if (string.IsNullOrWhiteSpace(variantPath))
        {
            return ResolveMediaUrl(rawImageUrl);
        }

        var variantResolved = ResolveMediaUrl(variantPath);
        return string.IsNullOrWhiteSpace(variantResolved) ? ResolveMediaUrl(rawImageUrl) : variantResolved;
    }

    /// <remarks>Derived keys: path/to/name.ext → path/to/name-768.webp.</remarks>
    private static string BuildHeroWebpVariantPublicPath(string normalizedPublicPath, int width)
    {
        var path = normalizedPublicPath.Trim();
        if (path.Length == 0)
        {
            return string.Empty;
        }

        var lastSlash = path.LastIndexOf('/');
        var lastDot = path.LastIndexOf('.');
        var stem = lastDot > lastSlash ? path[..lastDot] : path;
        return stem + "-" + width + ".webp";
    }

    /// <remarks>Commas inside URLs break srcset tokenization — encode them.</remarks>
    private static string EscapeSrcsetUrlCandidate(string url)
        => url.Replace(",", "%2C");

    private string? NormalizeMediaLookupKey(string? url)
    {
        var value = (url ?? string.Empty).Trim();
        if (value.Length == 0)
        {
            return null;
        }

        if (value.StartsWith('/'))
        {
            return value.Split('?', '#')[0];
        }

        if (value.StartsWith("host/", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("tenants/", StringComparison.OrdinalIgnoreCase))
        {
            return "/" + value.Split('?', '#')[0].TrimStart('/');
        }

        var configuredBasePath = GetConfiguredMediaBasePath();
        if (!string.IsNullOrWhiteSpace(configuredBasePath) &&
            value.StartsWith(configuredBasePath.TrimStart('/') + "/", StringComparison.OrdinalIgnoreCase))
        {
            return "/" + value.Split('?', '#')[0].TrimStart('/');
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out var absolute))
        {
            return null;
        }

        if (Uri.TryCreate((_configuration["MediaFiles:PublicBaseUrl"] ?? string.Empty).Trim(), UriKind.Absolute, out var baseUri))
        {
            var basePath = baseUri.AbsolutePath.TrimEnd('/');
            if (basePath.Length == 0)
            {
                return absolute.AbsolutePath;
            }

            if (absolute.AbsolutePath.StartsWith(basePath + "/", StringComparison.Ordinal))
            {
                return absolute.AbsolutePath;
            }
        }

        return absolute.AbsolutePath;
    }

    private string GetConfiguredMediaBasePath()
    {
        var publicBaseUrl = (_configuration["MediaFiles:PublicBaseUrl"] ?? string.Empty).Trim();
        if (Uri.TryCreate(publicBaseUrl, UriKind.Absolute, out var baseUri))
        {
            return baseUri.AbsolutePath.Trim('/');
        }

        return publicBaseUrl.Trim('/');
    }

    private TimeSpan GetCacheDuration(string configurationKey, TimeSpan fallback)
    {
        var minutes = _configuration.GetValue<int?>(configurationKey);
        return minutes is > 0 ? TimeSpan.FromMinutes(minutes.Value) : fallback;
    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }

    private sealed record LandingPageCacheItem(
        IReadOnlyList<HomeBannerDto> HomeBanners,
        IReadOnlyList<PlaceWithNavigationPropertiesDto> Places,
        IReadOnlyList<PublicContentCardViewModel> Jobs);
}

/// <summary>Resolved hero slide assets for &lt;picture&gt; + webp srcset.</summary>
public sealed record HeroBannerResponsiveSources(string FallbackUrl, string WebpSrcset)
{
    public static HeroBannerResponsiveSources Empty { get; } = new(string.Empty, string.Empty);

    public bool HasImage => !string.IsNullOrWhiteSpace(FallbackUrl);
}
