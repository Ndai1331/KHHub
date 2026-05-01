using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;

namespace KHHub.Web.Pages.MediaFiles;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public MediaFileUpdateViewModel MediaFile { get; set; }

    protected IMediaFilesAppService _mediaFilesAppService;

    public EditModalModelBase(IMediaFilesAppService mediaFilesAppService)
    {
        _mediaFilesAppService = mediaFilesAppService;
        MediaFile = new();
    }

    public virtual async Task OnGetAsync()
    {
        var mediaFile = await _mediaFilesAppService.GetAsync(Id);
        MediaFile = ObjectMapper.Map<MediaFileDto, MediaFileUpdateViewModel>(mediaFile);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _mediaFilesAppService.UpdateAsync(Id, ObjectMapper.Map<MediaFileUpdateViewModel, MediaFileUpdateDto>(MediaFile));
        return NoContent();
    }
}

public class MediaFileUpdateViewModel : MediaFileUpdateDto
{
}