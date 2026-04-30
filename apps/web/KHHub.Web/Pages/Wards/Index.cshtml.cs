using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Wards;
using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.Wards;

public abstract class IndexModelBase : AbpPageModel
{
    public string? CodeFilter { get; set; }

    public string? NameFilter { get; set; }

    [SelectItems(nameof(ProvinceLookupList))]
    public Guid ProvinceIdFilter { get; set; }

    public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IWardsAppService _wardsAppService;

    public IndexModelBase(IWardsAppService wardsAppService)
    {
        _wardsAppService = wardsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        ProvinceLookupList.AddRange((await _wardsAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}