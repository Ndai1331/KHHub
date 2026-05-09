using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using KHHub.Publish_website.Services;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages.Jobs;

public sealed class DetailsModel : PageModel
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly PublicMasterDataCatalogClient _catalogClient;

    public DetailsModel(PublicMasterDataCatalogClient catalogClient)
    {
        _catalogClient = catalogClient;
    }

    public DetailPageViewModel Detail { get; private set; } = default!;

    public SeoMetadata Seo { get; private set; } = default!;

    public string JsonLd { get; private set; } = string.Empty;

    public string BreadcrumbJsonLd { get; private set; } = string.Empty;

    public string ListPath => IsEnglish ? "/jobs" : "/viec-lam";

    public string ListTitle => IsEnglish ? "Jobs" : "Việc làm";

    private bool IsEnglish => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase);

    public async Task<IActionResult> OnGetAsync(string slug, CancellationToken cancellationToken)
    {
        var detail = await _catalogClient.GetPublishedJobDetailAsync(slug, ListPath, cancellationToken);
        if (detail == null)
        {
            return NotFound();
        }

        Detail = detail;
        var path = $"{ListPath.TrimEnd('/')}/{slug}";
        Seo = new SeoMetadata(
            $"{detail.Title} | KH HUB",
            detail.Description,
            AbsoluteUrl(path),
            "JobPosting",
            detail.ThumbnailUrl);
        JsonLd = JsonSerializer.Serialize(BuildDetailSchema(detail, path), JsonOptions);
        BreadcrumbJsonLd = JsonSerializer.Serialize(BuildBreadcrumbSchema(detail.Title, path), JsonOptions);

        return Page();
    }

    private Dictionary<string, object?> BuildDetailSchema(DetailPageViewModel detail, string path)
    {
        return new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "JobPosting",
            ["name"] = detail.Title,
            ["headline"] = detail.Title,
            ["description"] = detail.Description,
            ["image"] = detail.ThumbnailUrl,
            ["url"] = AbsoluteUrl(path),
            ["datePosted"] = detail.CreatedAt.ToString("O"),
            ["hiringOrganization"] = new Dictionary<string, object?>
            {
                ["@type"] = "Organization",
                ["name"] = detail.AuthorOrCompany ?? "KH HUB"
            },
            ["jobLocation"] = new Dictionary<string, object?>
            {
                ["@type"] = "Place",
                ["address"] = $"{detail.Ward}, {detail.Province}"
            },
            ["baseSalary"] = detail.Salary
        };
    }

    private Dictionary<string, object?> BuildBreadcrumbSchema(string detailTitle, string detailPath)
    {
        return new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "BreadcrumbList",
            ["itemListElement"] = new[]
            {
                Breadcrumb(1, IsEnglish ? "Home" : "Trang chủ", AbsoluteUrl("/")),
                Breadcrumb(2, ListTitle, AbsoluteUrl(ListPath)),
                Breadcrumb(3, detailTitle, AbsoluteUrl(detailPath))
            }
        };
    }

    private static Dictionary<string, object?> Breadcrumb(int position, string name, string url)
    {
        return new Dictionary<string, object?>
        {
            ["@type"] = "ListItem",
            ["position"] = position,
            ["name"] = name,
            ["item"] = url
        };
    }

    private string AbsoluteUrl(string path)
    {
        var normalizedPath = path.StartsWith('/') ? path : "/" + path;
        return $"{Request.Scheme}://{Request.Host}{normalizedPath}";
    }
}
