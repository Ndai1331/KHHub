using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Provinces;

namespace KHHub.Web.Pages.Provinces;

public abstract class IndexModelBase : AbpPageModel
{
    public string? CodeFilter { get; set; }

    public string? NameFilter { get; set; }

    protected IProvincesAppService _provincesAppService;

    public IndexModelBase(IProvincesAppService provincesAppService)
    {
        _provincesAppService = provincesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}
