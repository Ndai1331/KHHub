using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

namespace KHHub.Web.Pages.ArticleTagMappings;

public class IndexModel : IndexModelBase
{
    public IndexModel(IArticleTagMappingsAppService articleTagMappingsAppService) : base(articleTagMappingsAppService)
    {
    }
}