using KHHub.MasterDataService.Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Services.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;
using Microsoft.Extensions.Localization;

namespace KHHub.Web.Pages.Articles;

public class IndexModel : IndexModelBase
{
    public IndexModel(
        IArticlesAppService articlesAppService,
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer) : base(articlesAppService, masterDataLocalizer)
    {
    }
}