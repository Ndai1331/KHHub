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

namespace KHHub.Web.Pages.ArticleTags;

public class CreateModalModel : CreateModalModelBase
{
    public CreateModalModel(IArticleTagsAppService articleTagsAppService) : base(articleTagsAppService)
    {
    }
}