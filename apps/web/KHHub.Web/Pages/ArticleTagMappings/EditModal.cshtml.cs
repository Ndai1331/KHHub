using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

namespace KHHub.Web.Pages.ArticleTagMappings;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleTagMappingUpdateViewModel ArticleTagMapping { get; set; }

    public List<SelectListItem> ArticleTagLookupListRequired { get; set; } = new List<SelectListItem> { };
    public List<SelectListItem> ArticleLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IArticleTagMappingsAppService _articleTagMappingsAppService;

    public EditModalModelBase(IArticleTagMappingsAppService articleTagMappingsAppService)
    {
        _articleTagMappingsAppService = articleTagMappingsAppService;
        ArticleTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        var articleTagMappingWithNavigationPropertiesDto = await _articleTagMappingsAppService.GetWithNavigationPropertiesAsync(Id);
        ArticleTagMapping = ObjectMapper.Map<ArticleTagMappingDto, ArticleTagMappingUpdateViewModel>(articleTagMappingWithNavigationPropertiesDto.ArticleTagMapping);
        ArticleTagLookupListRequired.AddRange((await _articleTagMappingsAppService.GetArticleTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        ArticleLookupListRequired.AddRange((await _articleTagMappingsAppService.GetArticleLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articleTagMappingsAppService.UpdateAsync(Id, ObjectMapper.Map<ArticleTagMappingUpdateViewModel, ArticleTagMappingUpdateDto>(ArticleTagMapping));
        return NoContent();
    }
}

public class ArticleTagMappingUpdateViewModel : ArticleTagMappingUpdateDto
{
}