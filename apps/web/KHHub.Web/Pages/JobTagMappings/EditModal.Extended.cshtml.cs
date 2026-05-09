using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobTagMappings;

namespace KHHub.Web.Pages.JobTagMappings;

public class EditModalModel : EditModalModelBase
{
    public EditModalModel(IJobTagMappingsAppService jobTagMappingsAppService) : base(jobTagMappingsAppService)
    {
    }
}