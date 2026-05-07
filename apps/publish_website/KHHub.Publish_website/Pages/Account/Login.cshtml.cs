using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages.Account;

public sealed class LoginModel : PageModel
{
    public IActionResult OnGet(string? returnUrl = null)
    {
        var redirectUri = GetLocalReturnUrl(returnUrl);
        var properties = new AuthenticationProperties
        {
            RedirectUri = redirectUri
        };

        return Challenge(properties, OpenIdConnectDefaults.AuthenticationScheme);
    }

    private string GetLocalReturnUrl(string? returnUrl)
    {
        if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
        {
            return returnUrl;
        }

        return Url.Content("~/");
    }
}
