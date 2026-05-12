using System.Text.Json;
using KHHub.Publish_website.Services.GoldPrices;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages.GoldPrices;

public sealed class IndexModel(IGoldPriceClient goldPriceClient) : PageModel
{
    public GoldPriceSnapshot Snapshot { get; private set; } = new();

    public string CanonicalUrl { get; private set; } = string.Empty;

    public string JsonLd { get; private set; } = "{}";

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        Snapshot = await goldPriceClient.GetLatestAsync(cancellationToken);
        var currentCulture = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
        var goldPricePath = currentCulture.Equals("en", StringComparison.OrdinalIgnoreCase) ? "/gold-price" : "/gia-vang";
        CanonicalUrl = $"{Request.Scheme}://{Request.Host}{goldPricePath}";
        JsonLd = BuildJsonLd();
    }

    public string FormatPrice(decimal price)
    {
        return price.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN"));
    }

    private string BuildJsonLd()
    {
        var payload = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "WebPage",
            ["name"] = "Giá vàng hôm nay",
            ["description"] = "Bảng giá vàng SJC, vàng nhẫn, nữ trang và biểu đồ XAUUSD thời gian thực.",
            ["url"] = CanonicalUrl,
            ["dateModified"] = Snapshot.UpdatedAt?.UtcDateTime.ToString("O"),
            ["mainEntity"] = new Dictionary<string, object?>
            {
                ["@type"] = "Dataset",
                ["name"] = "Bảng giá vàng trong nước",
                ["sourceOrganization"] = Snapshot.Source,
                ["variableMeasured"] = Snapshot.Data.Select(item => new Dictionary<string, object?>
                {
                    ["@type"] = "PropertyValue",
                    ["name"] = item.Type,
                    ["value"] = item.SellPrice,
                    ["unitText"] = "nghìn đồng/lượng"
                })
            }
        };

        return JsonSerializer.Serialize(payload, new JsonSerializerOptions(JsonSerializerDefaults.Web));
    }
}

