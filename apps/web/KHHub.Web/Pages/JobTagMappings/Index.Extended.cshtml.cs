using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;

namespace KHHub.Web.Pages.JobTagMappings;

public class IndexModel : IndexModelBase
{
    public IndexModel(IJobTagMappingsAppService jobTagMappingsAppService) : base(jobTagMappingsAppService)
    {
    }
}