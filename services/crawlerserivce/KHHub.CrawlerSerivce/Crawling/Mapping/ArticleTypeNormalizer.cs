using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.Places;

namespace KHHub.CrawlerSerivce.Crawling.Mapping;

public static class ArticleTypeNormalizer
{
    public static ArticleType FromCategoryLabel(string? label)
    {
        if (string.IsNullOrWhiteSpace(label))
        {
            return ArticleType.News;
        }

        var t = label.Trim().ToLowerInvariant();
        if (t.Contains("sự kiện", StringComparison.Ordinal) || t.Contains("su kien", StringComparison.Ordinal))
        {
            return ArticleType.Event;
        }

        if (t.Contains("du lịch", StringComparison.Ordinal) || t.Contains("du lich", StringComparison.Ordinal))
        {
            return ArticleType.Guide;
        }

        if (t.Contains("khuyến mãi", StringComparison.Ordinal) || t.Contains("quảng cáo", StringComparison.Ordinal))
        {
            return ArticleType.Promotion;
        }

        return ArticleType.News;
    }
}
