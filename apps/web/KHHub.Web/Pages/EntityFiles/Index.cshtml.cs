using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.EntityFiles;

public abstract class IndexModelBase : AbpPageModel
{
    public string? EntityTypeFilter { get; set; }

    public string? EntityIdFilter { get; set; }

    public string? CollectionFilter { get; set; }

    public int? SortOrderFilterMin { get; set; }

    public int? SortOrderFilterMax { get; set; }

    [SelectItems(nameof(IsPrimaryBoolFilterItems))]
    public string IsPrimaryFilter { get; set; }

    public List<SelectListItem> IsPrimaryBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(MediaFileLookupList))]
    public Guid MediaFileIdFilter { get; set; }

    public List<SelectListItem> MediaFileLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IEntityFilesAppService _entityFilesAppService;

    public IndexModelBase(IEntityFilesAppService entityFilesAppService)
    {
        _entityFilesAppService = entityFilesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        MediaFileLookupList.AddRange((await _entityFilesAppService.GetMediaFileLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}