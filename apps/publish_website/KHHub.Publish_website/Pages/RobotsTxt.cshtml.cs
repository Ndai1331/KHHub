using KHHub.Publish_website.Seo;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace KHHub.Publish_website.Pages;

public class RobotsTxtModel : PageModel
{
    private readonly IConfiguration _configuration;

    public RobotsTxtModel(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string SitemapAbsoluteUrl => SeoUrls.ResolvePublicSiteBase(HttpContext.Request, _configuration) + "/sitemap.xml";
}
