using System.Globalization;
using KHHub.Publish_website.Pages.PublicContent;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;

namespace KHHub.Publish_website.Pages.Locations;

public sealed class DetailsModel : PublicDetailPageModel
{
    public DetailsModel(IPublicContentCatalog catalog)
        : base(catalog)
    {
    }

    public IActionResult OnGet(string slug)
    {
        var listPath = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase)
            ? "/places"
            : "/dia-diem";
        return LoadDetail(PublicContentKind.Location, slug, listPath, "Địa điểm");
    }
}
