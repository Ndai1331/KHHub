using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.JobTagMappings;

namespace KHHub.Web.Pages.JobTagMappings;

public class CreateModalModel : CreateModalModelBase
{
    public CreateModalModel(IJobTagMappingsAppService jobTagMappingsAppService) : base(jobTagMappingsAppService)
    {
    }
}