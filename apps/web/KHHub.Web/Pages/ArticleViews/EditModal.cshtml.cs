using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;

namespace KHHub.Web.Pages.ArticleViews;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleViewUpdateViewModel ArticleView { get; set; }

    public List<SelectListItem> ArticleLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IArticleViewsAppService _articleViewsAppService;

    public EditModalModelBase(IArticleViewsAppService articleViewsAppService)
    {
        _articleViewsAppService = articleViewsAppService;
        ArticleView = new();
    }

    public virtual async Task OnGetAsync()
    {
        var articleViewWithNavigationPropertiesDto = await _articleViewsAppService.GetWithNavigationPropertiesAsync(Id);
        ArticleView = ObjectMapper.Map<ArticleViewDto, ArticleViewUpdateViewModel>(articleViewWithNavigationPropertiesDto.ArticleView);
        ArticleLookupListRequired.AddRange((await _articleViewsAppService.GetArticleLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articleViewsAppService.UpdateAsync(Id, ObjectMapper.Map<ArticleViewUpdateViewModel, ArticleViewUpdateDto>(ArticleView));
        return NoContent();
    }
}

public class ArticleViewUpdateViewModel : ArticleViewUpdateDto
{
}