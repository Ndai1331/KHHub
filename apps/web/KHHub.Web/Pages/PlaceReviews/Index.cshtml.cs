using KHHub.MasterDataService.Entities.PlaceReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.PlaceReviews;

public abstract class IndexModelBase : AbpPageModel
{
    public Guid? PlaceIdFilter { get; set; }

    public List<SelectListItem> PlaceReviewStatusList { get; set; } = [];

    public PlaceReviewStatus? StatusFilter { get; set; }

    protected IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public IndexModelBase(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _masterDataLocalizer = masterDataLocalizer;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceReviewStatusList =
        [
            new SelectListItem(_masterDataLocalizer["All"].Value, string.Empty),
        ];
        PlaceReviewStatusList.AddRange(
            Enum.GetValues<PlaceReviewStatus>()
                .Select(s => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(PlaceReviewStatus)}.{(int)s}"].Value, ((int)s).ToString())));

        await Task.CompletedTask;
    }
}
