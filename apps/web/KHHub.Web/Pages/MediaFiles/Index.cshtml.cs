using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;

namespace KHHub.Web.Pages.MediaFiles;

public abstract class IndexModelBase : AbpPageModel
{
    public string? FileNameFilter { get; set; }

    public string? OriginalFileNameFilter { get; set; }

    public string? ExtensionFilter { get; set; }

    public string? ContentTypeFilter { get; set; }

    public string? StorageProviderFilter { get; set; }

    public string? BucketFilter { get; set; }

    public string? FolderFilter { get; set; }

    public string? PathFilter { get; set; }

    public string? UrlFilter { get; set; }

    public string? ChecksumFilter { get; set; }

    public int? WidthFilterMin { get; set; }

    public int? WidthFilterMax { get; set; }

    public int? HeightFilterMin { get; set; }

    public int? HeightFilterMax { get; set; }

    public FileType? FileTypeFilter { get; set; }

    public FileStatus? StatusFilter { get; set; }

    /// <summary>Embedded media library picker (e.g. iframe from Article/Place forms).</summary>
    [BindProperty(SupportsGet = true)]
    public bool Picker { get; set; }

    protected IMediaFilesAppService _mediaFilesAppService;

    public IndexModelBase(IMediaFilesAppService mediaFilesAppService)
    {
        _mediaFilesAppService = mediaFilesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}