using KHHub.Publish_website.Pages.PublicContent;
using KHHub.Publish_website.Services.PublicContent;

namespace KHHub.Publish_website.Pages.Jobs;

public sealed class IndexModel : PublicListingPageModel
{
    public IndexModel(IPublicContentCatalog catalog)
        : base(catalog)
    {
    }

    public void OnGet()
    {
        LoadListing(
            PublicContentKind.Job,
            "/jobs",
            "Việc làm Khánh Hòa | KH HUB",
            "Cơ hội việc làm nổi bật",
            "Tìm việc làm ngành du lịch, công nghệ, dịch vụ và sáng tạo tại Khánh Hòa.");
    }
}
