using System;
using System.Linq;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Crawling.Mapping;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoKhanhHoa;
using KHHub.MasterDataService.Entities.Articles;
using Shouldly;
using Xunit;

namespace KHHub.CrawlerSerivce.Tests;

public class BaoKhanhHoaArticleSiteHandlerParseTests
{
    private static readonly BaoKhanhHoaArticleSiteHandler Handler = new();

    [Fact]
    public async Task ParseArticleDetailHtmlAsync_MergesLdJsonKeywordsMetaTagsBreadcrumbAndUri()
    {
        const string html = """
                            <html><head>
                            <meta name="keywords" content="alpha, beta; gamma" />
                            <meta property="article:tag" content="metaTag" />
                            <script type="application/ld+json">
                            {"@type":"NewsArticle","articleSection":["Xã hội","An ninh"],"keywords":"ld1, ld2","description":"d","articleBody":"<p>body</p>","datePublished":"2026-05-01T00:00:00Z"}
                            </script>
                            </head><body>
                            <div class="breadcrumb"><a href="/">Trang chủ</a><a href="/xa-hoi">Xã hội</a><a href="#">Tin tức</a></div>
                            </body></html>
                            """;

        var docUri = new Uri("https://www.baokhanhhoa.vn/chinh-tri/doi-ngoai/202605/some-slug-id");
        var patch = await Handler.ParseArticleDetailHtmlAsync(html, docUri);

        patch.CategoryLabel.ShouldBe("An ninh");
        patch.ArticleCategoryNameCandidates.ShouldContain("Xã hội");
        patch.ArticleCategoryNameCandidates.ShouldContain("Tin tức");
        patch.ArticleCategoryNameCandidates.ShouldContain("Đối ngoại");
        patch.ArticleCategoryNameCandidates.ShouldContain("Chính trị");

        patch.TopicLabels.OrderBy(x => x, StringComparer.Ordinal).ToArray().ShouldBe(
            new[] { "alpha", "beta", "gamma", "ld1", "ld2", "metaTag" });
    }

    [Fact]
    public async Task ParseListingHtmlAsync_SetsCategoryFromArticleHref()
    {
        const string html = """
                            <html><body>
                            <a href="/xa-hoi/202605/a-title-long-enough/">News title here</a>
                            </body></html>
                            """;

        var list = await Handler.ParseListingHtmlAsync(html, new Uri("https://www.baokhanhhoa.vn/"));
        list.Count.ShouldBe(1);
        list[0].CategoryLabel.ShouldBe("Xã hội");
        list[0].ArticleCategoryNameCandidates.ShouldBeEmpty();
    }

    [Fact]
    public void ArticleCrawlerMergeMapper_MergesDistinctCategoryCandidatesWithoutDuplicatingPrimary()
    {
        var listing = new CrawledArticleListItem
        {
            SourceAbsoluteUrl = "https://www.baokhanhhoa.vn/x",
            Title = "Title long enough here",
            CategoryLabel = "Leaf",
            ArticleCategoryNameCandidates = { "ListingParent" }
        };

        var detail = new CrawledArticleDetailPatch
        {
            ContentHtml = "<p>x</p>",
            CategoryLabel = "Leaf",
            ArticleCategoryNameCandidates = { "DetailParent", "ListingParent" },
            TopicLabels = { "t1" }
        };

        var dto = ArticleCrawlerMergeMapper.ToUpsertDto(
            listing,
            detail,
            Guid.Empty,
            "Author",
            ArticleStatus.Published,
            extraTagNames: new[] { "t2", "t1" });

        dto.PrimaryArticleCategoryName.ShouldBe("Leaf");
        dto.ArticleCategoryNameCandidates.ShouldBe(new[] { "DetailParent", "ListingParent" }, ignoreOrder: false);
        dto.TagNames.ShouldBe(new[] { "t2", "t1" }, ignoreOrder: false);
    }
}
