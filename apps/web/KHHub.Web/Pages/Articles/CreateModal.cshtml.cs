using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;

namespace KHHub.Web.Pages.Articles;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleCreateViewModel Article { get; set; }

    public List<SelectListItem> ArticleCategoryLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IArticlesAppService _articlesAppService;

    public CreateModalModelBase(IArticlesAppService articlesAppService)
    {
        _articlesAppService = articlesAppService;
        Article = new();
    }

    public virtual async Task OnGetAsync()
    {
        Article = new ArticleCreateViewModel();
        ArticleCategoryLookupListRequired.AddRange((await _articlesAppService.GetArticleCategoryLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articlesAppService.CreateAsync(ObjectMapper.Map<ArticleCreateViewModel, ArticleCreateDto>(Article));
        return NoContent();
    }
}

public class ArticleCreateViewModel : ArticleCreateDto
{
}