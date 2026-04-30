using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;

namespace KHHub.Web.Pages.ArticleViews;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleViewCreateViewModel ArticleView { get; set; }

    public List<SelectListItem> ArticleLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IArticleViewsAppService _articleViewsAppService;

    public CreateModalModelBase(IArticleViewsAppService articleViewsAppService)
    {
        _articleViewsAppService = articleViewsAppService;
        ArticleView = new();
    }

    public virtual async Task OnGetAsync()
    {
        ArticleView = new ArticleViewCreateViewModel();
        ArticleLookupListRequired.AddRange((await _articleViewsAppService.GetArticleLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articleViewsAppService.CreateAsync(ObjectMapper.Map<ArticleViewCreateViewModel, ArticleViewCreateDto>(ArticleView));
        return NoContent();
    }
}

public class ArticleViewCreateViewModel : ArticleViewCreateDto
{
}