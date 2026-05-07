using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages;

/// <summary>
/// Persists the user's language selection in a cookie so that the choice
/// survives subsequent navigation. Without this handler the query string
/// would only affect the current request and the site would fall back to
/// the configured default culture (vi) on the next page.
/// </summary>
public class SetLanguageModel : PageModel
{
    private static readonly HashSet<string> SupportedCultures = new(StringComparer.OrdinalIgnoreCase)
    {
        "vi",
        "en"
    };

    public IActionResult OnGet(string? culture, string? returnUrl)
    {
        if (string.IsNullOrWhiteSpace(culture) || !SupportedCultures.Contains(culture))
        {
            culture = "vi";
        }

        Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddYears(1),
                IsEssential = true,
                HttpOnly = false,
                SameSite = SameSiteMode.Lax
            });

        var safeReturnUrl = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? returnUrl!
            : "/";

        return LocalRedirect(safeReturnUrl);
    }
}
