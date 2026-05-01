using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.PlaceViews;

public abstract class IndexModelBase : AbpPageModel
{
    public Guid? PlaceIdFilter { get; set; }

    public virtual Task OnGetAsync()
    {
        return Task.CompletedTask;
    }
}
