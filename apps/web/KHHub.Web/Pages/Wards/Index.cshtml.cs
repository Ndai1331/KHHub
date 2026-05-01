using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Wards;

namespace KHHub.Web.Pages.Wards;

public abstract class IndexModelBase : AbpPageModel
{
    public string? CodeFilter { get; set; }

    public string? NameFilter { get; set; }

    public Guid? ProvinceIdFilter { get; set; }

    protected IWardsAppService _wardsAppService;

    public IndexModelBase(IWardsAppService wardsAppService)
    {
        _wardsAppService = wardsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}
