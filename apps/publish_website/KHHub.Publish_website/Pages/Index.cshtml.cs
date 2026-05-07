using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.Publish_website.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; 

namespace KHHub.Publish_website.Pages;

public class IndexModel : PageModel
{
    private const string LandingDataCacheKey = "PublishWebsite:Landing:Data";
    private const string LandingMediaCacheKey = "PublishWebsite:Landing:MediaReadUrls";

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

    private IReadOnlyDictionary<string, string> MediaReadUrlMap { get; set; } =
        new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var data = await _cache.GetOrCreateAsync(
            LandingDataCacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = GetCacheDuration("LandingCache:DataMinutes", TimeSpan.FromMinutes(10));
                var now = _timeProvider.GetUtcNow().UtcDateTime;
                var banners = await _catalogClient.GetActiveHomeBannersAsync(8, now, cancellationToken);
                var places = await _catalogClient.GetPublishedPlacesAsync(12, cancellationToken);

                return new LandingPageCacheItem(banners, places);
            });

        HomeBanners = data?.HomeBanners ?? Array.Empty<HomeBannerDto>();
        Places = data?.Places ?? Array.Empty<PlaceWithNavigationPropertiesDto>();
        MediaReadUrlMap = await LoadPresignedMediaUrlsAsync(cancellationToken);
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

        var lookupKey = NormalizeMediaLookupKey(value);
        if (!string.IsNullOrWhiteSpace(lookupKey) &&
            MediaReadUrlMap.TryGetValue(lookupKey, out var signed) &&
            !string.IsNullOrWhiteSpace(signed))
        {
            return signed;
        }

        if (MediaReadUrlMap.TryGetValue(value, out signed) && !string.IsNullOrWhiteSpace(signed))
        {
            return signed;
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
        _cache.Remove(LandingMediaCacheKey);
        _logger.LogInformation("Publish website landing cache was reset.");

        return RedirectToPage("/Index");
    }

    private async Task<IReadOnlyDictionary<string, string>> LoadPresignedMediaUrlsAsync(CancellationToken cancellationToken)
    {
        var rawPaths = HomeBanners
            .Select(b => b.ImageUrl)
            .Concat(Places.Select(p => p.Place?.ThumbnailUrl))
            .Concat(Places.Select(p => p.Place?.CoverImageUrl))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x!.Trim())
            .Select(NormalizeMediaLookupKey)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (rawPaths.Count == 0)
        {
            return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        var cached = await _cache.GetOrCreateAsync(
            LandingMediaCacheKey,
            async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = GetCacheDuration("LandingCache:MediaMinutes", TimeSpan.FromMinutes(30));
                var signedUrls = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                foreach (var path in rawPaths)
                {
                    var signed = await _catalogClient.GetPresignedReadUrlByPublicPathAsync(path, cancellationToken);
                    if (!string.IsNullOrWhiteSpace(signed))
                    {
                        signedUrls[path] = signed!;
                    }
                }

                return signedUrls;
            });

        return cached ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }

    private string? NormalizeMediaLookupKey(string? url)
    {
        var value = (url ?? string.Empty).Trim();
        if (value.Length == 0)
        {
            return null;
        }

        if (value.StartsWith('/'))
        {
            return value;
        }

        if (!Uri.TryCreate(value, UriKind.Absolute, out var absolute))
        {
            return null;
        }

        var publicBaseUrl = (_configuration["MediaFiles:PublicBaseUrl"] ?? string.Empty).Trim();
        if (Uri.TryCreate(publicBaseUrl, UriKind.Absolute, out var baseUri))
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
        IReadOnlyList<PlaceWithNavigationPropertiesDto> Places);
}
