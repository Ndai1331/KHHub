using System.Globalization;
using KHHub.Publish_website.Pages.PublicContent;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;

namespace KHHub.Publish_website.Pages.News;

public sealed class DetailsModel : PublicDetailPageModel
{
    public DetailsModel(IPublicContentCatalog catalog)
        : base(catalog)
    {
    }

    public IActionResult OnGet(string slug)
    {
        var listPath = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase)
            ? "/news"
            : "/tin-tuc";
        return LoadDetail(PublicContentKind.News, slug, listPath, "Tin tức");
    }
}
