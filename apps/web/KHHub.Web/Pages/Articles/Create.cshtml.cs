using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Articles;

[Authorize(MasterDataServicePermissions.Articles.Create)]
public class CreateModel : AbpPageModel
{
    [BindProperty]
    public ArticleCreatePageViewModel Article { get; set; } = new();

    public List<SelectListItem> ArticleTypeList { get; set; } = [];

    public List<SelectListItem> ArticleStatusList { get; set; } = [];

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public CreateModel(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _masterDataLocalizer = masterDataLocalizer;
    }

    public Task OnGetAsync()
    {
        Article.PublishedAt = Clock.Now;
        FillEnumLookups();
        return Task.CompletedTask;
    }

    private void FillEnumLookups()
    {
        ArticleTypeList = Enum.GetValues<ArticleType>()
            .Select(t => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(ArticleType)}.{(int)t}"].Value, ((int)t).ToString()))
            .ToList();

        ArticleStatusList = Enum.GetValues<ArticleStatus>()
            .Select(s => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(ArticleStatus)}.{(int)s}"].Value, ((int)s).ToString()))
            .ToList();
    }
}

public class ArticleCreatePageViewModel : ArticleCreateDto
{
}
