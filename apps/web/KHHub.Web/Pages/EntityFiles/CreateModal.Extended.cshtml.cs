using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.EntityFiles;

namespace KHHub.Web.Pages.EntityFiles;

public class CreateModalModel : CreateModalModelBase
{
    public CreateModalModel(IEntityFilesAppService entityFilesAppService) : base(entityFilesAppService)
    {
    }
}