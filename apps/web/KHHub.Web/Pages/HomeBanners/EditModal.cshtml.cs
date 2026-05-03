using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;

namespace KHHub.Web.Pages.HomeBanners;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public HomeBannerUpdateViewModel HomeBanner { get; set; }

    protected IHomeBannersAppService _homeBannersAppService;

    public EditModalModelBase(IHomeBannersAppService homeBannersAppService)
    {
        _homeBannersAppService = homeBannersAppService;
        HomeBanner = new();
    }

    public virtual async Task OnGetAsync()
    {
        var homeBanner = await _homeBannersAppService.GetAsync(Id);
        HomeBanner = ObjectMapper.Map<HomeBannerDto, HomeBannerUpdateViewModel>(homeBanner);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _homeBannersAppService.UpdateAsync(Id, ObjectMapper.Map<HomeBannerUpdateViewModel, HomeBannerUpdateDto>(HomeBanner));
        return NoContent();
    }
}

public class HomeBannerUpdateViewModel : HomeBannerUpdateDto
{
}