using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Jobs;
using KHHub.MasterDataService.Services.Dtos.Jobs;

namespace KHHub.Web.Pages.Jobs;

public class IndexModel : IndexModelBase
{
    public IndexModel(IJobsAppService jobsAppService) : base(jobsAppService)
    {
    }
}