using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

namespace KHHub.Web.Pages.ArticleTagMappings;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleTagMappingCreateViewModel ArticleTagMapping { get; set; }

    public List<SelectListItem> ArticleTagLookupListRequired { get; set; } = new List<SelectListItem> { };
    public List<SelectListItem> ArticleLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IArticleTagMappingsAppService _articleTagMappingsAppService;

    public CreateModalModelBase(IArticleTagMappingsAppService articleTagMappingsAppService)
    {
        _articleTagMappingsAppService = articleTagMappingsAppService;
        ArticleTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        ArticleTagMapping = new ArticleTagMappingCreateViewModel();
        ArticleTagLookupListRequired.AddRange((await _articleTagMappingsAppService.GetArticleTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        ArticleLookupListRequired.AddRange((await _articleTagMappingsAppService.GetArticleLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articleTagMappingsAppService.CreateAsync(ObjectMapper.Map<ArticleTagMappingCreateViewModel, ArticleTagMappingCreateDto>(ArticleTagMapping));
        return NoContent();
    }
}

public class ArticleTagMappingCreateViewModel : ArticleTagMappingCreateDto
{
}