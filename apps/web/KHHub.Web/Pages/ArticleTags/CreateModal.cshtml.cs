using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;

namespace KHHub.Web.Pages.ArticleTags;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleTagCreateViewModel ArticleTag { get; set; }

    protected IArticleTagsAppService _articleTagsAppService;

    public CreateModalModelBase(IArticleTagsAppService articleTagsAppService)
    {
        _articleTagsAppService = articleTagsAppService;
        ArticleTag = new();
    }

    public virtual async Task OnGetAsync()
    {
        ArticleTag = new ArticleTagCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articleTagsAppService.CreateAsync(ObjectMapper.Map<ArticleTagCreateViewModel, ArticleTagCreateDto>(ArticleTag));
        return NoContent();
    }
}

public class ArticleTagCreateViewModel : ArticleTagCreateDto
{
}