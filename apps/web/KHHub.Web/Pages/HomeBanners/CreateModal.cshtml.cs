using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;

namespace KHHub.Web.Pages.HomeBanners;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public HomeBannerCreateViewModel HomeBanner { get; set; }

    protected IHomeBannersAppService _homeBannersAppService;

    public CreateModalModelBase(IHomeBannersAppService homeBannersAppService)
    {
        _homeBannersAppService = homeBannersAppService;
        HomeBanner = new();
    }

    public virtual async Task OnGetAsync()
    {
        HomeBanner = new HomeBannerCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _homeBannersAppService.CreateAsync(ObjectMapper.Map<HomeBannerCreateViewModel, HomeBannerCreateDto>(HomeBanner));
        return NoContent();
    }
}

public class HomeBannerCreateViewModel : HomeBannerCreateDto
{
}