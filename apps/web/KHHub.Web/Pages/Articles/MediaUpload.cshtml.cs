using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Articles;

[Authorize]
public class MediaUploadModel : AbpPageModel
{
    private static readonly string[] AllowedExtensions =
        [".jpg", ".jpeg", ".png", ".gif", ".webp"];

    private const long MaxBytes = 10 * 1024 * 1024;

    private readonly IWebHostEnvironment _webHostEnvironment;

    public MediaUploadModel(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
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

        var relativeDir = Path.Combine("uploads", "articles");
        var physicalDir = Path.Combine(_webHostEnvironment.WebRootPath, relativeDir);
        Directory.CreateDirectory(physicalDir);

        var fileName = GuidGenerator.Create().ToString("N") + ext;
        var physicalPath = Path.Combine(physicalDir, fileName);

        await using (var stream = System.IO.File.Create(physicalPath))
        {
            await file.CopyToAsync(stream);
        }

        var url = "/" + relativeDir.Replace(Path.DirectorySeparatorChar, '/') + "/" + fileName;
        return new JsonResult(new { url });
    }
}
