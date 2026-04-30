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

namespace KHHub.Web.Pages.ArticleTagMappings;

public class EditModalModel : EditModalModelBase
{
    public EditModalModel(IArticleTagMappingsAppService articleTagMappingsAppService) : base(articleTagMappingsAppService)
    {
    }
}