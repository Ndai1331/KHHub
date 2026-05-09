using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Services.Jobs;
using KHHub.MasterDataService.Services.Provinces;
using KHHub.Web.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Jobs;

[Authorize(MasterDataServicePermissions.Jobs.Edit)]
public class EditModel : AbpPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobUpdatePageViewModel Job { get; set; } = new();

    public List<SelectListItem> EmploymentTypeList { get; set; } = [];

    public List<SelectListItem> WorkModeList { get; set; } = [];

    public List<SelectListItem> ExperienceLevelList { get; set; } = [];

    public List<SelectListItem> JobStatusList { get; set; } = [];

    public string? SelectedProvinceDisplayName { get; set; }

    public string? SelectedWardDisplayName { get; set; }

    public string? SelectedJobCategoryDisplayName { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public bool CanDelete { get; set; }

    public Guid DefaultProvinceId { get; set; }

    public string DefaultProvinceDisplayName { get; set; } = "";

    private readonly IJobsAppService _jobsAppService;

    private readonly IAuthorizationService _authorizationService;

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    private readonly IProvincesAppService _provincesAppService;

    public EditModel(
        IJobsAppService jobsAppService,
        IAuthorizationService authorizationService,
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer,
        IProvincesAppService provincesAppService)
    {
        _jobsAppService = jobsAppService;
        _authorizationService = authorizationService;
        _masterDataLocalizer = masterDataLocalizer;
        _provincesAppService = provincesAppService;
    }

    public async Task OnGetAsync()
    {
        FillEnumLookups();

        var dto = await _jobsAppService.GetWithNavigationPropertiesAsync(Id);
        Job = ObjectMapper.Map<JobDto, JobUpdatePageViewModel>(dto.Job);
        SelectedJobCategoryDisplayName = dto.JobCategory?.Name;
        LastUpdatedAt = dto.Job.LastModificationTime ?? dto.Job.CreationTime;
        CanDelete = await _authorizationService.IsGrantedAsync(MasterDataServicePermissions.Jobs.Delete);

        var prov = await KhHubDefaultProvinceAddress.TryGetAsync(_provincesAppService);
        if (prov != null)
        {
            DefaultProvinceId = prov.Value.Id;
            DefaultProvinceDisplayName = prov.Value.Name;
            Job.ProvinceId = prov.Value.Id;
            SelectedProvinceDisplayName = prov.Value.Name;

            if (dto.Job.ProvinceId != prov.Value.Id)
            {
                Job.WardId = Guid.Empty;
                SelectedWardDisplayName = null;
            }
            else
            {
                SelectedWardDisplayName = dto.Ward?.Name;
            }
        }
        else
        {
            SelectedProvinceDisplayName = dto.Province?.Name;
            SelectedWardDisplayName = dto.Ward?.Name;
        }
    }

    private void FillEnumLookups()
    {
        EmploymentTypeList = Enum.GetValues<EmploymentType>()
            .Select(x => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(EmploymentType)}.{(int)x}"].Value, ((int)x).ToString()))
            .ToList();

        WorkModeList = Enum.GetValues<WorkMode>()
            .Select(x => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(WorkMode)}.{(int)x}"].Value, ((int)x).ToString()))
            .ToList();

        ExperienceLevelList = Enum.GetValues<ExperienceLevel>()
            .Select(x => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(ExperienceLevel)}.{(int)x}"].Value, ((int)x).ToString()))
            .ToList();

        JobStatusList = Enum.GetValues<JobStatus>()
            .Select(x => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(JobStatus)}.{(int)x}"].Value, ((int)x).ToString()))
            .ToList();
    }
}

public class JobUpdatePageViewModel : JobUpdateDto
{
}
