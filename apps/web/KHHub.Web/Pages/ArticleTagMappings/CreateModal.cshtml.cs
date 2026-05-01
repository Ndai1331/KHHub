using System.Threading.Tasks;
using KHHub.MasterDataService.Services.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.ArticleTagMappings;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleTagMappingCreateViewModel ArticleTagMapping { get; set; }

    protected IArticleTagMappingsAppService _articleTagMappingsAppService;

    public CreateModalModelBase(IArticleTagMappingsAppService articleTagMappingsAppService)
    {
        _articleTagMappingsAppService = articleTagMappingsAppService;
        ArticleTagMapping = new();
    }

    public virtual Task OnGetAsync()
    {
        ArticleTagMapping = new ArticleTagMappingCreateViewModel();
        return Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articleTagMappingsAppService.CreateAsync(
            ObjectMapper.Map<ArticleTagMappingCreateViewModel, ArticleTagMappingCreateDto>(ArticleTagMapping));
        return NoContent();
    }
}

public class ArticleTagMappingCreateViewModel : ArticleTagMappingCreateDto
{
}
