using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.Publish_website.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging; 

namespace KHHub.Publish_website.Pages;

public class IndexModel : PageModel
{
    private readonly PublicMasterDataCatalogClient _catalogClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<IndexModel> _logger;   
    public IndexModel(
        PublicMasterDataCatalogClient catalogClient,
        IConfiguration configuration,
        ILogger<IndexModel> logger)
    {   
        _catalogClient = catalogClient;
        _configuration = configuration;
        _logger = logger;
    }

    public IReadOnlyList<HomeBannerDto> HomeBanners { get; private set; } = Array.Empty<HomeBannerDto>();

    public IReadOnlyList<PlaceWithNavigationPropertiesDto> Places { get; private set; } =
        Array.Empty<PlaceWithNavigationPropertiesDto>();
    private Dictionary<string, string> MediaReadUrlMap { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        HomeBanners = await _catalogClient.GetActiveHomeBannersAsync(8, now, cancellationToken);
        _logger.LogInformation("HomeBanners: {HomeBanners}", HomeBanners.Count);
        _logger.LogInformation("HomeBanners: {HomeBanners}", HomeBanners.Select(b => b.ImageUrl).ToList());
        Places = await _catalogClient.GetPublishedPlacesAsync(12, cancellationToken);
        await LoadPresignedMediaUrlsAsync(cancellationToken);
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

        if (MediaReadUrlMap.TryGetValue(value, out var signed) && !string.IsNullOrWhiteSpace(signed))
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

    private async Task LoadPresignedMediaUrlsAsync(CancellationToken cancellationToken)
    {
        var rawPaths = HomeBanners
            .Select(b => b.ImageUrl)
            .Concat(Places.Select(p => p.Place?.ThumbnailUrl))
            .Concat(Places.Select(p => p.Place?.CoverImageUrl))
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x!.Trim())
            .Where(x => x.StartsWith('/'))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();

        if (rawPaths.Count == 0)
        {
            return;
        }

        foreach (var path in rawPaths)
        {
            var signed = await _catalogClient.GetPresignedReadUrlByPublicPathAsync(path, cancellationToken);
            if (!string.IsNullOrWhiteSpace(signed))
            {
                MediaReadUrlMap[path] = signed!;
            }
        }
    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
