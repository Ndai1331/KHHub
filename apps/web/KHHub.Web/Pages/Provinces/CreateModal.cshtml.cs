using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.Provinces;
using KHHub.MasterDataService.Services.Dtos.Provinces;

namespace KHHub.Web.Pages.Provinces;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public ProvinceCreateViewModel Province { get; set; }

    protected IProvincesAppService _provincesAppService;

    public CreateModalModelBase(IProvincesAppService provincesAppService)
    {
        _provincesAppService = provincesAppService;
        Province = new();
    }

    public virtual async Task OnGetAsync()
    {
        Province = new ProvinceCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _provincesAppService.CreateAsync(ObjectMapper.Map<ProvinceCreateViewModel, ProvinceCreateDto>(Province));
        return NoContent();
    }
}

public class ProvinceCreateViewModel : ProvinceCreateDto
{
}