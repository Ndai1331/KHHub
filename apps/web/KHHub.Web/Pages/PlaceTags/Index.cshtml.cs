using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.PlaceTags;

public abstract class IndexModelBase : AbpPageModel
{
    public virtual Task OnGetAsync()
    {
        return Task.CompletedTask;
    }
}
