using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Models;
using Microsoft.AspNetCore.WebUtilities;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Crawling.Sites.CareerLink;

/// <summary>
/// Listing + detail parser for <see href="https://www.careerlink.vn/vieclam/list">careerlink.vn</see>.
/// Listing uses <c>li.job-item</c>; detail prefers schema.org <c>JobPosting</c> JSON-LD and DOM sections.
/// </summary>
public class CareerLinkJobBoardSiteHandler : IJobBoardSiteHandler, ITransientDependency
{
    public const string SiteKeyConst = "CareerLink";

    private static readonly Regex JobDetailPathRegex = new(
        @"^/tim-viec-lam/.+?/(\d+)/?$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

    private static readonly Regex LdJsonScriptRegex = new(
        @"<script\s+type=[""']application/ld\+json[""'][^>]*>(.*?)</script>",
        RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    private static readonly Regex HtmlTagRegex = new("<.*?>", RegexOptions.Compiled | RegexOptions.Singleline);

    private static readonly Regex EmailRegex = new(
        @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private static readonly Regex PhoneDigitRunRegex = new(@"\d(?:[\d\s.+]*)?\d", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string SiteKey => SiteKeyConst;

    public bool SupportsListingPage(Uri uri)
    {
        var path = uri.AbsolutePath.TrimEnd('/');
        return IsCareerLinkHost(uri.Host)
               && path.Equals("/vieclam/list", StringComparison.OrdinalIgnoreCase);
    }

    public bool SupportsJobDetailPage(Uri uri)
    {
        return IsCareerLinkHost(uri.Host)
               && JobDetailPathRegex.IsMatch(uri.AbsolutePath);
    }

    public Uri BuildListingPageUri(Uri seedListingUri, int pageNumber)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }

        var queryPairs = ParseQueryToMutableDictionary(seedListingUri.Query);
        if (pageNumber <= 1)
        {
            queryPairs.Remove("page");
        }
        else
        {
            queryPairs["page"] = pageNumber.ToString(CultureInfo.InvariantCulture);
        }

        var queryString = BuildQueryString(queryPairs);
        var builder = new UriBuilder(Uri.UriSchemeHttps, "www.careerlink.vn", -1, "/vieclam/list")
        {
            Query = queryString
        };
        return builder.Uri;
    }

    public Task<IReadOnlyList<CrawledJobListItem>> ParseListingHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var items = doc.DocumentNode.SelectNodes("//li[contains(@class,'job-item')]");
        var list = new List<CrawledJobListItem>();
        if (items == null)
        {
            return Task.FromResult<IReadOnlyList<CrawledJobListItem>>(list);
        }

        foreach (var li in items)
        {
            var link = li.SelectSingleNode(".//a[contains(@class,'job-link')][@href]");
            var href = link?.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(href))
            {
                continue;
            }

            var absolute = new Uri(documentUri, href).AbsoluteUri;
            if (!absolute.Contains("/tim-viec-lam/", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var titleNode = li.SelectSingleNode(".//h5[contains(@class,'job-name')]");
            var title = WebUtility.HtmlDecode((titleNode?.InnerText ?? link!.InnerText).Trim());
            if (string.IsNullOrWhiteSpace(title))
            {
                continue;
            }

            var companyNode = li.SelectSingleNode(".//a[contains(@class,'job-company')]");
            var company = WebUtility.HtmlDecode(companyNode?.InnerText.Trim() ?? string.Empty);

            var salaryNode = li.SelectSingleNode(".//span[contains(@class,'job-salary')]");
            var salaryText = ExtractDirectText(salaryNode);

            var positionNode = li.SelectSingleNode(".//a[contains(@class,'job-position')]");
            var employmentLabel = WebUtility.HtmlDecode(positionNode?.InnerText.Trim() ?? string.Empty);

            var dtNode = li.SelectSingleNode(".//span[contains(@class,'cl-datetime')][@data-datetime]");
            var publishedAt = TryParseUnixSecondsAttribute(dtNode?.GetAttributeValue("data-datetime", null));

            list.Add(new CrawledJobListItem
            {
                SourceAbsoluteUrl = StripUrlFragment(absolute),
                ExternalJobId = TryExtractExternalIdFromPath(new Uri(absolute, UriKind.Absolute).AbsolutePath),
                Title = title,
                CompanyName = string.IsNullOrEmpty(company) ? null : company,
                EmploymentTypeLabel = string.IsNullOrEmpty(employmentLabel) ? null : employmentLabel,
                SalaryDisplayText = string.IsNullOrEmpty(salaryText) ? null : salaryText,
                PublishedAt = publishedAt
            });
        }

        return Task.FromResult<IReadOnlyList<CrawledJobListItem>>(list);
    }

    public Task<CrawledJobDetailPatch> ParseJobDetailHtmlAsync(
        string html,
        Uri documentUri,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        CrawledJobDetailPatch? patch = null;
        foreach (Match m in LdJsonScriptRegex.Matches(html))
        {
            JsonNode? root;
            try
            {
                root = JsonNode.Parse(m.Groups[1].Value);
            }
            catch
            {
                continue;
            }

            var jobPosting = FindJobPostingNode(root);
            if (jobPosting != null)
            {
                patch = MapJobPostingLd(jobPosting);
                break;
            }
        }

        patch ??= new CrawledJobDetailPatch();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        EnrichPatchFromDetailDom(doc, patch, documentUri);

        return Task.FromResult(patch);
    }

    private static void EnrichPatchFromDetailDom(HtmlDocument doc, CrawledJobDetailPatch patch, Uri documentUri)
    {
        ApplyCompanyLogoImageUrls(doc, patch, documentUri);

        patch.JobDescriptionHtml = ReadRichTextSection(doc, "section-job-description");
        patch.BenefitsHtml = BuildBenefitsHtml(doc);
        patch.RequirementsHtml = ReadRichTextSection(doc, "section-job-skills");

        ParseIndustryFromJobSummary(doc, patch);
        ParseContactSection(doc, patch);

        if (!string.IsNullOrWhiteSpace(patch.JobDescriptionHtml))
        {
            patch.Description = patch.JobDescriptionHtml;
            patch.Summary = BuildSummary(PlainTextFromHtml(patch.JobDescriptionHtml));
        }
    }

    private static void ApplyCompanyLogoImageUrls(HtmlDocument doc, CrawledJobDetailPatch patch, Uri documentUri)
    {
        if (!string.IsNullOrWhiteSpace(patch.ThumbnailUrl))
        {
            return;
        }

        var img = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'company-logo')]//img[@src]")
                  ?? doc.DocumentNode.SelectSingleNode("//img[contains(@class,'company-img')][@src]");
        var src = img?.GetAttributeValue("src", null)?.Trim();
        if (string.IsNullOrWhiteSpace(src))
        {
            return;
        }

        var absolute = NormalizeImageUrl(documentUri, src);
        if (string.IsNullOrWhiteSpace(absolute))
        {
            return;
        }

        patch.ThumbnailUrl = absolute;
        patch.CoverImageUrl = absolute;
    }

    private static string? ReadRichTextSection(HtmlDocument doc, string sectionId)
    {
        var section = doc.DocumentNode.SelectSingleNode($"//*[@id='{sectionId}']");
        var rich = section?.SelectSingleNode(".//div[contains(@class,'rich-text-content')]")
                   ?? section?.SelectSingleNode(".//div[contains(@class,'raw-content')]");
        var htmlInner = rich?.InnerHtml.Trim();
        return string.IsNullOrWhiteSpace(htmlInner) ? null : htmlInner;
    }

    private static string? BuildBenefitsHtml(HtmlDocument doc)
    {
        var section = doc.DocumentNode.SelectSingleNode("//*[@id='section-job-benefits']");
        if (section == null)
        {
            return null;
        }

        var rows = section.SelectNodes(".//div[contains(@class,'job-benefit-item')]");
        if (rows == null || rows.Count == 0)
        {
            return null;
        }

        var sb = new StringBuilder();
        sb.Append("<ul>");
        foreach (var row in rows)
        {
            var span = row.SelectSingleNode(".//span[last()]");
            var text = WebUtility.HtmlDecode(span?.InnerText.Trim() ?? string.Empty);
            if (string.IsNullOrWhiteSpace(text))
            {
                continue;
            }

            sb.Append("<li>").Append(WebUtility.HtmlEncode(text)).Append("</li>");
        }

        sb.Append("</ul>");
        var result = sb.ToString();
        return result == "<ul></ul>" ? null : result;
    }

    private static void ParseIndustryFromJobSummary(HtmlDocument doc, CrawledJobDetailPatch patch)
    {
        if (patch.IndustryLabels.Count > 0)
        {
            return;
        }

        var labels = doc.DocumentNode.SelectNodes(
            "//div[contains(@class,'job-summary-item')][.//div[contains(@class,'summary-label')][contains(normalize-space(.),'Ngành nghề')]]//div[contains(@class,'font-weight-bolder')]//span");
        if (labels == null)
        {
            return;
        }

        foreach (var n in labels)
        {
            var t = WebUtility.HtmlDecode(n.InnerText.Trim());
            if (!string.IsNullOrWhiteSpace(t))
            {
                patch.IndustryLabels.Add(t);
            }
        }
    }

    private static void ParseContactSection(HtmlDocument doc, CrawledJobDetailPatch patch)
    {
        var root = doc.DocumentNode.SelectSingleNode("//*[@id='section-job-contact-information']");
        if (root == null)
        {
            return;
        }

        var blob = WebUtility.HtmlDecode(root.InnerText).Replace('\u00A0', ' ');
        foreach (var rawLine in blob.Split('\n', StringSplitOptions.RemoveEmptyEntries))
        {
            var line = rawLine.Trim();
            if (line.StartsWith("Địa chỉ:", StringComparison.OrdinalIgnoreCase))
            {
                patch.ContactAddress = line["Địa chỉ:".Length..].Trim();
            }
            else if (line.StartsWith("Điện thoại:", StringComparison.OrdinalIgnoreCase))
            {
                var tail = line["Điện thoại:".Length..].Trim();
                patch.ContactPhone = ExtractPhoneDigits(tail);
            }
            else if (line.StartsWith("Email:", StringComparison.OrdinalIgnoreCase))
            {
                var tail = line["Email:".Length..].Trim();
                var m = EmailRegex.Match(tail);
                if (m.Success)
                {
                    patch.ContactEmail = m.Value;
                }
            }
        }

        if (string.IsNullOrWhiteSpace(patch.ContactAddress))
        {
            var locationLi = root.SelectSingleNode(".//ul[contains(@class,'contact-person')]//li[.//i[contains(@class,'cli-location')]]");
            if (locationLi != null)
            {
                var spans = locationLi.SelectNodes(".//span");
                if (spans != null)
                {
                    var parts = spans
                        .Select(s => WebUtility.HtmlDecode(s.InnerText.Trim()))
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .ToList();
                    if (parts.Count > 0)
                    {
                        patch.ContactAddress = string.Join(", ", parts);
                    }
                }
            }
        }

        if (string.IsNullOrWhiteSpace(patch.ContactEmail))
        {
            var m = EmailRegex.Match(root.InnerHtml);
            if (m.Success)
            {
                patch.ContactEmail = m.Value;
            }
        }

        if (string.IsNullOrWhiteSpace(patch.ContactPhone))
        {
            foreach (Match m in PhoneDigitRunRegex.Matches(blob))
            {
                var phone = ExtractPhoneDigits(m.Value);
                if (!string.IsNullOrEmpty(phone))
                {
                    patch.ContactPhone = phone;
                    break;
                }
            }
        }
    }

    private static CrawledJobDetailPatch MapJobPostingLd(JsonNode jp)
    {
        var patch = new CrawledJobDetailPatch();

        patch.Description = jp["description"]?.GetValue<string>();
        patch.Summary = BuildSummary(patch.Description);

        ApplyBaseSalary(jp["baseSalary"], patch);

        patch.EmploymentTypeLabel = jp["employmentType"]?.GetValue<string>();

        var org = jp["hiringOrganization"];
        if (org != null)
        {
            var logo = org["logo"];
            patch.ThumbnailUrl = logo switch
            {
                JsonValue lv => lv.TryGetValue<string>(out var u) ? u : null,
                JsonObject lo => lo["url"]?.GetValue<string>() ?? lo["@id"]?.GetValue<string>(),
                _ => patch.ThumbnailUrl
            };
            patch.CoverImageUrl = patch.ThumbnailUrl;
        }

        patch.LocationDetail = BuildLocationFromJobLocation(jp["jobLocation"]);
        AppendLocalityHintsFromJobLocation(jp["jobLocation"], patch);

        var industry = jp["industry"]?.GetValue<string>();
        if (!string.IsNullOrWhiteSpace(industry))
        {
            foreach (var segment in industry.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var t = segment.Trim();
                if (t.Length > 0)
                {
                    patch.IndustryLabels.Add(t);
                }
            }
        }

        var datePosted = jp["datePosted"]?.GetValue<string>();
        if (!string.IsNullOrWhiteSpace(datePosted)
            && DateTime.TryParse(datePosted, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var posted))
        {
            patch.PublishedAt = DateTime.SpecifyKind(posted, DateTimeKind.Utc);
        }

        return patch;
    }

    private static void ApplyBaseSalary(JsonNode? salaryNode, CrawledJobDetailPatch patch)
    {
        if (salaryNode == null)
        {
            return;
        }

        patch.SalaryCurrency = salaryNode["currency"]?.GetValue<string>() ?? "VND";

        var valueObj = salaryNode["value"];
        if (valueObj is JsonObject vo)
        {
            if (TryParseDecimalNode(vo["minValue"], out var smin))
            {
                patch.SalaryMin = smin;
            }

            if (TryParseDecimalNode(vo["maxValue"], out var smax))
            {
                patch.SalaryMax = smax;
            }
        }

        if (patch.SalaryMin == null && TryParseDecimalNode(salaryNode["minValue"], out var smin2))
        {
            patch.SalaryMin = smin2;
        }

        if (patch.SalaryMax == null && TryParseDecimalNode(salaryNode["maxValue"], out var smax2))
        {
            patch.SalaryMax = smax2;
        }
    }

    private static bool TryParseDecimalNode(JsonNode? node, out decimal value)
    {
        value = 0;
        if (node == null)
        {
            return false;
        }

        if (node is JsonValue jv)
        {
            if (jv.TryGetValue<decimal>(out value))
            {
                return true;
            }

            if (jv.TryGetValue<string>(out var s) && decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
            {
                return true;
            }
        }

        return false;
    }

    private static string? BuildLocationFromJobLocation(JsonNode? jobLocationNode)
    {
        if (jobLocationNode == null)
        {
            return null;
        }

        if (jobLocationNode is JsonArray arr)
        {
            var parts = new List<string>();
            foreach (var item in arr)
            {
                var one = FormatPostalAddress(item?["address"]);
                if (!string.IsNullOrWhiteSpace(one))
                {
                    parts.Add(one!);
                }
            }

            return parts.Count == 0 ? null : string.Join(" | ", parts);
        }

        return FormatPostalAddress(jobLocationNode["address"]);
    }

    private static string? FormatPostalAddress(JsonNode? addr)
    {
        if (addr == null)
        {
            return null;
        }

        var street = addr["streetAddress"]?.GetValue<string>();
        var locality = addr["addressLocality"]?.GetValue<string>();
        var region = addr["addressRegion"]?.GetValue<string>();
        var country = addr["addressCountry"]?.GetValue<string>();
        return string.Join(
            ", ",
            new[] { street, locality, region, country }.Where(s => !string.IsNullOrWhiteSpace(s)));
    }

    private static void AppendLocalityHintsFromJobLocation(JsonNode? jobLocationNode, CrawledJobDetailPatch patch)
    {
        void AddHint(string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return;
            }

            var t = s.Trim();
            if (patch.RecruitmentLocationLabels.Any(x => string.Equals(x, t, StringComparison.OrdinalIgnoreCase)))
            {
                return;
            }

            patch.RecruitmentLocationLabels.Add(t);
        }

        if (jobLocationNode is JsonArray arr)
        {
            foreach (var item in arr)
            {
                var addr = item?["address"];
                AddHint(addr?["addressLocality"]?.GetValue<string>());
                AddHint(addr?["addressRegion"]?.GetValue<string>());
            }

            return;
        }

        var singleAddr = jobLocationNode?["address"];
        AddHint(singleAddr?["addressLocality"]?.GetValue<string>());
        AddHint(singleAddr?["addressRegion"]?.GetValue<string>());
    }

    private static string? BuildSummary(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return null;
        }

        var plain = (PlainTextFromHtml(description) ?? string.Empty).Trim();
        const int max = 500;
        return plain.Length <= max ? plain : plain[..max];
    }

    private static JsonNode? FindJobPostingNode(JsonNode? node)
    {
        if (node == null)
        {
            return null;
        }

        if (IsJobPosting(node))
        {
            return node;
        }

        if (node is JsonObject obj && obj.TryGetPropertyValue("@graph", out var graph) && graph is JsonArray graphArr)
        {
            foreach (var item in graphArr)
            {
                var found = FindJobPostingNode(item);
                if (found != null)
                {
                    return found;
                }
            }
        }

        return null;
    }

    private static bool IsJobPosting(JsonNode node)
    {
        var typeNode = node["@type"];
        if (typeNode == null)
        {
            return false;
        }

        if (typeNode is JsonValue v && v.TryGetValue<string>(out var s))
        {
            return string.Equals(s, "JobPosting", StringComparison.OrdinalIgnoreCase);
        }

        if (typeNode is JsonArray a)
        {
            foreach (var x in a)
            {
                if (x is JsonValue jv && jv.TryGetValue<string>(out var part) &&
                    string.Equals(part, "JobPosting", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private static bool IsCareerLinkHost(string host)
    {
        return host.Equals("careerlink.vn", StringComparison.OrdinalIgnoreCase)
               || host.Equals("www.careerlink.vn", StringComparison.OrdinalIgnoreCase);
    }

    private static Dictionary<string, string> ParseQueryToMutableDictionary(string? query)
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(query))
        {
            return dict;
        }

        var parsed = QueryHelpers.ParseQuery(query);
        foreach (var kv in parsed)
        {
            dict[kv.Key] = kv.Value.ToString();
        }

        return dict;
    }

    private static string BuildQueryString(Dictionary<string, string> pairs)
    {
        if (pairs.Count == 0)
        {
            return string.Empty;
        }

        return string.Join(
            "&",
            pairs.Select(kv =>
                $"{Uri.EscapeDataString(kv.Key)}={Uri.EscapeDataString(kv.Value)}"));
    }

    private static string? TryExtractExternalIdFromPath(string absolutePath)
    {
        var m = JobDetailPathRegex.Match(absolutePath);
        return m.Success ? m.Groups[1].Value : null;
    }

    private static DateTime? TryParseUnixSecondsAttribute(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
        {
            return null;
        }

        if (!long.TryParse(raw.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out var sec))
        {
            return null;
        }

        try
        {
            return DateTimeOffset.FromUnixTimeSeconds(sec).UtcDateTime;
        }
        catch
        {
            return null;
        }
    }

    private static string? ExtractDirectText(HtmlNode? root)
    {
        if (root == null)
        {
            return null;
        }

        var clone = root.Clone();
        var toRemove = clone.SelectNodes(".//span[contains(@class,'d-lg-none')]");
        if (toRemove != null)
        {
            foreach (var n in toRemove)
            {
                n.Remove();
            }
        }

        var text = WebUtility.HtmlDecode(clone.InnerText).Replace('\u00A0', ' ');
        text = Regex.Replace(text, @"\s+", " ", RegexOptions.CultureInvariant).Trim();
        return string.IsNullOrEmpty(text) ? null : text;
    }

    private static string? NormalizeImageUrl(Uri documentUri, string src)
    {
        var trimmed = src.Trim();

        if (trimmed.StartsWith("//", StringComparison.Ordinal))
        {
            trimmed = (documentUri.Scheme == Uri.UriSchemeHttps ? "https:" : "http:") + trimmed;
        }

        if (Uri.TryCreate(trimmed, UriKind.Absolute, out var abs))
        {
            return abs.GetComponents(UriComponents.HttpRequestUrl, UriFormat.UriEscaped);
        }

        try
        {
            return new Uri(documentUri, trimmed).GetComponents(UriComponents.AbsoluteUri, UriFormat.UriEscaped);
        }
        catch (UriFormatException)
        {
            return null;
        }
    }

    private static string? PlainTextFromHtml(string html)
    {
        var decoded = WebUtility.HtmlDecode(html);
        var spaced = HtmlTagRegex.Replace(decoded, " ");
        return Regex.Replace(spaced, @"\s+", " ", RegexOptions.CultureInvariant).Trim();
    }

    private static string? ExtractPhoneDigits(string raw)
    {
        var digits = Regex.Replace(raw, @"\D", "", RegexOptions.CultureInvariant);
        if (digits.StartsWith("84", StringComparison.Ordinal) && digits.Length >= 10)
        {
            digits = "0" + digits[2..];
        }

        if (digits.Length is >= 9 and <= 15 && digits.StartsWith('0'))
        {
            return digits.Length <= 20 ? digits : digits[..20];
        }

        return null;
    }

    private static string StripUrlFragment(string url)
    {
        var hash = url.IndexOf('#', StringComparison.Ordinal);
        return hash >= 0 ? url[..hash] : url;
    }
}
