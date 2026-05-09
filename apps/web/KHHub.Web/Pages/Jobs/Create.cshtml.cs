using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Services.Provinces;
using KHHub.Web.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Jobs;

[Authorize(MasterDataServicePermissions.Jobs.Create)]
public class CreateModel : AbpPageModel
{
    [BindProperty]
    public JobCreatePageViewModel Job { get; set; } = new();

    public List<SelectListItem> EmploymentTypeList { get; set; } = [];

    public List<SelectListItem> WorkModeList { get; set; } = [];

    public List<SelectListItem> ExperienceLevelList { get; set; } = [];

    public List<SelectListItem> JobStatusList { get; set; } = [];

    /// <summary>Resolved province Id for code <see cref="KhHubDefaultProvinceAddress.DefaultProvinceCode"/> (ward lookup + hidden field).</summary>
    public Guid DefaultProvinceId { get; set; }

    public string DefaultProvinceDisplayName { get; set; } = "";

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    private readonly IProvincesAppService _provincesAppService;

    public CreateModel(
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer,
        IProvincesAppService provincesAppService)
    {
        _masterDataLocalizer = masterDataLocalizer;
        _provincesAppService = provincesAppService;
    }

    public async Task OnGetAsync()
    {
        Job.PublishedAt = Clock.Now;
        Job.SalaryCurrency = "VND";
        FillEnumLookups();

        var prov = await KhHubDefaultProvinceAddress.TryGetAsync(_provincesAppService);
        if (prov != null)
        {
            DefaultProvinceId = prov.Value.Id;
            DefaultProvinceDisplayName = prov.Value.Name;
            Job.ProvinceId = prov.Value.Id;
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

public class JobCreatePageViewModel : JobCreateDto
{
}
