using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;

namespace KHHub.Web.Pages.MediaFiles;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public MediaFileCreateViewModel MediaFile { get; set; }

    protected IMediaFilesAppService _mediaFilesAppService;

    public CreateModalModelBase(IMediaFilesAppService mediaFilesAppService)
    {
        _mediaFilesAppService = mediaFilesAppService;
        MediaFile = new();
    }

    public virtual async Task OnGetAsync()
    {
        MediaFile = new MediaFileCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _mediaFilesAppService.CreateAsync(ObjectMapper.Map<MediaFileCreateViewModel, MediaFileCreateDto>(MediaFile));
        return NoContent();
    }
}

public class MediaFileCreateViewModel : MediaFileCreateDto
{
}