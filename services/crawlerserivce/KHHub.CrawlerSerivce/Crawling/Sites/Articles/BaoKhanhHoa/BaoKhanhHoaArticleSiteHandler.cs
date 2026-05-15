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

namespace KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoKhanhHoa;

/// <summary>
/// Listing + detail for <see href="https://baokhanhhoa.vn/">baokhanhhoa.vn</see> (heuristic article URLs + JSON-LD / OG).
/// Registered explicitly in <see cref="CrawlerSiteHandlersServiceCollectionExtensions.AddCrawlerArticleNewsSiteHandlers"/>.
/// </summary>
public class BaoKhanhHoaArticleSiteHandler : IArticleNewsSiteHandler
{
    public const string SiteKeyConst = "BaoKhanhHoa";

    private static readonly Regex LdJsonScriptRegex = new(
        @"<script\s+type=[""']application/ld\+json[""'][^>]*>(.*?)</script>",
        RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    /// <summary>
    /// Article paths like <c>/xa-hoi/202605/slug-id/</c> (no .html suffix on current site layout).
    /// </summary>
    private static readonly Regex ModernArticlePathRegex = new(
        @"/(?:19|20)\d{4}/[^/]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string SiteKey => SiteKeyConst;

    public bool SupportsListingPage(Uri uri)
    {
        return IsBaoKhanhHoaHost(uri.Host);
    }

    public bool SupportsArticleDetailPage(Uri uri)
    {
        return IsBaoKhanhHoaHost(uri.Host) && LooksLikeArticlePath(uri.AbsolutePath);
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
            var href = a.GetAttributeValue("href", null)?.Trim();
            if (string.IsNullOrWhiteSpace(href))
            {
                continue;
            }

            var absolute = new Uri(documentUri, href);
            if (!IsBaoKhanhHoaHost(absolute.Host) || !LooksLikeArticlePath(absolute.AbsolutePath))
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
                title = WebUtility.HtmlDecode(a.GetAttributeValue("title", string.Empty).Trim());
            }

            if (title.Length < 5)
            {
                var img = a.SelectSingleNode(".//img[@alt]");
                if (img != null)
                {
                    title = WebUtility.HtmlDecode(img.GetAttributeValue("alt", string.Empty).Trim());
                }
            }

            if (title.Length < 5)
            {
                continue;
            }

            var item = new CrawledArticleListItem
            {
                SourceAbsoluteUrl = url,
                Title = title,
                ThumbnailUrl = null,
                PublishedAt = null,
                IsHot = false,
                IsTrending = false
            };

            if (BaoKhanhHoaUriCategoryExtractor.TryExtractCategoryPath(absolute, out var primary,
                    out var parents) &&
                !string.IsNullOrWhiteSpace(primary))
            {
                item.CategoryLabel = primary;
                item.ArticleCategoryNameCandidates.AddRange(parents);
            }

            list.Add(item);
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
            MergeLdJsonBlock(m.Groups[1].Value, patch);
        }

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        if (string.IsNullOrWhiteSpace(patch.ContentHtml))
        {
            var body = doc.DocumentNode.SelectSingleNode(
                           "//div[contains(@class,'article')]") ??
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
            patch.AuthorName = GetMeta(doc, "author") ?? GetMeta(doc, "article:author");
        }

        FillTopicLabelsFromMeta(doc, patch);
        TryFillCategoryFromBreadcrumb(doc, patch);
        ApplyUriCategoryHints(documentUri, patch);

        patch.SourceLabel ??= documentUri.Host;

        return Task.FromResult(patch);
    }

    private static void MergeLdJsonBlock(string raw, CrawledArticleDetailPatch patch)
    {
        try
        {
            var root = JsonNode.Parse(raw);
            foreach (var article in CollectNewsArticleObjects(root))
            {
                MergeNewsArticleFields(article, patch);
            }
        }
        catch
        {
            // Malformed or non-JSON noise inside script tags — ignore.
        }
    }

    private static IEnumerable<JsonObject> CollectNewsArticleObjects(JsonNode? node)
    {
        switch (node)
        {
            case JsonObject obj:
                if (IsNewsArticleObject(obj))
                {
                    yield return obj;
                }

                if (obj["@graph"] is JsonArray graph)
                {
                    foreach (var item in graph)
                    {
                        foreach (var nested in CollectNewsArticleObjects(item))
                        {
                            yield return nested;
                        }
                    }
                }

                yield break;

            case JsonArray arr:
                foreach (var item in arr)
                {
                    foreach (var nested in CollectNewsArticleObjects(item))
                    {
                        yield return nested;
                    }
                }

                yield break;
        }
    }

    private static bool IsNewsArticleObject(JsonObject obj)
    {
        return TypeNodeContainsNewsArticle(obj["@type"]);
    }

    private static bool TypeNodeContainsNewsArticle(JsonNode? typeNode)
    {
        switch (typeNode)
        {
            case JsonArray arr:
                return arr.Any(TypeTokenIsNewsArticle);
            default:
                return TypeTokenIsNewsArticle(typeNode);
        }
    }

    private static bool TypeTokenIsNewsArticle(JsonNode? n)
    {
        if (n is JsonValue jv && jv.TryGetValue<string>(out var s))
        {
            return s.Contains("NewsArticle", StringComparison.OrdinalIgnoreCase);
        }

        var t = n?.ToString() ?? string.Empty;
        return t.Contains("NewsArticle", StringComparison.OrdinalIgnoreCase);
    }

    private static void MergeNewsArticleFields(JsonObject article, CrawledArticleDetailPatch patch)
    {
        patch.Summary ??= JsonString(article["description"]);
        patch.AuthorName ??= JsonString(article["author"]?["name"]) ?? JsonString(article["author"]);
        patch.ContentHtml ??= JsonString(article["articleBody"]);
        if (article["datePublished"] != null &&
            DateTime.TryParse(article["datePublished"]!.ToString(), CultureInfo.InvariantCulture,
                DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out var dt))
        {
            patch.PublishedAt ??= dt;
        }

        if (article["image"] is JsonObject imgObj && imgObj["url"] != null)
        {
            patch.ThumbnailUrl ??= JsonString(imgObj["url"]);
        }
        else if (article["image"] is JsonValue imgVal)
        {
            patch.ThumbnailUrl ??= JsonString(imgVal);
        }

        ApplyArticleSectionNode(article["articleSection"], patch);
        MergeKeywordsNode(article["keywords"], patch);
    }

    private static void ApplyArticleSectionNode(JsonNode? sectionNode, CrawledArticleDetailPatch patch)
    {
        var parts = FlattenArticleSectionParts(sectionNode);
        if (parts.Count == 0)
        {
            return;
        }

        // Breadcrumb-like order: general → specific; leaf is last.
        var leaf = parts[^1];
        if (string.IsNullOrWhiteSpace(patch.CategoryLabel))
        {
            patch.CategoryLabel = leaf;
        }
        else if (!leaf.Equals(patch.CategoryLabel.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            AddCategoryCandidate(patch, leaf);
        }

        for (var i = parts.Count - 2; i >= 0; i--)
        {
            AddCategoryCandidate(patch, parts[i]);
        }
    }

    private static List<string> FlattenArticleSectionParts(JsonNode? sectionNode)
    {
        var list = new List<string>();
        switch (sectionNode)
        {
            case JsonValue jv when jv.TryGetValue<string>(out var s):
                AppendSectionToken(list, s);
                break;
            case JsonArray arr:
                foreach (var item in arr)
                {
                    var t = JsonString(item);
                    if (!string.IsNullOrWhiteSpace(t))
                    {
                        list.Add(t.Trim());
                    }
                }

                break;
        }

        return list;
    }

    private static void AppendSectionToken(List<string> list, string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return;
        }

        foreach (var piece in raw.Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var t = piece.Trim();
            if (t.Length > 0)
            {
                list.Add(t);
            }
        }
    }

    private static void MergeKeywordsNode(JsonNode? keywordsNode, CrawledArticleDetailPatch patch)
    {
        switch (keywordsNode)
        {
            case JsonValue jv when jv.TryGetValue<string>(out var s):
                SplitAndAddTopicLabels(patch, s);
                break;
            case JsonArray arr:
                foreach (var item in arr)
                {
                    var t = JsonString(item);
                    if (!string.IsNullOrWhiteSpace(t))
                    {
                        AddTopicLabel(patch, t.Trim());
                    }
                }

                break;
        }
    }

    private static string? JsonString(JsonNode? node)
    {
        if (node == null)
        {
            return null;
        }

        if (node is JsonValue jv && jv.TryGetValue<string>(out var s))
        {
            return s;
        }

        return node.ToString();
    }

    private static void FillTopicLabelsFromMeta(HtmlDocument doc, CrawledArticleDetailPatch patch)
    {
        foreach (var n in doc.DocumentNode.SelectNodes("//meta[@name='keywords']") ??
                           Enumerable.Empty<HtmlNode>())
        {
            var content = WebUtility.HtmlDecode(n.GetAttributeValue("content", string.Empty).Trim());
            SplitAndAddTopicLabels(patch, content);
        }

        foreach (var n in doc.DocumentNode.SelectNodes("//meta[@property='article:tag']") ??
                           Enumerable.Empty<HtmlNode>())
        {
            var content = WebUtility.HtmlDecode(n.GetAttributeValue("content", string.Empty).Trim());
            AddTopicLabel(patch, content);
        }
    }

    private static void TryFillCategoryFromBreadcrumb(HtmlDocument doc, CrawledArticleDetailPatch patch)
    {
        var anchors = doc.DocumentNode.SelectNodes("//div[contains(@class,'breadcrumb')]//a");
        if (anchors == null || anchors.Count == 0)
        {
            return;
        }

        var crumbs = new List<string>();
        foreach (var a in anchors)
        {
            var text = WebUtility.HtmlDecode(a.InnerText.Trim());
            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            if (text.Equals("Trang chủ", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            crumbs.Add(text);
        }

        if (crumbs.Count == 0)
        {
            return;
        }

        var leaf = crumbs[^1];
        if (string.IsNullOrWhiteSpace(patch.CategoryLabel))
        {
            patch.CategoryLabel = leaf;
        }
        else if (!leaf.Equals(patch.CategoryLabel.Trim(), StringComparison.OrdinalIgnoreCase))
        {
            AddCategoryCandidate(patch, leaf);
        }

        for (var i = crumbs.Count - 2; i >= 0; i--)
        {
            AddCategoryCandidate(patch, crumbs[i]);
        }
    }

    private static void ApplyUriCategoryHints(Uri documentUri, CrawledArticleDetailPatch patch)
    {
        if (!BaoKhanhHoaUriCategoryExtractor.TryExtractCategoryPath(documentUri, out var primary,
                out var parents)
            || string.IsNullOrWhiteSpace(primary))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(patch.CategoryLabel))
        {
            patch.CategoryLabel = primary;
        }
        else if (!string.Equals(patch.CategoryLabel.Trim(), primary, StringComparison.OrdinalIgnoreCase))
        {
            AddCategoryCandidate(patch, primary);
        }

        foreach (var p in parents)
        {
            AddCategoryCandidate(patch, p);
        }
    }

    private static void AddTopicLabel(CrawledArticleDetailPatch patch, string label)
    {
        if (string.IsNullOrWhiteSpace(label))
        {
            return;
        }

        var t = label.Trim();
        if (patch.TopicLabels.Any(x => string.Equals(x, t, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        patch.TopicLabels.Add(t);
    }

    private static void SplitAndAddTopicLabels(CrawledArticleDetailPatch patch, string blob)
    {
        if (string.IsNullOrWhiteSpace(blob))
        {
            return;
        }

        foreach (var part in blob.Split(new[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries))
        {
            AddTopicLabel(patch, part.Trim());
        }
    }

    private static void AddCategoryCandidate(CrawledArticleDetailPatch patch, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return;
        }

        var t = name.Trim();
        var primary = patch.CategoryLabel?.Trim();
        if (!string.IsNullOrWhiteSpace(primary) &&
            string.Equals(t, primary, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        if (patch.ArticleCategoryNameCandidates.Any(x =>
                string.Equals(x, t, StringComparison.OrdinalIgnoreCase)))
        {
            return;
        }

        patch.ArticleCategoryNameCandidates.Add(t);
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

    private static bool IsBaoKhanhHoaHost(string host)
    {
        var h = CrawlerUriHostNormalizer.NormalizeHost(host);
        return h.Equals("baokhanhhoa.vn", StringComparison.OrdinalIgnoreCase)
               || h.Equals("www.baokhanhhoa.vn", StringComparison.OrdinalIgnoreCase);
    }

    private static string NormalizeHost(string host)
    {
        var h = CrawlerUriHostNormalizer.NormalizeHost(host);
        return h.StartsWith("www.", StringComparison.OrdinalIgnoreCase) ? h : "www.baokhanhhoa.vn";
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

        var lower = path.ToLowerInvariant();
        if (lower.Contains("/tag/", StringComparison.Ordinal) ||
            lower.Contains("/gioi-thieu", StringComparison.Ordinal) ||
            lower.Contains("/lien-he", StringComparison.Ordinal))
        {
            return false;
        }

        // Legacy CMS URLs
        if (path.EndsWith(".html", StringComparison.OrdinalIgnoreCase))
        {
            return true;
        }

        // Current layout: /.../yyyymm/slug-token/
        var trimmed = path.TrimEnd('/');
        return trimmed.Length >= 20 && ModernArticlePathRegex.IsMatch(trimmed);
    }
}
