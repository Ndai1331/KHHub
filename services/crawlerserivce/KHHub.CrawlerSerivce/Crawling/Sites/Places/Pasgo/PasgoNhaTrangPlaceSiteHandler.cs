using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Models;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling.Sites.Places.Pasgo;

/// <summary>
/// Pasgo Nha Trang listing + place detail (heuristic URLs + JSON-LD LocalBusiness when present).
/// </summary>
public class PasgoNhaTrangPlaceSiteHandler : IPlaceDirectorySiteHandler, ITransientDependency
{
    public const string SiteKeyConst = "PasgoNhaTrang";

    private static readonly Regex LdJsonScriptRegex = new(
        @"<script\s+type=[""']application/ld\+json[""'][^>]*>(.*?)</script>",
        RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    public string SiteKey => SiteKeyConst;

    public bool SupportsListingPage(Uri uri)
    {
        return IsPasgoHost(uri.Host)
               && uri.AbsolutePath.Contains("/nha-trang/", StringComparison.OrdinalIgnoreCase)
               && !IsProbablePlaceDetailPath(uri.AbsolutePath);
    }

    public bool SupportsPlaceDetailPage(Uri uri)
    {
        return IsPasgoHost(uri.Host) && IsProbablePlaceDetailPath(uri.AbsolutePath);
    }

    public Uri BuildListingPageUri(Uri seedListingUri, int pageNumber)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }

        var b = new UriBuilder(seedListingUri) { Scheme = Uri.UriSchemeHttps, Host = "www.pasgo.vn" };
        if (pageNumber <= 1)
        {
            return b.Uri;
        }

        b.Query = $"page={pageNumber}";
        return b.Uri;
    }

    public Task<IReadOnlyList<CrawledPlaceListItem>> ParseListingHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var list = new List<CrawledPlaceListItem>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var anchors = doc.DocumentNode.SelectNodes("//a[contains(@href,'/nha-trang/')]");
        if (anchors == null)
        {
            return Task.FromResult<IReadOnlyList<CrawledPlaceListItem>>(list);
        }

        foreach (var a in anchors)
        {
            var href = a.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(href))
            {
                continue;
            }

            var absolute = new Uri(documentUri, href);
            if (!IsPasgoHost(absolute.Host) || !IsProbablePlaceDetailPath(absolute.AbsolutePath))
            {
                continue;
            }

            var url = NormalizeUrl(absolute);
            if (!seen.Add(url))
            {
                continue;
            }

            var name = WebUtility.HtmlDecode(a.InnerText.Trim());
            if (name.Length < 2)
            {
                continue;
            }

            list.Add(new CrawledPlaceListItem
            {
                SourceAbsoluteUrl = url,
                Name = name
            });
        }

        return Task.FromResult<IReadOnlyList<CrawledPlaceListItem>>(list);
    }

    public Task<CrawledPlaceDetailPatch> ParsePlaceDetailHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var patch = new CrawledPlaceDetailPatch();

        foreach (Match m in LdJsonScriptRegex.Matches(html))
        {
            if (TryLdJsonLocalBusiness(m.Groups[1].Value, patch))
            {
                break;
            }
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        if (string.IsNullOrWhiteSpace(patch.Description))
        {
            var intro = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'intro')]") ??
                        doc.DocumentNode.SelectSingleNode("//*[contains(@class,'description')]");
            patch.Description ??= WebUtility.HtmlDecode(intro?.InnerText.Trim() ?? string.Empty);
        }

        patch.ShortDescription ??= GetMeta(doc, "og:description");
        if (string.IsNullOrWhiteSpace(patch.Description))
        {
            patch.Description = patch.ShortDescription;
        }

        return Task.FromResult(patch);
    }

    private static bool TryLdJsonLocalBusiness(string raw, CrawledPlaceDetailPatch patch)
    {
        try
        {
            var node = JsonNode.Parse(raw) as JsonObject;
            if (node == null)
            {
                return false;
            }

            var types = node["@type"]?.ToString() ?? string.Empty;
            if (!types.Contains("Restaurant", StringComparison.OrdinalIgnoreCase)
                && !types.Contains("LocalBusiness", StringComparison.OrdinalIgnoreCase)
                && !types.Contains("FoodEstablishment", StringComparison.OrdinalIgnoreCase)
                && node["@graph"] is JsonArray graph)
            {
                node = graph.OfType<JsonObject>().FirstOrDefault(o =>
                    (o["@type"]?.ToString().Contains("Restaurant", StringComparison.OrdinalIgnoreCase) ?? false)
                    || (o["@type"]?.ToString().Contains("LocalBusiness", StringComparison.OrdinalIgnoreCase) ?? false));
            }

            if (node == null)
            {
                return false;
            }

            patch.Address ??= node["address"] is JsonObject addr
                ? addr["streetAddress"]?.ToString()
                : node["address"]?.ToString();

            if (node["geo"] is JsonObject geo && geo["latitude"] != null && geo["longitude"] != null &&
                decimal.TryParse(geo["latitude"]!.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var lat)
                && decimal.TryParse(geo["longitude"]!.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var lng))
            {
                patch.Latitude = lat;
                patch.Longituded = lng;
            }

            patch.PhoneNumber ??= node["telephone"]?.ToString();
            patch.Website ??= node["url"]?.ToString();
            patch.CategoryLabel ??= node["name"]?.ToString();
            if (node["aggregateRating"] is JsonObject ar)
            {
                if (decimal.TryParse(ar["ratingValue"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var rv))
                {
                    patch.RatingAveraged = rv;
                }

                if (int.TryParse(ar["reviewCount"]?.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var rc))
                {
                    patch.ReviewCount = rc;
                }
            }

            patch.OpeningHours ??= node["openingHoursSpecification"]?.ToString();

            return true;
        }
        catch
        {
            return false;
        }
    }

    private static string? GetMeta(HtmlDocument doc, string property)
    {
        var n = doc.DocumentNode.SelectSingleNode($"//meta[@property='{property}']/@content")
                ?? doc.DocumentNode.SelectSingleNode($"//meta[@name='{property}']/@content");
        return WebUtility.HtmlDecode(n?.GetAttributeValue("content", null)?.Trim());
    }

    private static bool IsPasgoHost(string host)
    {
        return host.Equals("pasgo.vn", StringComparison.OrdinalIgnoreCase)
               || host.Equals("www.pasgo.vn", StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeUrl(Uri uri)
    {
        var b = new UriBuilder(uri) { Scheme = Uri.UriSchemeHttps, Fragment = string.Empty };
        return b.Uri.AbsoluteUri;
    }

    /// <summary>
    /// Detail paths typically have category + slug under <c>/nha-trang/</c>.
    /// </summary>
    private static bool IsProbablePlaceDetailPath(string path)
    {
        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length < 3)
        {
            return false;
        }

        if (!segments[0].Equals("nha-trang", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        return segments.Length >= 3;
    }
}
