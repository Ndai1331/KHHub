using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.ArticleViews;

public abstract class IndexModelBase : AbpPageModel
{
    public Guid? ArticleIdFilter { get; set; }

    public virtual Task OnGetAsync()
    {
        return Task.CompletedTask;
    }
}
