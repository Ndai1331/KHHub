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
        LoadListing(
            PublicContentKind.News,
            "/news",
            "Tin tức Khánh Hòa | KH HUB",
            "Tin tức mới nhất",
            "Cập nhật tin tức, sự kiện, du lịch, công nghệ và đời sống tại Khánh Hòa.");
    }
}
