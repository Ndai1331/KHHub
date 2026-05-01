using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;

namespace KHHub.Web.Pages.EntityFiles;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EntityFileUpdateViewModel EntityFile { get; set; }

    public MediaFileDto MediaFile { get; set; }

    protected IEntityFilesAppService _entityFilesAppService;

    public EditModalModelBase(IEntityFilesAppService entityFilesAppService)
    {
        _entityFilesAppService = entityFilesAppService;
        EntityFile = new();
    }

    public virtual async Task OnGetAsync()
    {
        var entityFileWithNavigationPropertiesDto = await _entityFilesAppService.GetWithNavigationPropertiesAsync(Id);
        EntityFile = ObjectMapper.Map<EntityFileDto, EntityFileUpdateViewModel>(entityFileWithNavigationPropertiesDto.EntityFile);
        MediaFile = entityFileWithNavigationPropertiesDto.MediaFile;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _entityFilesAppService.UpdateAsync(Id, ObjectMapper.Map<EntityFileUpdateViewModel, EntityFileUpdateDto>(EntityFile));
        return NoContent();
    }
}

public class EntityFileUpdateViewModel : EntityFileUpdateDto
{
}