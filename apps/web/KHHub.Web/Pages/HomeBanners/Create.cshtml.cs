using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.HomeBanners;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.HomeBanners;

[Authorize(MasterDataServicePermissions.HomeBanners.Create)]
public class CreateModel : AbpPageModel
{
    [BindProperty]
    public HomeBannerCreateViewModel HomeBanner { get; set; } = new();

    public List<SelectListItem> TargetTypeList { get; set; } = [];

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public CreateModel(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _masterDataLocalizer = masterDataLocalizer;
    }

    public Task OnGetAsync()
    {
        HomeBanner = new HomeBannerCreateViewModel
        {
            SortOrder = 1,
            IsActive = true,
        };
        FillTargetTypeList();
        return Task.CompletedTask;
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
