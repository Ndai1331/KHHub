using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.IntegrationServices;

namespace KHHub.Web.Pages.Articles;

[Authorize]
public class ArticleMediaFileModel : AbpPageModel
{
    private readonly IArticleMediaIntegrationService _articleMediaIntegrationService;

    public ArticleMediaFileModel(IArticleMediaIntegrationService articleMediaIntegrationService)
    {
        _articleMediaIntegrationService = articleMediaIntegrationService;
    }

    public async Task<IActionResult> OnGetAsync(string name)
    {
        var content = await _articleMediaIntegrationService.GetAsync(name);
        var stream = content.GetStream();
        return File(stream, content.ContentType ?? "application/octet-stream");
    }
}
