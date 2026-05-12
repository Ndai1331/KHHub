using System.Text.Encodings.Web;
using System.Text.Json;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages.PublicContent;

public abstract class PublicListingPageModel : PageModel
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly IPublicContentCatalog _catalog;

    protected PublicListingPageModel(IPublicContentCatalog catalog)
    {
        _catalog = catalog;
    }

    public PublicContentQuery Query { get; set; } = new();

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Province { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Ward { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? Category { get; set; }

    [BindProperty(SupportsGet = true)]
    public List<string> Tags { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string Sort { get; set; } = PublicContentSort.Newest;

    [BindProperty(SupportsGet = true)]
    public string View { get; set; } = PublicContentView.List;

    public int PageNumber { get; set; } = 1;

    public PagedContentResult<PublicContentCardViewModel> Cards { get; protected set; } = new();

    public FilterSidebarViewModel Filters { get; protected set; } = default!;

    public SortBarViewModel SortBar { get; protected set; } = default!;

    public SeoMetadata Seo { get; protected set; } = default!;

    public string JsonLd { get; protected set; } = string.Empty;

    protected void LoadListing(PublicContentKind kind, string path, string title, string heading, string description)
    {
        NormalizeQuery();

        Cards = _catalog.GetCards(kind, Query);
        Filters = _catalog.GetFilters(kind, Query, path, "Bộ lọc");
        SortBar = new SortBarViewModel
        {
            ActionPath = path,
            Query = Query,
            TotalCount = Cards.TotalCount,
            Heading = heading
        };
        Seo = new SeoMetadata(title, description, AbsoluteUrl(path), "website");
        JsonLd = JsonSerializer.Serialize(BuildCollectionSchema(title, description, path, Cards.Items), JsonOptions);
    }

    private void NormalizeQuery()
    {
        if (int.TryParse(Request.Query["Page"], out var requestedPage))
        {
            PageNumber = requestedPage;
        }

        Query = new PublicContentQuery
        {
            Search = Search,
            Province = Province,
            Ward = Ward,
            Category = Category,
            Tags = Tags,
            Sort = Sort,
            View = View,
            Page = PageNumber,
            PageSize = Query.PageSize
        };
        Query.Page = Math.Max(1, Query.Page);
        Query.PageSize = Math.Clamp(Query.PageSize, 3, 24);
        Query.Sort = string.IsNullOrWhiteSpace(Query.Sort) ? PublicContentSort.Newest : Query.Sort.ToLowerInvariant();
        Query.View = string.Equals(Query.View, PublicContentView.Grid, StringComparison.OrdinalIgnoreCase)
            ? PublicContentView.Grid
            : PublicContentView.List;
        Query.Tags = Query.Tags
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
    }

    private Dictionary<string, object?> BuildCollectionSchema(
        string title,
        string description,
        string path,
        IReadOnlyList<PublicContentCardViewModel> items)
    {
        return new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "CollectionPage",
            ["name"] = title,
            ["description"] = description,
            ["url"] = AbsoluteUrl(path),
            ["mainEntity"] = new Dictionary<string, object?>
            {
                ["@type"] = "ItemList",
                ["itemListElement"] = items.Select((item, index) => new Dictionary<string, object?>
                {
                    ["@type"] = "ListItem",
                    ["position"] = index + 1,
                    ["url"] = AbsoluteUrl(item.Url),
                    ["name"] = item.Title
                }).ToList()
            }
        };
    }

    protected string AbsoluteUrl(string path)
    {
        var normalizedPath = path.StartsWith('/') ? path : "/" + path;
        return $"{Request.Scheme}://{Request.Host}{normalizedPath}";
    }
}

public abstract class PublicDetailPageModel : PageModel
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly IPublicContentCatalog _catalog;

    protected PublicDetailPageModel(IPublicContentCatalog catalog)
    {
        _catalog = catalog;
    }

    public DetailPageViewModel Detail { get; protected set; } = default!;

    public SeoMetadata Seo { get; protected set; } = default!;

    public string JsonLd { get; protected set; } = string.Empty;

    public string BreadcrumbJsonLd { get; protected set; } = string.Empty;

    /// <summary>Listing path for the current culture (e.g. /tin-tuc, /news).</summary>
    public string ContentListPath { get; protected set; } = string.Empty;

    protected IActionResult LoadDetail(PublicContentKind kind, string slug, string listPath, string listTitle)
    {
        var detail = _catalog.GetDetail(kind, slug);
        if (detail == null)
        {
            return NotFound();
        }

        Detail = detail;
        ContentListPath = listPath;
        var path = $"{listPath}/{slug}";
        Seo = new SeoMetadata(
            $"{detail.Title} | KH HUB",
            detail.Description,
            AbsoluteUrl(path),
            SchemaType(kind),
            detail.ThumbnailUrl);
        JsonLd = JsonSerializer.Serialize(BuildDetailSchema(kind, detail, path), JsonOptions);
        BreadcrumbJsonLd = JsonSerializer.Serialize(BuildBreadcrumbSchema(listPath, listTitle, detail.Title, path), JsonOptions);

        return Page();
    }

    private Dictionary<string, object?> BuildDetailSchema(PublicContentKind kind, DetailPageViewModel detail, string path)
    {
        var schema = new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = SchemaType(kind),
            ["name"] = detail.Title,
            ["headline"] = detail.Title,
            ["description"] = detail.Description,
            ["image"] = detail.ThumbnailUrl,
            ["url"] = AbsoluteUrl(path),
            ["datePublished"] = detail.CreatedAt.ToString("O")
        };

        if (kind == PublicContentKind.Job)
        {
            schema["hiringOrganization"] = new Dictionary<string, object?>
            {
                ["@type"] = "Organization",
                ["name"] = detail.AuthorOrCompany
            };
            schema["jobLocation"] = new Dictionary<string, object?>
            {
                ["@type"] = "Place",
                ["address"] = $"{detail.Ward}, {detail.Province}"
            };
            schema["baseSalary"] = detail.Salary;
        }

        if (kind == PublicContentKind.Location)
        {
            schema["address"] = detail.Address;
            schema["aggregateRating"] = new Dictionary<string, object?>
            {
                ["@type"] = "AggregateRating",
                ["ratingValue"] = detail.Rating,
                ["reviewCount"] = detail.Comments.Count
            };
        }

        return schema;
    }

    private Dictionary<string, object?> BuildBreadcrumbSchema(
        string listPath,
        string listTitle,
        string detailTitle,
        string detailPath)
    {
        return new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "BreadcrumbList",
            ["itemListElement"] = new[]
            {
                Breadcrumb(1, "Trang chủ", AbsoluteUrl("/")),
                Breadcrumb(2, listTitle, AbsoluteUrl(listPath)),
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

    private static string SchemaType(PublicContentKind kind)
    {
        return kind switch
        {
            PublicContentKind.Job => "JobPosting",
            PublicContentKind.Location => "Place",
            _ => "Article"
        };
    }

    protected string AbsoluteUrl(string path)
    {
        var normalizedPath = path.StartsWith('/') ? path : "/" + path;
        return $"{Request.Scheme}://{Request.Host}{normalizedPath}";
    }
}
