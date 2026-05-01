using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Articles;

[Authorize(MasterDataServicePermissions.Articles.Edit)]
public class EditModel : AbpPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public ArticleUpdatePageViewModel Article { get; set; } = new();

    public List<SelectListItem> ArticleTypeList { get; set; } = [];

    public List<SelectListItem> ArticleStatusList { get; set; } = [];

    public string? SelectedArticleCategoryDisplayName { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public bool CanDelete { get; set; }

    private readonly IArticlesAppService _articlesAppService;

    private readonly IAuthorizationService _authorizationService;

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public EditModel(
        IArticlesAppService articlesAppService,
        IAuthorizationService authorizationService,
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _articlesAppService = articlesAppService;
        _authorizationService = authorizationService;
        _masterDataLocalizer = masterDataLocalizer;
    }

    public async Task OnGetAsync()
    {
        FillEnumLookups();

        var dto = await _articlesAppService.GetWithNavigationPropertiesAsync(Id);
        Article = ObjectMapper.Map<ArticleDto, ArticleUpdatePageViewModel>(dto.Article);
        SelectedArticleCategoryDisplayName = dto.ArticleCategory?.Name;
        LastUpdatedAt = dto.Article.LastModificationTime ?? dto.Article.CreationTime;
        CanDelete = await _authorizationService.IsGrantedAsync(MasterDataServicePermissions.Articles.Delete);
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

public class ArticleUpdatePageViewModel : ArticleUpdateDto
{
}
