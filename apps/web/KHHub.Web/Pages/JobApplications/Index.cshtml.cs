using KHHub.MasterDataService.Entities.JobApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.JobApplications;
using KHHub.MasterDataService.Services.Dtos.JobApplications;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.JobApplications;

public abstract class IndexModelBase : AbpPageModel
{
    public string? UserIdFilter { get; set; }

    public string? FullNameFilter { get; set; }

    public string? EmailFilter { get; set; }

    public string? PhoneNumberFilter { get; set; }

    public string? CvUrlFilter { get; set; }

    public string? PortfolioUrlFilter { get; set; }

    public DateTime? AppliedAtFilterMin { get; set; }

    public DateTime? AppliedAtFilterMax { get; set; }

    public ApplicationStatus? StatusFilter { get; set; }

    [SelectItems(nameof(JobLookupList))]
    public Guid JobIdFilter { get; set; }

    public List<SelectListItem> JobLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IJobApplicationsAppService _jobApplicationsAppService;

    public IndexModelBase(IJobApplicationsAppService jobApplicationsAppService)
    {
        _jobApplicationsAppService = jobApplicationsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        JobLookupList.AddRange((await _jobApplicationsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}