using KHHub.Publish_website.Pages.PublicContent;
using KHHub.Publish_website.Services.PublicContent;

namespace KHHub.Publish_website.Pages.Locations;

public sealed class IndexModel : PublicListingPageModel
{
    public IndexModel(IPublicContentCatalog catalog)
        : base(catalog)
    {
    }

    public void OnGet()
    {
        LoadListing(
            PublicContentKind.Location,
            "/locations",
            "Địa điểm Khánh Hòa | KH HUB",
            "Địa điểm đáng khám phá",
            "Khám phá địa điểm du lịch, ẩm thực, văn hóa và trải nghiệm địa phương tại Khánh Hòa.");
    }
}
