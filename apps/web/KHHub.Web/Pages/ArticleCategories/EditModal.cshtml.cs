using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;

namespace KHHub.Web.Pages.ArticleCategories;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleCategoryUpdateViewModel ArticleCategory { get; set; }

    protected IArticleCategoriesAppService _articleCategoriesAppService;

    public EditModalModelBase(IArticleCategoriesAppService articleCategoriesAppService)
    {
        _articleCategoriesAppService = articleCategoriesAppService;
        ArticleCategory = new();
    }

    public virtual async Task OnGetAsync()
    {
        var articleCategory = await _articleCategoriesAppService.GetAsync(Id);
        ArticleCategory = ObjectMapper.Map<ArticleCategoryDto, ArticleCategoryUpdateViewModel>(articleCategory);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _articleCategoriesAppService.UpdateAsync(Id, ObjectMapper.Map<ArticleCategoryUpdateViewModel, ArticleCategoryUpdateDto>(ArticleCategory));
        return NoContent();
    }
}

public class ArticleCategoryUpdateViewModel : ArticleCategoryUpdateDto
{
}