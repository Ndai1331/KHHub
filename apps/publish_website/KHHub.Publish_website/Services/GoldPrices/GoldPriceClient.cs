using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;

namespace KHHub.Publish_website.Services.GoldPrices;

public sealed class GoldPriceClient(HttpClient httpClient, IMemoryCache cache, ILogger<GoldPriceClient> logger) : IGoldPriceClient
{
    private const string CacheKey = "khhub.gold-prices.latest";

    public async Task<GoldPriceSnapshot> GetLatestAsync(CancellationToken cancellationToken = default)
    {
        if (cache.TryGetValue(CacheKey, out GoldPriceSnapshot? cached) && cached is not null)
        {
            return cached;
        }

        try
        {
            var snapshot = await httpClient.GetFromJsonAsync<GoldPriceSnapshot>("api/gold", cancellationToken);
            if (snapshot?.Data.Count > 0)
            {
                cache.Set(CacheKey, snapshot, TimeSpan.FromMinutes(1));
                return snapshot;
            }
        }
        catch (Exception ex) when (ex is HttpRequestException or TaskCanceledException or System.Text.Json.JsonException)
        {
            logger.LogWarning(ex, "Unable to fetch live gold prices.");
        }

        var fallback = new GoldPriceSnapshot
        {
            Data = FallbackData,
            Source = "sjc.com.vn",
            UpdatedAt = new DateTimeOffset(2026, 4, 27, 7, 37, 7, TimeSpan.Zero),
            Stale = true,
            IsFallback = true,
            ErrorMessage = "Đang dùng dữ liệu dự phòng vì chưa lấy được giá vàng mới nhất."
        };

        cache.Set(CacheKey, fallback, TimeSpan.FromSeconds(30));
        return fallback;
    }

    private static readonly IReadOnlyList<GoldPriceItem> FallbackData =
    [
        new() { Type = "Vàng SJC 1L, 10L, 1KG", BuyPrice = 166300, SellPrice = 168800, Region = "Hồ Chí Minh" },
        new() { Type = "Vàng SJC 5 chỉ", BuyPrice = 166300, SellPrice = 168820, Region = "Hồ Chí Minh" },
        new() { Type = "Vàng SJC 0.5 chỉ, 1 chỉ, 2 chỉ", BuyPrice = 166300, SellPrice = 168830, Region = "Hồ Chí Minh" },
        new() { Type = "Vàng nhẫn SJC 99,99% 1 chỉ, 2 chỉ, 5 chỉ", BuyPrice = 165800, SellPrice = 168300, Region = "Hồ Chí Minh" },
        new() { Type = "Vàng nhẫn SJC 99,99% 0.5 chỉ, 0.3 chỉ", BuyPrice = 165800, SellPrice = 168400, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 99,99%", BuyPrice = 163800, SellPrice = 166800, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 99%", BuyPrice = 158649, SellPrice = 165149, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 75%", BuyPrice = 116363, SellPrice = 125263, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 68%", BuyPrice = 104685, SellPrice = 113585, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 61%", BuyPrice = 93008, SellPrice = 101908, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 58,3%", BuyPrice = 88504, SellPrice = 97404, Region = "Hồ Chí Minh" },
        new() { Type = "Nữ trang 41,7%", BuyPrice = 60813, SellPrice = 69713, Region = "Hồ Chí Minh" },
        new() { Type = "Vàng SJC 1L, 10L, 1KG", BuyPrice = 166300, SellPrice = 168800, Region = "Miền Trung" }
    ];
}

