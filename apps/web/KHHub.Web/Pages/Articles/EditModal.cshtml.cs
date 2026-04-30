using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;

namespace KHHub.Web.Pages.Articles;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleUpdateViewModel Article { get; set; }

    public List<SelectListItem> ArticleCategoryLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IArticlesAppService _articlesAppService;

    public EditModalModelBase(IArticlesAppService articlesAppService)
    {
        _articlesAppService = articlesAppService;
        Article = new();
    }

    public virtual async Task OnGetAsync()
    {
        var articleWithNavigationPropertiesDto = await _articlesAppService.GetWithNavigationPropertiesAsync(Id);
        Article = ObjectMapper.Map<ArticleDto, ArticleUpdateViewModel>(articleWithNavigationPropertiesDto.Article);
        ArticleCategoryLookupListRequired.AddRange((await _articlesAppService.GetArticleCategoryLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articlesAppService.UpdateAsync(Id, ObjectMapper.Map<ArticleUpdateViewModel, ArticleUpdateDto>(Article));
        return NoContent();
    }
}

public class ArticleUpdateViewModel : ArticleUpdateDto
{
}