using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.Publish_website.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages;

public class IndexModel : PageModel
{
    private readonly PublicMasterDataCatalogClient _catalogClient;
    private readonly IConfiguration _configuration;

    public IndexModel(
        PublicMasterDataCatalogClient catalogClient,
        IConfiguration configuration)
    {
        _catalogClient = catalogClient;
        _configuration = configuration;
    }

    public IReadOnlyList<HomeBannerDto> HomeBanners { get; private set; } = Array.Empty<HomeBannerDto>();

    public IReadOnlyList<PlaceWithNavigationPropertiesDto> Places { get; private set; } =
        Array.Empty<PlaceWithNavigationPropertiesDto>();

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        HomeBanners = await _catalogClient.GetActiveHomeBannersAsync(8, now, cancellationToken);
        Places = await _catalogClient.GetPublishedPlacesAsync(12, cancellationToken);
    }

    /// <summary>
    /// Builds absolute media URLs when API returns paths relative to blob/CDN base.
    /// </summary>
    public string ResolveMediaUrl(string? url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            return string.Empty;
        }

        if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return url;
        }

        var baseUrl = _configuration["MediaFiles:PublicBaseUrl"]?.TrimEnd('/');
        if (string.IsNullOrEmpty(baseUrl))
        {
            return url;
        }

        return url.StartsWith('/') ? $"{baseUrl}{url}" : $"{baseUrl}/{url}";
    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
