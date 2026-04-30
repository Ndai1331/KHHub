using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;

namespace KHHub.Web.Pages.ArticleCategories;

public class IndexModel : IndexModelBase
{
    public IndexModel(IArticleCategoriesAppService articleCategoriesAppService) : base(articleCategoriesAppService)
    {
    }
}