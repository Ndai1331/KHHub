using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.IntegrationServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.Content;

namespace KHHub.Web.Pages.HomeBanners;

[Authorize]
public class MediaUploadModel : AbpPageModel
{
    private static readonly string[] AllowedExtensions =
        [".jpg", ".jpeg", ".png", ".gif", ".webp"];

    private const long MaxBytes = 10 * 1024 * 1024;

    private readonly IArticleMediaIntegrationService _articleMediaIntegrationService;

    public MediaUploadModel(IArticleMediaIntegrationService articleMediaIntegrationService)
    {
        _articleMediaIntegrationService = articleMediaIntegrationService;
    }

    public IActionResult OnGet()
    {
        return NotFound();
    }

    public async Task<IActionResult> OnPostAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest(new { error = "No file uploaded." });
        }

        if (file.Length > MaxBytes)
        {
            return BadRequest(new { error = "File is too large." });
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
        {
            return BadRequest(new { error = "File type is not allowed." });
        }

        await using var readStream = file.OpenReadStream();
        using var ms = new MemoryStream();
        await readStream.CopyToAsync(ms);
        ms.Position = 0;

        var remote = new RemoteStreamContent(ms, file.FileName, file.ContentType ?? "application/octet-stream");

        var result = await _articleMediaIntegrationService.UploadImageAsync(remote);
        var url = Url.Page("/Articles/ArticleMediaFile", null, new { name = result.Name })!;
        return new JsonResult(new { url });
    }
}
