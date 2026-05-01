using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.BlobContainers;
using KHHub.MasterDataService.IntegrationServices;
using KHHub.MasterDataService.Services.Dtos.ArticleMedia;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;
using Volo.Abp.Content;

namespace KHHub.MasterDataService.IntegrationServices;

[Authorize]
public class ArticleMediaIntegrationService : ApplicationService, IArticleMediaIntegrationService
{
    private static readonly HashSet<string> AllowedExtensions =
        [".jpg", ".jpeg", ".png", ".gif", ".webp"];

    private const long MaxBytes = 10 * 1024 * 1024;

    private readonly IBlobContainer<ArticleMediaBlobContainer> _blobContainer;

    public ArticleMediaIntegrationService(IBlobContainer<ArticleMediaBlobContainer> blobContainer)
    {
        _blobContainer = blobContainer;
    }

    public async Task<ArticleMediaUploadResultDto> UploadImageAsync(IRemoteStreamContent content)
    {
        ArgumentNullException.ThrowIfNull(content);

        var originalName = content.FileName ?? "file";
        var ext = Path.GetExtension(originalName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !AllowedExtensions.Contains(ext))
        {
            throw new Volo.Abp.UserFriendlyException(L["UploadFailedMessage"]);
        }

        if (content.ContentLength.HasValue && content.ContentLength.Value > MaxBytes)
        {
            throw new Volo.Abp.UserFriendlyException(L["UploadFailedMessage"]);
        }

        await using var input = content.GetStream();
        using var buffer = new MemoryStream();
        await CopyWithSizeLimitAsync(input, buffer);

        buffer.Position = 0;
        var blobName = GuidGenerator.Create().ToString("N") + ext;
        await _blobContainer.SaveAsync(blobName, buffer, overrideExisting: true);

        var contentType = string.IsNullOrWhiteSpace(content.ContentType)
            ? GuessContentType(ext)
            : content.ContentType!;

        return new ArticleMediaUploadResultDto { Name = blobName, ContentType = contentType };
    }

    public async Task<IRemoteStreamContent> GetAsync(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        if (name.AsSpan().ContainsAny(['/', '\\']))
        {
            throw new Volo.Abp.UserFriendlyException(L["UploadFailedMessage"]);
        }

        await using var input = await _blobContainer.GetAsync(name);
        var ms = new MemoryStream();
        await input.CopyToAsync(ms);
        ms.Position = 0;
        var ext = Path.GetExtension(name).ToLowerInvariant();
        var contentType = GuessContentType(ext);
        return new RemoteStreamContent(ms, name, contentType);
    }

    private async Task CopyWithSizeLimitAsync(Stream source, MemoryStream destination)
    {
        var buf = new byte[8192];
        long total = 0;
        int read;
        while ((read = await source.ReadAsync(buf.AsMemory(0, buf.Length))) > 0)
        {
            total += read;
            if (total > MaxBytes)
            {
                throw new Volo.Abp.UserFriendlyException(L["UploadFailedMessage"]);
            }

            await destination.WriteAsync(buf.AsMemory(0, read));
        }
    }

    private static string GuessContentType(string ext)
    {
        return ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            _ => "application/octet-stream"
        };
    }
}
