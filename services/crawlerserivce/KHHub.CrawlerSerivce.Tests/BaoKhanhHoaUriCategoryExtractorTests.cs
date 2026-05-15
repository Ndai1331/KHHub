using KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoKhanhHoa;
using Shouldly;
using Xunit;

namespace KHHub.CrawlerSerivce.Tests;

public class BaoKhanhHoaUriCategoryExtractorTests
{
    [Fact]
    public void TryExtractCategoryPath_SingleSegmentBeforeYyyyMm_MapsPrimaryAndNoParents()
    {
        var ok = BaoKhanhHoaUriCategoryExtractor.TryExtractCategoryPath(
            new Uri("https://www.baokhanhhoa.vn/xa-hoi/202605/my-article-slug"),
            out var primary,
            out var parents);

        ok.ShouldBeTrue();
        primary.ShouldBe("Xã hội");
        parents.ShouldBeEmpty();
    }

    [Fact]
    public void TryExtractCategoryPath_NestedPath_ParentsOrderedImmediateFirst()
    {
        var ok = BaoKhanhHoaUriCategoryExtractor.TryExtractCategoryPath(
            new Uri("https://baokhanhhoa.vn/chinh-tri/ten-chuyen-muc-con/202605/slug-id/"),
            out var primary,
            out var parents);

        ok.ShouldBeTrue();
        primary.ShouldBe("Ten Chuyen Muc Con");
        parents.Count.ShouldBe(1);
        parents[0].ShouldBe("Chính trị");
    }

    [Fact]
    public void SlugToDisplayName_KnownSlug_ReturnsVietnameseTitle()
    {
        BaoKhanhHoaUriCategoryExtractor.SlugToDisplayName("kinh-te").ShouldBe("Kinh tế");
    }

    [Fact]
    public void SlugToDisplayName_UnknownSlug_TitleCasesWords()
    {
        BaoKhanhHoaUriCategoryExtractor.SlugToDisplayName("foo-bar-baz").ShouldBe("Foo Bar Baz");
    }
}
