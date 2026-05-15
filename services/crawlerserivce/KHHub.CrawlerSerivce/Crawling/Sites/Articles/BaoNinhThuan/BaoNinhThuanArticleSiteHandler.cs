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
using KHHub.CrawlerSerivce.Crawling;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Models;

namespace KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoNinhThuan;

/// <summary>
/// Listing + detail for <see href="https://www.baoninhthuan.com.vn/">baoninhthuan.com.vn</see>.
/// Registered explicitly in <see cref="CrawlerSiteHandlersServiceCollectionExtensions.AddCrawlerArticleNewsSiteHandlers"/>.
/// </summary>
public class BaoNinhThuanArticleSiteHandler : IArticleNewsSiteHandler
{
    public const string SiteKeyConst = "BaoNinhThuan";

    private static readonly Regex LdJsonScriptRegex = new(
        @"<script\s+type=[""']application/ld\+json[""'][^>]*>(.*?)</script>",
        RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    public string SiteKey => SiteKeyConst;

    public bool SupportsListingPage(Uri uri)
    {
        return IsHost(uri.Host);
    }

    public bool SupportsArticleDetailPage(Uri uri)
    {
        return IsHost(uri.Host) && LooksLikeArticlePath(uri.AbsolutePath);
    }

    public Uri BuildListingPageUri(Uri seedListingUri, int pageNumber)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }

        var b = new UriBuilder(seedListingUri)
        {
            Scheme = Uri.UriSchemeHttps,
            Host = NormalizeHost(seedListingUri.Host)
        };

        if (pageNumber <= 1)
        {
            return b.Uri;
        }

        b.Query = $"page={pageNumber}";
        return b.Uri;
    }

    public Task<IReadOnlyList<CrawledArticleListItem>> ParseListingHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var anchors = doc.DocumentNode.SelectNodes("//a[@href]");
        var list = new List<CrawledArticleListItem>();
        if (anchors == null)
        {
            return Task.FromResult<IReadOnlyList<CrawledArticleListItem>>(list);
        }

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        foreach (var a in anchors)
        {
            var href = a.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(href))
            {
                continue;
            }

            var absolute = new Uri(documentUri, href);
            if (!IsHost(absolute.Host) || !LooksLikeArticlePath(absolute.AbsolutePath))
            {
                continue;
            }

            var url = NormalizeUrl(absolute);
            if (!seen.Add(url))
            {
                continue;
            }

            var title = WebUtility.HtmlDecode(a.InnerText.Trim());
            if (title.Length < 5)
            {
                continue;
            }

            list.Add(new CrawledArticleListItem
            {
                SourceAbsoluteUrl = url,
                Title = title,
            });
        }

        return Task.FromResult<IReadOnlyList<CrawledArticleListItem>>(list);
    }

    public Task<CrawledArticleDetailPatch> ParseArticleDetailHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var patch = new CrawledArticleDetailPatch();

        foreach (Match m in LdJsonScriptRegex.Matches(html))
        {
            if (TryFillFromLdJson(m.Groups[1].Value, patch))
            {
                break;
            }
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        if (string.IsNullOrWhiteSpace(patch.ContentHtml))
        {
            var body = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'article')]") ??
                       doc.DocumentNode.SelectSingleNode("//article") ??
                       doc.DocumentNode.SelectSingleNode("//*[contains(@class,'detail')]");
            patch.ContentHtml = WebUtility.HtmlDecode(body?.InnerHtml ?? string.Empty);
        }

        if (string.IsNullOrWhiteSpace(patch.Summary))
        {
            patch.Summary = GetMeta(doc, "description") ?? GetMeta(doc, "og:description");
        }

        if (patch.PublishedAt == null)
        {
            patch.PublishedAt = TryParseDate(GetMeta(doc, "article:published_time"));
        }

        if (string.IsNullOrWhiteSpace(patch.AuthorName))
        {
            patch.AuthorName = GetMeta(doc, "author");
        }

        patch.SourceLabel ??= documentUri.Host;

        return Task.FromResult(patch);
    }

    private static bool TryFillFromLdJson(string raw, CrawledArticleDetailPatch patch)
    {
        try
        {
            var node = JsonNode.Parse(raw) as JsonObject;
            if (node == null)
            {
                return false;
            }

            JsonObject? article = null;
            if (node["@type"]?.ToString().Contains("NewsArticle", StringComparison.OrdinalIgnoreCase) == true)
            {
                article = node;
            }
            else if (node["@graph"] is JsonArray graph)
            {
                article = graph.OfType<JsonObject>()
                    .FirstOrDefault(o => o["@type"]?.ToString().Contains("NewsArticle", StringComparison.OrdinalIgnoreCase) == true);
            }

            if (article == null)
            {
                return false;
            }

            patch.Summary ??= article["description"]?.ToString();
            patch.AuthorName ??= article["author"]?["name"]?.ToString() ?? article["author"]?.ToString();
            patch.ContentHtml ??= article["articleBody"]?.ToString();
            if (article["datePublished"] != null &&
                DateTime.TryParse(article["datePublished"]!.ToString(), CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var dt))
            {
                patch.PublishedAt = dt;
            }

            if (article["image"] is JsonObject imgObj && imgObj["url"] != null)
            {
                patch.ThumbnailUrl ??= imgObj["url"]!.ToString();
            }
            else if (article["image"] is JsonValue imgVal)
            {
                patch.ThumbnailUrl ??= imgVal.ToString();
            }

            return !string.IsNullOrWhiteSpace(patch.ContentHtml) || !string.IsNullOrWhiteSpace(patch.Summary);
        }
        catch
        {
            return false;
        }
    }

    private static string? GetMeta(HtmlDocument doc, string nameOrProperty)
    {
        var n = doc.DocumentNode.SelectSingleNode($"//meta[@property='{nameOrProperty}']/@content")
                ?? doc.DocumentNode.SelectSingleNode($"//meta[@name='{nameOrProperty}']/@content");
        return WebUtility.HtmlDecode(n?.GetAttributeValue("content", null)?.Trim());
    }

    private static DateTime? TryParseDate(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return null;
        }

        return DateTime.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind, out var d)
            ? d
            : null;
    }

    private static bool IsHost(string host)
    {
        var h = CrawlerUriHostNormalizer.NormalizeHost(host);
        return h.Equals("baoninhthuan.com.vn", StringComparison.OrdinalIgnoreCase)
               || h.Equals("www.baoninhthuan.com.vn", StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeHost(string host)
    {
        var h = CrawlerUriHostNormalizer.NormalizeHost(host);
        return h.Contains("www.", StringComparison.OrdinalIgnoreCase) ? h : "www.baoninhthuan.com.vn";
    }

    private static string NormalizeUrl(Uri uri)
    {
        var b = new UriBuilder(uri) { Scheme = Uri.UriSchemeHttps, Fragment = string.Empty };
        if (b.Path.EndsWith('/'))
        {
            b.Path = b.Path.TrimEnd('/');
        }

        return b.Uri.AbsoluteUri;
    }

    private static bool LooksLikeArticlePath(string path)
    {
        if (path.Length < 12)
        {
            return false;
        }

        if (!path.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var lower = path.ToLowerInvariant();
        if (lower.Contains("/tag/", StringComparison.Ordinal))
        {
            return false;
        }

        return true;
    }
}
