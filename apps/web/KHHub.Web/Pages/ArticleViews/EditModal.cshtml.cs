using System;
using System.Threading.Tasks;
using KHHub.MasterDataService.Services.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.ArticleViews;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleViewUpdateViewModel ArticleView { get; set; }

    protected IArticleViewsAppService _articleViewsAppService;

    public EditModalModelBase(IArticleViewsAppService articleViewsAppService)
    {
        _articleViewsAppService = articleViewsAppService;
        ArticleView = new();
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _articleViewsAppService.GetWithNavigationPropertiesAsync(Id);
        ArticleView = ObjectMapper.Map<ArticleViewDto, ArticleViewUpdateViewModel>(dto.ArticleView);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articleViewsAppService.UpdateAsync(
            Id,
            ObjectMapper.Map<ArticleViewUpdateViewModel, ArticleViewUpdateDto>(ArticleView));
        return NoContent();
    }
}

public class ArticleViewUpdateViewModel : ArticleViewUpdateDto
{
}
