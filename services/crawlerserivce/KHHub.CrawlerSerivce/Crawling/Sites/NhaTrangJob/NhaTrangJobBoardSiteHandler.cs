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

namespace KHHub.CrawlerSerivce.Crawling.Sites.NhaTrangJob;

/// <summary>
/// Listing + detail parser for <see href="https://nhatrangjob.vn/viec-lam">nhatrangjob.vn</see>.
/// Detail enrichment prefers schema.org <c>JobPosting</c> JSON-LD when present.
/// </summary>
public class NhaTrangJobBoardSiteHandler : IJobBoardSiteHandler, ITransientDependency
{
    public const string SiteKeyConst = "NhaTrangJob";

    private static readonly Regex JobDetailPathRegex = new(
        @"^/viec-lam/.+\-\d+\.html$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

    private static readonly Regex LdJsonScriptRegex = new(
        @"<script\s+type=[""']application/ld\+json[""'][^>]*>(.*?)</script>",
        RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.IgnoreCase);

    private static readonly Regex ExternalJobIdRegex = new(@"-(\d+)\.html$", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly Regex PublishedRegex =
        new(@"Ngày\s+đăng:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly Regex DeadlineRegex =
        new(@"Hạn\s+nộp\s+hồ\s+sơ:\s*(\d{2}/\d{2}/\d{4})", RegexOptions.Compiled | RegexOptions.CultureInvariant);

    public string SiteKey => SiteKeyConst;

    public bool SupportsListingPage(Uri uri)
    {
        if (!uri.Host.Equals("nhatrangjob.vn", StringComparison.OrdinalIgnoreCase))
        {
            return false;
        }

        var path = uri.AbsolutePath;
        return path.StartsWith("/viec-lam", StringComparison.OrdinalIgnoreCase) && !JobDetailPathRegex.IsMatch(path);
    }

    public bool SupportsJobDetailPage(Uri uri)
    {
        return uri.Host.Equals("nhatrangjob.vn", StringComparison.OrdinalIgnoreCase)
               && JobDetailPathRegex.IsMatch(uri.AbsolutePath);
    }

    public Uri BuildListingPageUri(Uri seedListingUri, int pageNumber)
    {
        if (pageNumber < 1)
        {
            throw new ArgumentOutOfRangeException(nameof(pageNumber));
        }

        var builder = new UriBuilder(seedListingUri)
        {
            Scheme = Uri.UriSchemeHttps,
            Host = "nhatrangjob.vn",
            Path = "/viec-lam/",
            Query = pageNumber <= 1 ? string.Empty : $"page={pageNumber}"
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

        var boxes = doc.DocumentNode.SelectNodes("//div[contains(@class,'box-job-horizontal')]");

        var list = new List<CrawledJobListItem>();
        if (boxes == null)
        {
            return Task.FromResult<IReadOnlyList<CrawledJobListItem>>(list);
        }

        foreach (var box in boxes)
        {
            var titleAnchor = box.SelectSingleNode(".//p[contains(@class,'job-name')]//a[@href]");
            var href = titleAnchor?.GetAttributeValue("href", null);
            if (string.IsNullOrWhiteSpace(href))
            {
                continue;
            }

            var absolute = new Uri(documentUri, href).AbsoluteUri;
            if (!absolute.Contains("/viec-lam/", StringComparison.OrdinalIgnoreCase))
            {
                continue;
            }

            var title = WebUtility.HtmlDecode(titleAnchor!.InnerText.Trim());
            var companyAnchor = box.SelectSingleNode(".//a[contains(@class,'job-company')]");
            var company = WebUtility.HtmlDecode(companyAnchor?.InnerText.Trim() ?? string.Empty);

            var jobTypeNode = box.SelectSingleNode(".//span[contains(@class,'job-type')]");
            var employmentLabel = WebUtility.HtmlDecode(jobTypeNode?.InnerText.Trim() ?? string.Empty);

            var salaryNode = box.SelectSingleNode(".//div[contains(@class,'_salary')]//span");
            var salaryText = WebUtility.HtmlDecode(salaryNode?.InnerText.Trim() ?? string.Empty);

            var dateNode = box.SelectSingleNode(".//div[contains(@class,'_date')]");
            var dateText = dateNode?.InnerText ?? string.Empty;

            var expNode = box.SelectSingleNode(".//div[contains(@class,'_exp')]");
            var expText = expNode?.InnerText ?? string.Empty;

            list.Add(new CrawledJobListItem
            {
                SourceAbsoluteUrl = absolute,
                ExternalJobId = TryExtractExternalId(absolute),
                Title = title,
                CompanyName = string.IsNullOrEmpty(company) ? null : company,
                EmploymentTypeLabel = string.IsNullOrEmpty(employmentLabel) ? null : employmentLabel,
                SalaryDisplayText = string.IsNullOrEmpty(salaryText) ? null : salaryText,
                PublishedAt = MatchDate(PublishedRegex, dateText),
                ApplicationDeadline = MatchDate(DeadlineRegex, expText)
            });
        }

        return Task.FromResult<IReadOnlyList<CrawledJobListItem>>(list);
    }

    private static readonly Regex HtmlTagRegex = new("<.*?>", RegexOptions.Compiled | RegexOptions.Singleline);

    private static readonly Regex EmailRegex = new(
        @"[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}",
        RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private static readonly Regex PhoneDigitRunRegex = new(@"\d(?:[\d\s.+]*)?\d", RegexOptions.Compiled | RegexOptions.CultureInvariant);

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

    /// <summary>
    /// Fills structured blocks from nhatrangjob.vn DOM (badges + section HTML).
    /// </summary>
    private static void EnrichPatchFromDetailDom(HtmlDocument doc, CrawledJobDetailPatch patch, Uri documentUri)
    {
        ApplyJobLogoImageUrls(doc, patch, documentUri);

        ParseBadgeGroup(doc, "Địa điểm tuyển dụng", patch.RecruitmentLocationLabels);
        ParseBadgeGroup(doc, "Ngành nghề", patch.IndustryLabels);

        patch.JobDescriptionHtml = ReadSectionInnerHtml(doc, "Mô tả công việc");
        patch.BenefitsHtml = ReadSectionInnerHtml(doc, "Quyền lợi được hưởng");
        patch.RequirementsHtml = ReadSectionInnerHtml(doc, "Yêu cầu công việc");

        ParseContactSection(doc, patch);

        if (!string.IsNullOrWhiteSpace(patch.JobDescriptionHtml))
        {
            patch.Description = patch.JobDescriptionHtml;
            patch.Summary = BuildSummary(PlainTextFromHtml(patch.JobDescriptionHtml));
        }

        if (string.IsNullOrWhiteSpace(patch.LocationDetail))
        {
            var joined = string.Join(", ", patch.RecruitmentLocationLabels.Where(s => !string.IsNullOrWhiteSpace(s)));
            if (!string.IsNullOrWhiteSpace(joined))
            {
                patch.LocationDetail = joined.Trim();
            }
        }
    }

    /// <summary>
    /// Reads employer logo from <c>.job-logo img</c>; uses same URL for thumbnail and cover on this site.
    /// </summary>
    private static void ApplyJobLogoImageUrls(HtmlDocument doc, CrawledJobDetailPatch patch, Uri documentUri)
    {
        var img = doc.DocumentNode.SelectSingleNode("//div[contains(@class,'job-logo')]//img[@src]");
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

    private static void ParseBadgeGroup(HtmlDocument doc, string labelContains, List<string> sink)
    {
        var root = doc.DocumentNode.SelectSingleNode(
            $"//div[contains(@class,'text-icon')][.//div[contains(@class,'_text')][contains(normalize-space(.), '{labelContains}')]]//div[contains(@class,'_content')]");
        if (root == null)
        {
            return;
        }

        var anchors = root.SelectNodes(".//a");
        if (anchors == null)
        {
            return;
        }

        foreach (var a in anchors)
        {
            var t = WebUtility.HtmlDecode(a.InnerText.Trim());
            if (!string.IsNullOrWhiteSpace(t))
            {
                sink.Add(t);
            }
        }
    }

    private static string? ReadSectionInnerHtml(HtmlDocument doc, string headingText)
    {
        var xpath =
            $"//div[contains(@class,'sub-heading')][.//h2[contains(normalize-space(.), '{headingText}')]]/following-sibling::div[contains(@class,'entry-content')][1]";
        var node = doc.DocumentNode.SelectSingleNode(xpath);
        var html = node?.InnerHtml.Trim();
        return string.IsNullOrWhiteSpace(html) ? null : html;
    }

    private static void ParseContactSection(HtmlDocument doc, CrawledJobDetailPatch patch)
    {
        var xpath =
            "//div[contains(@class,'sub-heading')][.//h2[contains(normalize-space(.),'Thông tin liên hệ')]]/following-sibling::div[contains(@class,'entry-content')][1]";
        var node = doc.DocumentNode.SelectSingleNode(xpath);
        if (node == null)
        {
            return;
        }

        var blob = WebUtility.HtmlDecode(node.InnerText).Replace('\u00A0', ' ');
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

        if (string.IsNullOrWhiteSpace(patch.ContactEmail))
        {
            var m = EmailRegex.Match(node.InnerHtml);
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

    private static string? PlainTextFromHtml(string html)
    {
        var decoded = WebUtility.HtmlDecode(html);
        var spaced = HtmlTagRegex.Replace(decoded, " ");
        return Regex.Replace(spaced, @"\s+", " ", RegexOptions.CultureInvariant).Trim();
    }

    private static CrawledJobDetailPatch MapJobPostingLd(JsonNode jp)
    {
        var patch = new CrawledJobDetailPatch();

        patch.Description = jp["description"]?.GetValue<string>();
        patch.Summary = BuildSummary(patch.Description);

        var salaryNode = jp["baseSalary"];
        if (salaryNode != null)
        {
            patch.SalaryCurrency = salaryNode["currency"]?.GetValue<string>() ?? "VND";
            if (decimal.TryParse(salaryNode["minValue"]?.GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture, out var smin))
            {
                patch.SalaryMin = smin;
            }

            if (decimal.TryParse(salaryNode["maxValue"]?.GetValue<string>(), NumberStyles.Any, CultureInfo.InvariantCulture, out var smax))
            {
                patch.SalaryMax = smax;
            }
        }

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
        }

        var addr = jp["jobLocation"]?["address"];
        if (addr != null)
        {
            var street = addr["streetAddress"]?.GetValue<string>();
            var locality = addr["addressLocality"]?.GetValue<string>();
            patch.LocationDetail = string.Join(", ", new[] { street, locality }.Where(s => !string.IsNullOrWhiteSpace(s)));
        }

        return patch;
    }

    private static string? BuildSummary(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
        {
            return null;
        }

        var plain = WebUtility.HtmlDecode(description).Trim();
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

        if (node is JsonObject obj && obj.TryGetPropertyValue("@graph", out var graph) && graph is JsonArray arr)
        {
            foreach (var item in arr)
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

    private static string? TryExtractExternalId(string absoluteUrl)
    {
        var m = ExternalJobIdRegex.Match(absoluteUrl);
        return m.Success ? m.Groups[1].Value : null;
    }

    private static DateTime? MatchDate(Regex regex, string text)
    {
        var m = regex.Match(text.Replace('\u00A0', ' '));
        if (!m.Success)
        {
            return null;
        }

        return DateTime.TryParseExact(
            m.Groups[1].Value,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.AssumeLocal,
            out var dt)
            ? dt
            : null;
    }
}
