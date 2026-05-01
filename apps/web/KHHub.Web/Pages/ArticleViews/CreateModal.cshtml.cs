using System.Threading.Tasks;
using KHHub.MasterDataService.Services.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.ArticleViews;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleViewCreateViewModel ArticleView { get; set; }

    protected IArticleViewsAppService _articleViewsAppService;

    public CreateModalModelBase(IArticleViewsAppService articleViewsAppService)
    {
        _articleViewsAppService = articleViewsAppService;
        ArticleView = new();
    }

    public virtual Task OnGetAsync()
    {
        ArticleView = new ArticleViewCreateViewModel();
        return Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articleViewsAppService.CreateAsync(
            ObjectMapper.Map<ArticleViewCreateViewModel, ArticleViewCreateDto>(ArticleView));
        return NoContent();
    }
}

public class ArticleViewCreateViewModel : ArticleViewCreateDto
{
}
