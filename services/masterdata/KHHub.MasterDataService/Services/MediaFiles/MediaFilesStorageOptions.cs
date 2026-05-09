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
    /// When true, explorer list replaces the response <c>Url</c> with a temporary MinIO presigned GET URL.
    /// Keep false because the MinIO bucket is public and browser previews can use stable public URLs directly.
    /// </summary>
    public bool UsePresignedReadUrls { get; set; } = false;

    /// <summary>
    /// Presigned URL validity in seconds (clamped server-side).
    /// </summary>
    public int PresignedReadExpirySeconds { get; set; } = 3600;

    /// <summary>
    /// Optional host (with optional scheme/port) used when building presigned URLs.
    /// Overrides <c>BlobStoring:Minio:EndPoint</c> when backend must advertise a browser-reachable address.
    /// Use <c>https://minio.example.com:443</c> when TLS terminates at nginx so SDK generates HTTPS presigned links.
    /// </summary>
    public string? PresignPublicEndpoint { get; set; }
}
