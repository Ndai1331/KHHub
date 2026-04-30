using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;

namespace KHHub.Web.Pages.ArticleTags;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleTagUpdateViewModel ArticleTag { get; set; }

    protected IArticleTagsAppService _articleTagsAppService;

    public EditModalModelBase(IArticleTagsAppService articleTagsAppService)
    {
        _articleTagsAppService = articleTagsAppService;
        ArticleTag = new();
    }

    public virtual async Task OnGetAsync()
    {
        var articleTag = await _articleTagsAppService.GetAsync(Id);
        ArticleTag = ObjectMapper.Map<ArticleTagDto, ArticleTagUpdateViewModel>(articleTag);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articleTagsAppService.UpdateAsync(Id, ObjectMapper.Map<ArticleTagUpdateViewModel, ArticleTagUpdateDto>(ArticleTag));
        return NoContent();
    }
}

public class ArticleTagUpdateViewModel : ArticleTagUpdateDto
{
}