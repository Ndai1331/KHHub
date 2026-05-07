using KHHub.Publish_website.Pages.PublicContent;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;

namespace KHHub.Publish_website.Pages.Jobs;

public sealed class DetailsModel : PublicDetailPageModel
{
    public DetailsModel(IPublicContentCatalog catalog)
        : base(catalog)
    {
    }

    public IActionResult OnGet(string slug)
    {
        return LoadDetail(PublicContentKind.Job, slug, "/jobs", "Việc làm");
    }
}
