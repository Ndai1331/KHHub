using System;
using System.IO;
using System.Text.RegularExpressions;
using KHHub.MasterDataService.Entities.MediaFiles;

namespace KHHub.MasterDataService.Services.MediaFiles;

public static class MediaExplorerPathHelper
{
    private static readonly Regex InvalidNameChars = new(@"[<>:/\\""|?*]", RegexOptions.Compiled);

    /// <summary>
    /// Normalizes logical folder path segment list (trim, unify slashes).
    /// Returns null when root folder.
    /// </summary>
    public static string? NormalizeParentFolderPath(string? path)
    {
        if (string.IsNullOrWhiteSpace(path))
        {
            return null;
        }

        var n = path!.Replace('\\', '/').Trim().Trim('/');
        return n.Length == 0 ? null : n;
    }

    public static void ValidateExplorerSegment(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Name is required.", nameof(name));
        }

        if (name.Contains('/') || name.Contains('\\'))
        {
            throw new ArgumentException("Name must not contain path separators.", nameof(name));
        }

        if (InvalidNameChars.IsMatch(name))
        {
            throw new ArgumentException("Name contains invalid characters.", nameof(name));
        }
    }

    public static string CombineLogicalChildPath(string? parentFolderNormalized, string validSegmentName)
    {
        return parentFolderNormalized == null ? validSegmentName : $"{parentFolderNormalized}/{validSegmentName}";
    }

    /// <summary>
    /// Absolute object key in the MinIO bucket. Must stay in sync with
    /// <c>DefaultMinioBlobNameCalculator</c> (prefixes <c>host/</c> or <c>tenants/&lt;Guid:D&gt;/</c>).
    /// </summary>
    /// <remarks>
    /// Pass only the explorer-relative path to <see cref="Volo.Abp.BlobStoring.IBlobContainer.SaveAsync"/>;
    /// MinIO provider will prepend the tenant segment itself.
    /// </remarks>
    public static string FormatMinioObjectKey(Guid? tenantId, string logicalExplorerPath)
    {
        var rel = logicalExplorerPath.Replace('\\', '/').Trim('/');

        return tenantId == null
            ? $"host/{rel}"
            : $"tenants/{tenantId.Value:D}/{rel}";
    }

    public static string CombinePublicUrl(string publicBaseUrl, string blobStorageKey)
    {
        publicBaseUrl = publicBaseUrl.TrimEnd('/');
        blobStorageKey = blobStorageKey.TrimStart('/');
        return $"{publicBaseUrl}/{blobStorageKey}";
    }

    public static FileType InferFileTypeFromExtension(string? extensionOrFileName)
    {
        var ext = Path.GetExtension(extensionOrFileName ?? string.Empty).ToLowerInvariant();
        if (ext.Length > 0 && ext[0] == '.')
        {
            ext = ext[1..];
        }

        if (IsImage(ext))
        {
            return FileType.Image;
        }

        return IsVideo(ext) ? FileType.Video : FileType.Document;
    }

    private static bool IsImage(string extNoDot) =>
        extNoDot is "jpg" or "jpeg" or "png" or "gif" or "webp" or "bmp" or "svg";

    private static bool IsVideo(string extNoDot) =>
        extNoDot is "mp4" or "mov" or "webm" or "mkv" or "avi" or "m4v";

    /// <summary>
    /// Match exactly one logical parent bucket (supports legacy rows with Folder null vs empty).
    /// </summary>
    public static bool IsUnderParentFolder(string? entityFolder, string? normalizedParentFolder)
    {
        if (normalizedParentFolder == null)
        {
            return string.IsNullOrEmpty(entityFolder);
        }

        return string.Equals(entityFolder, normalizedParentFolder, StringComparison.Ordinal);
    }
}
