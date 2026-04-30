using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.Provinces;
using KHHub.MasterDataService.Services.Dtos.Provinces;

namespace KHHub.Web.Pages.Provinces;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ProvinceUpdateViewModel Province { get; set; }

    protected IProvincesAppService _provincesAppService;

    public EditModalModelBase(IProvincesAppService provincesAppService)
    {
        _provincesAppService = provincesAppService;
        Province = new();
    }

    public virtual async Task OnGetAsync()
    {
        var province = await _provincesAppService.GetAsync(Id);
        Province = ObjectMapper.Map<ProvinceDto, ProvinceUpdateViewModel>(province);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _provincesAppService.UpdateAsync(Id, ObjectMapper.Map<ProvinceUpdateViewModel, ProvinceUpdateDto>(Province));
        return NoContent();
    }
}

public class ProvinceUpdateViewModel : ProvinceUpdateDto
{
}