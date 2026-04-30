using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;

namespace KHHub.Web.Pages.ArticleCategories;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ArticleCategoryCreateViewModel ArticleCategory { get; set; }

    protected IArticleCategoriesAppService _articleCategoriesAppService;

    public CreateModalModelBase(IArticleCategoriesAppService articleCategoriesAppService)
    {
        _articleCategoriesAppService = articleCategoriesAppService;
        ArticleCategory = new();
    }

    public virtual async Task OnGetAsync()
    {
        ArticleCategory = new ArticleCategoryCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _articleCategoriesAppService.CreateAsync(ObjectMapper.Map<ArticleCategoryCreateViewModel, ArticleCategoryCreateDto>(ArticleCategory));
        return NoContent();
    }
}

public class ArticleCategoryCreateViewModel : ArticleCategoryCreateDto
{
}