using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;
using Volo.Abp.DependencyInjection;

namespace KHHub.MasterDataService.Services.MediaFiles;

/// <summary>
/// Builds temporary GET URLs so browsers can load private MinIO/S3 objects (thumbnails and preview modal).
/// </summary>
public interface IMinioBlobReadUrlSigner : ISingletonDependency
{
    Task<string?> TryGetPresignedReadUrlAsync(string bucketName, string objectKey, int expirySeconds);
}

public sealed class MinioBlobReadUrlSigner : IMinioBlobReadUrlSigner
{
    private readonly ILogger<MinioBlobReadUrlSigner> _logger;
    private readonly IConfiguration _configuration;
    private readonly MediaFilesStorageOptions _mediaOptions;
    private readonly IMinioClient? _client;

    public MinioBlobReadUrlSigner(
        ILogger<MinioBlobReadUrlSigner> logger,
        IConfiguration configuration,
        IOptions<MediaFilesStorageOptions> mediaOptions)
    {
        _logger = logger;
        _configuration = configuration;
        _mediaOptions = mediaOptions.Value;
        _client = TryCreateClient(configuration, _mediaOptions, logger);
    }

    /// <inheritdoc />
    public async Task<string?> TryGetPresignedReadUrlAsync(string bucketName, string objectKey, int expirySeconds)
    {
        if (!_mediaOptions.UsePresignedReadUrls || _client == null)
        {
            return null;
        }

        if (string.IsNullOrWhiteSpace(objectKey))
        {
            return null;
        }

        var bucket = (bucketName ?? "").Trim();
        if (bucket.Length == 0)
        {
            bucket = (_configuration["BlobStoring:Minio:BucketName"] ?? "").Trim();
        }

        if (bucket.Length == 0)
        {
            return null;
        }

        var key = objectKey.TrimStart('/').Trim();
        var secs = Math.Clamp(expirySeconds, 60, 86400);

        try
        {
            var args = new PresignedGetObjectArgs()
                .WithBucket(bucket)
                .WithObject(key)
                .WithExpiry(secs);

            var url = await _client.PresignedGetObjectAsync(args).ConfigureAwait(false);
            return string.IsNullOrWhiteSpace(url) ? null : url;
        }
        catch (Exception ex)
        {
            _logger.LogWarning(
                ex,
                "MinIO presigned GET failed for bucket {Bucket}, key {Key}.",
                bucket,
                key);
            return null;
        }
    }

    private static IMinioClient? TryCreateClient(
        IConfiguration configuration,
        MediaFilesStorageOptions mediaOptions,
        ILogger logger)
    {
        var sec = configuration.GetSection("BlobStoring:Minio");
        var accessKey = sec["AccessKey"];
        var secretKey = sec["SecretKey"];

        var raw =
            string.IsNullOrWhiteSpace(mediaOptions.PresignPublicEndpoint)
                ? sec["EndPoint"]
                : mediaOptions.PresignPublicEndpoint;

        raw = StripScheme(raw);

        if (string.IsNullOrWhiteSpace(raw)
            || string.IsNullOrWhiteSpace(accessKey)
            || string.IsNullOrWhiteSpace(secretKey))
        {
            logger.LogWarning("MinIO BlobStoring config is incomplete; presigned reads are disabled.");
            return null;
        }

        if (!TryParseHostPort(raw, out var host, out var port))
        {
            logger.LogWarning(
                "MinIO endpoint '{Endpoint}' could not be parsed; presigned reads disabled.",
                raw);
            return null;
        }

        var withSsl = sec.GetValue("WithSSL", false);

        try
        {
            return new MinioClient()
                .WithEndpoint(host, port)
                .WithCredentials(accessKey!, secretKey!)
                .WithSSL(withSsl)
                .Build();
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Failed to build MinIO client; presigned reads disabled.");
            return null;
        }
    }

    private static string? StripScheme(string? endpoint)
    {
        if (string.IsNullOrWhiteSpace(endpoint))
        {
            return endpoint;
        }

        var raw = endpoint.Trim().Trim('/');
        foreach (var p in new[] { "https://", "http://" })
        {
            if (raw.StartsWith(p, StringComparison.OrdinalIgnoreCase))
            {
                raw = raw[p.Length..].Trim().Trim('/');
                break;
            }
        }

        return raw;
    }

    /// <summary>
    /// Supports <c>host:port</c>, <c>host</c> (defaults to 9000), and bracketed IPv6 <c>[::1]:9000</c>.
    /// </summary>
    private static bool TryParseHostPort(string hostPort, out string host, out int port)
    {
        host = "";
        port = 0;
        hostPort = hostPort.Trim();

        if (hostPort.StartsWith("[", System.StringComparison.Ordinal))
        {
            var close = hostPort.IndexOf(']', 1);
            if (close <= 1)
            {
                return false;
            }

            host = hostPort.Substring(1, close - 1).Trim();

            var tail = close + 1 < hostPort.Length ? hostPort[(close + 1)..] : "";
            if (tail.Length > 0 && tail[0] == ':')
            {
                return int.TryParse(
                        tail.AsSpan(1),
                        NumberStyles.Integer,
                        CultureInfo.InvariantCulture,
                        out port)
                    && host.Length > 0;
            }

            port = 9000;
            return host.Length > 0;
        }

        var colon = hostPort.LastIndexOf(':');
        if (colon > 0 && colon < hostPort.Length - 1 &&
            int.TryParse(
                hostPort.AsSpan(colon + 1),
                NumberStyles.Integer,
                CultureInfo.InvariantCulture,
                out var p))
        {
            host = hostPort[..colon].Trim();
            port = p;
            return host.Length > 0;
        }

        host = hostPort.Trim();
        port = 9000;
        return host.Length > 0;
    }
}
