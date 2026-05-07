using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages.Account;

public sealed class LogoutModel : PageModel
{
    public IActionResult OnGet(string? returnUrl = null)
    {
        var redirectUri = !string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)
            ? returnUrl
            : Url.Content("~/");

        var properties = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };

        return SignOut(properties, CookieAuthenticationDefaults.AuthenticationScheme, OpenIdConnectDefaults.AuthenticationScheme);
    }
}
