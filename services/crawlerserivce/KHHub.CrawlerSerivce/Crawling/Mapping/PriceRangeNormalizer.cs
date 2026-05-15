using System;
using KHHub.MasterDataService.Entities.Places;

namespace KHHub.CrawlerSerivce.Crawling.Mapping;

public static class PriceRangeNormalizer
{
    public static PriceRange TryMap(string? label)
    {
        if (string.IsNullOrWhiteSpace(label))
        {
            return PriceRange.Free;
        }

        var t = label.Trim().ToLowerInvariant();
        if (t.Contains("luxury") || t.Contains("cao cấp") || t.Contains("sang"))
        {
            return PriceRange.Luxury;
        }

        if (t.Contains("đắt") || t.Contains("cao") || t.Contains("$$$$") || t.Contains("$$$"))
        {
            return PriceRange.High;
        }

        if (t.Contains("trung") || t.Contains("$$") || t.Contains("tb"))
        {
            return PriceRange.Medium;
        }

        if (t.Contains("rẻ") || t.Contains("bình dân") || t.Contains("$") || t.Contains("giá tốt"))
        {
            return PriceRange.Cheap;
        }

        if (t.Contains("miễn phí") || t.Contains("free"))
        {
            return PriceRange.Free;
        }

        return PriceRange.Medium;
    }
}
