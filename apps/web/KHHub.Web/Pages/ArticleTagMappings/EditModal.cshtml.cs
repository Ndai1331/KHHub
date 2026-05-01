using System;
using System.Threading.Tasks;
using KHHub.MasterDataService.Services.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.ArticleTagMappings;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleTagMappingUpdateViewModel ArticleTagMapping { get; set; }

    protected IArticleTagMappingsAppService _articleTagMappingsAppService;

    public EditModalModelBase(IArticleTagMappingsAppService articleTagMappingsAppService)
    {
        _articleTagMappingsAppService = articleTagMappingsAppService;
        ArticleTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _articleTagMappingsAppService.GetWithNavigationPropertiesAsync(Id);
        ArticleTagMapping = ObjectMapper.Map<ArticleTagMappingDto, ArticleTagMappingUpdateViewModel>(
            dto.ArticleTagMapping);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articleTagMappingsAppService.UpdateAsync(
            Id,
            ObjectMapper.Map<ArticleTagMappingUpdateViewModel, ArticleTagMappingUpdateDto>(ArticleTagMapping));
        return NoContent();
    }
}

public class ArticleTagMappingUpdateViewModel : ArticleTagMappingUpdateDto
{
}
