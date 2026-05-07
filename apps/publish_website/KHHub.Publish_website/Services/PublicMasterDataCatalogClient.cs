using System.Net.Http.Json;
using System.Text.Json;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.Places;
using Microsoft.Extensions.Logging;

namespace KHHub.Publish_website.Services;

/// <summary>
/// Loads public catalog data from MasterData via the Web Gateway (YARP).
/// </summary>
public class PublicMasterDataCatalogClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly ILogger<PublicMasterDataCatalogClient> _logger;

    public PublicMasterDataCatalogClient(
        HttpClient httpClient,
        ILogger<PublicMasterDataCatalogClient> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<IReadOnlyList<HomeBannerDto>> GetActiveHomeBannersAsync(
        int maxCount,
        DateTime now,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var sorting = Uri.EscapeDataString("SortOrder asc");
            var path = $"api/masterdata/home-banners?skipCount=0&maxResultCount={maxCount}&sorting={sorting}&isActive=true";
            using var response = await _httpClient.GetAsync(path, cancellationToken);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedResult<HomeBannerDto>>(JsonOptions, cancellationToken);
            if (page?.Items == null || page.Items.Count == 0)
            {
                return Array.Empty<HomeBannerDto>();
            }

            return page.Items
                .Where(b =>
                    (!b.StartDate.HasValue || b.StartDate.Value <= now) &&
                    (!b.EndDate.HasValue || b.EndDate.Value >= now))
                .OrderBy(b => b.SortOrder)
                .Take(maxCount)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load home banners from MasterData gateway.");
            return Array.Empty<HomeBannerDto>();
        }
    }

    public async Task<IReadOnlyList<PlaceWithNavigationPropertiesDto>> GetPublishedPlacesAsync(
        int maxCount,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var sorting = Uri.EscapeDataString("RatingAveraged desc,ViewCount desc");
            var status = (int)PlaceStatus.Published;
            var path =
                $"api/masterdata/places?skipCount=0&maxResultCount={maxCount}&sorting={sorting}&status={status}";
            using var response = await _httpClient.GetAsync(path, cancellationToken);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedResult<PlaceWithNavigationPropertiesDto>>(
                JsonOptions,
                cancellationToken);
            return page?.Items is { Count: > 0 }
                ? page.Items
                : Array.Empty<PlaceWithNavigationPropertiesDto>();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load places from MasterData gateway.");
            return Array.Empty<PlaceWithNavigationPropertiesDto>();
        }
    }

    public async Task<string?> GetPresignedReadUrlByPublicPathAsync(
        string? publicPath,
        CancellationToken cancellationToken = default)
    {
        var path = (publicPath ?? string.Empty).Trim();
        if (path.Length == 0)
        {
            return null;
        }

        try
        {
            var encoded = Uri.EscapeDataString(path);
            var endpoint = $"api/masterdata/media-files/presigned-read-url-by-public-path?publicPath={encoded}";
            using var response = await _httpClient.GetAsync(endpoint, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var value = await response.Content.ReadFromJsonAsync<string>(JsonOptions, cancellationToken);
            return string.IsNullOrWhiteSpace(value) ? null : value;
        }
        catch (Exception ex)
        {
            _logger.LogDebug(ex, "Failed to load presigned read URL for path: {PublicPath}", path);
            return null;
        }
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];
}
