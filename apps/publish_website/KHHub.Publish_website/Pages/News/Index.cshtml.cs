using System.Globalization;
using KHHub.Publish_website.Pages.PublicContent;
using KHHub.Publish_website.Services.PublicContent;

namespace KHHub.Publish_website.Pages.News;

public sealed class IndexModel : PublicListingPageModel
{
    public IndexModel(IPublicContentCatalog catalog)
        : base(catalog)
    {
    }

    public void OnGet()
    {
        var path = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase)
            ? "/news"
            : "/tin-tuc";
        LoadListing(
            PublicContentKind.News,
            path,
            "Tin tức Khánh Hòa | KH HUB",
            "Tin tức mới nhất",
            "Cập nhật tin tức, sự kiện, du lịch, công nghệ và đời sống tại Khánh Hòa.");
    }
}
