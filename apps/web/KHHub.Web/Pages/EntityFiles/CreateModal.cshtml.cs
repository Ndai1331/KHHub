using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;

namespace KHHub.Web.Pages.EntityFiles;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public EntityFileCreateViewModel EntityFile { get; set; }

    protected IEntityFilesAppService _entityFilesAppService;

    public CreateModalModelBase(IEntityFilesAppService entityFilesAppService)
    {
        _entityFilesAppService = entityFilesAppService;
        EntityFile = new();
    }

    public virtual async Task OnGetAsync()
    {
        EntityFile = new EntityFileCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _entityFilesAppService.CreateAsync(ObjectMapper.Map<EntityFileCreateViewModel, EntityFileCreateDto>(EntityFile));
        return NoContent();
    }
}

public class EntityFileCreateViewModel : EntityFileCreateDto
{
}