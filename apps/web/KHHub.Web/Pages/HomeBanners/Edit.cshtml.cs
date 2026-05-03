using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.HomeBanners;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.HomeBanners;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.HomeBanners;

[Authorize(MasterDataServicePermissions.HomeBanners.Edit)]
public class EditModel : AbpPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public HomeBannerUpdateViewModel HomeBanner { get; set; } = new();

    public List<SelectListItem> TargetTypeList { get; set; } = [];

    public DateTime? LastUpdatedAt { get; set; }

    public bool CanDelete { get; set; }

    private readonly IHomeBannersAppService _homeBannersAppService;

    private readonly IAuthorizationService _authorizationService;

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public EditModel(
        IHomeBannersAppService homeBannersAppService,
        IAuthorizationService authorizationService,
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _homeBannersAppService = homeBannersAppService;
        _authorizationService = authorizationService;
        _masterDataLocalizer = masterDataLocalizer;
    }

    public async Task OnGetAsync()
    {
        FillTargetTypeList();

        var dto = await _homeBannersAppService.GetAsync(Id);
        HomeBanner = ObjectMapper.Map<HomeBannerDto, HomeBannerUpdateViewModel>(dto);
        LastUpdatedAt = dto.LastModificationTime ?? dto.CreationTime;
        CanDelete = await _authorizationService.IsGrantedAsync(MasterDataServicePermissions.HomeBanners.Delete);
    }

    private void FillTargetTypeList()
    {
        TargetTypeList =
        [
            new SelectListItem(_masterDataLocalizer["TargetTypeOptionalNone"].Value, string.Empty),
        ];
        TargetTypeList.AddRange(
            Enum.GetValues<TargetType>()
                .Select(t => new SelectListItem(
                    _masterDataLocalizer[$"Enum:{nameof(TargetType)}.{(int)t}"].Value,
                    ((int)t).ToString())));
    }
}
