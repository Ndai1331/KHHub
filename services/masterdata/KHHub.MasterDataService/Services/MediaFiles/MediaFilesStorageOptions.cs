namespace KHHub.MasterDataService.Services.MediaFiles;

public class MediaFilesStorageOptions
{
    public const string SectionName = "MediaFilesStorage";

    /// <summary>
    /// Public base URL to build file URLs (e.g. https://cdn.example.com/bucket-name without trailing slash).
    /// </summary>
    public string PublicBaseUrl { get; set; } = "http://localhost:9000/khhub-articles";

    /// <summary>
    /// Max upload size per file (bytes).
    /// </summary>
    public long MaxUploadBytes { get; set; } = 104_857_600;

    /// <summary>
    /// When true (default), explorer list replaces the response <c>Url</c> with a temporary MinIO presigned GET URL.
    /// Use false only if the bucket allows anonymous reads or CDN serves blobs without signatures.
    /// </summary>
    public bool UsePresignedReadUrls { get; set; } = true;

    /// <summary>
    /// Presigned URL validity in seconds (clamped server-side).
    /// </summary>
    public int PresignedReadExpirySeconds { get; set; } = 3600;

    /// <summary>
    /// Optional Host:Port (or bracketed IPv6 <c>[::1]:9000</c>) used when building presigned URLs.
    /// Overrides <c>BlobStoring:Minio:EndPoint</c> when backend must advertise a browser-reachable address.
    /// </summary>
    public string? PresignPublicEndpoint { get; set; }
}
