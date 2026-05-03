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

    /// <summary>
    /// Builds a relative public path for CMS fields (no host, no presign params).
    /// Example: PublicBaseUrl http://127.0.0.1:9000/khhub-articles + key host/a.png → /khhub-articles/host/a.png
    /// </summary>
    public static string? BuildStablePublicPath(string? publicBaseUrl, string? blobStorageKey)
    {
        if (string.IsNullOrWhiteSpace(blobStorageKey))
        {
            return null;
        }

        var key = blobStorageKey.Replace('\\', '/').TrimStart('/');
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(publicBaseUrl))
        {
            return "/" + key;
        }

        var trimmed = publicBaseUrl.Trim();
        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var absolute))
        {
            var basePath = absolute.AbsolutePath.TrimEnd('/');
            return string.IsNullOrEmpty(basePath) ? "/" + key : $"{basePath}/{key}";
        }

        var prefix = trimmed.TrimEnd('/');
        if (!prefix.StartsWith('/'))
        {
            prefix = "/" + prefix;
        }

        return $"{prefix}/{key}";
    }

    /// <summary>
    /// Maps a persisted stable public path back to the blob object key (e.g. <c>host/guid.png</c>).
    /// Supports paths from <see cref="BuildStablePublicPath"/> with or without bucket prefix in the path.
    /// </summary>
    public static bool TryGetBlobKeyFromStablePublicPath(string? publicBaseUrl, string? stablePublicPath, out string? objectKey)
    {
        objectKey = null;
        var path = (stablePublicPath ?? string.Empty).Trim();
        if (path.Length == 0 || path[0] != '/')
        {
            return false;
        }

        var baseRaw = (publicBaseUrl ?? string.Empty).Trim();
        if (baseRaw.Length > 0 && Uri.TryCreate(baseRaw, UriKind.Absolute, out var abs))
        {
            var urlPrefix = abs.AbsolutePath.TrimEnd('/');
            if (urlPrefix.Length > 0 &&
                path.Length > urlPrefix.Length + 1 &&
                path.StartsWith(urlPrefix + "/", StringComparison.Ordinal))
            {
                objectKey = path[(urlPrefix.Length + 1)..].TrimStart('/');
                return objectKey.Length > 0;
            }
        }
        else if (baseRaw.Length > 0)
        {
            var relPrefix = "/" + baseRaw.Trim().Trim('/').TrimStart('/');
            if (path.Length > relPrefix.Length + 1 && path.StartsWith(relPrefix + "/", StringComparison.Ordinal))
            {
                objectKey = path[(relPrefix.Length + 1)..].TrimStart('/');
                return objectKey.Length > 0;
            }
        }

        // Paths like "/host/..." when PublicBaseUrl was not configured at save time
        var tail = path.TrimStart('/');
        if (tail.StartsWith("host/", StringComparison.OrdinalIgnoreCase) ||
            tail.StartsWith("tenants/", StringComparison.OrdinalIgnoreCase))
        {
            objectKey = tail;
            return true;
        }

        return false;
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
