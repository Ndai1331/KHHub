using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Content;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.MediaFiles;

namespace KHHub.Web.Pages.MediaFiles;

[Authorize]
public class ExplorerUploadModel : AbpPageModel
{
    private readonly IMediaFilesAppService _mediaFilesAppService;
    private readonly IPermissionChecker _permissionChecker;

    public ExplorerUploadModel(
        IMediaFilesAppService mediaFilesAppService,
        IPermissionChecker permissionChecker)
    {
        _mediaFilesAppService = mediaFilesAppService;
        _permissionChecker = permissionChecker;
    }

    public IActionResult OnGet()
    {
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? file, string? parentFolderPath)
    {
        if (!await _permissionChecker.IsGrantedAsync(MasterDataServicePermissions.MediaFiles.Create))
        {
            return Forbid();
        }

        if (file == null || file.Length == 0)
        {
            return BadRequest(new { message = L["MediaExplorer:UploadInvalidFile"].Value });
        }

        await using var readStream = file.OpenReadStream();
        using var ms = new MemoryStream();
        await readStream.CopyToAsync(ms);
        ms.Position = 0;

        var remote = new RemoteStreamContent(ms, file.FileName, file.ContentType ?? "application/octet-stream");

        var dto = await _mediaFilesAppService.UploadExplorerFileAsync(parentFolderPath, remote);
        return new JsonResult(dto);
    }
}
