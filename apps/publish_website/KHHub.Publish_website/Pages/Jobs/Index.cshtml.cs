using System.Globalization;
using System.Text.Encodings.Web;
using System.Text.Json;
using KHHub.Publish_website.Services;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace KHHub.Publish_website.Pages.Jobs;

public sealed class IndexModel : PageModel
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    private readonly PublicMasterDataCatalogClient _catalogClient;

    public IndexModel(PublicMasterDataCatalogClient catalogClient)
    {
        _catalogClient = catalogClient;
    }

    public PublicContentQuery Query { get; private set; } = new();

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

    public PagedContentResult<PublicContentCardViewModel> Cards { get; private set; } = new();

    public FilterSidebarViewModel Filters { get; private set; } = default!;

    public SortBarViewModel SortBar { get; private set; } = default!;

    public SeoMetadata Seo { get; private set; } = default!;

    public string JsonLd { get; private set; } = string.Empty;

    public string ListPath => IsEnglish ? "/jobs" : "/viec-lam";

    private bool IsEnglish => CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase);

    public async Task OnGetAsync(CancellationToken cancellationToken)
    {
        NormalizeQuery();

        var listing = await _catalogClient.GetPublishedJobListingAsync(Query, ListPath, cancellationToken);
        Cards = listing.Cards;
        Filters = listing.Filters;
        SortBar = new SortBarViewModel
        {
            ActionPath = ListPath,
            Query = Query,
            TotalCount = Cards.TotalCount,
            Heading = IsEnglish ? "Career opportunities" : "Cơ hội việc làm nổi bật"
        };

        var title = IsEnglish ? "Jobs in Khanh Hoa | KH HUB" : "Việc làm Khánh Hòa | KH HUB";
        var description = IsEnglish
            ? "Find tourism, technology, service and creative jobs in Khanh Hoa."
            : "Tìm việc làm ngành du lịch, công nghệ, dịch vụ và sáng tạo tại Khánh Hòa.";

        Seo = new SeoMetadata(title, description, AbsoluteUrl(ListPath), "website");
        JsonLd = JsonSerializer.Serialize(BuildCollectionSchema(title, description), JsonOptions);
    }

    private void NormalizeQuery()
    {
        var page = int.TryParse(Request.Query["Page"], out var requestedPage)
            ? requestedPage
            : 1;

        Query = new PublicContentQuery
        {
            Search = Search,
            Province = Province,
            Ward = Ward,
            Category = Category,
            Tags = Tags
                .Where(x => !string.IsNullOrWhiteSpace(x))
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList(),
            Sort = string.IsNullOrWhiteSpace(Sort) ? PublicContentSort.Newest : Sort.ToLowerInvariant(),
            View = string.Equals(View, PublicContentView.Grid, StringComparison.OrdinalIgnoreCase)
                ? PublicContentView.Grid
                : PublicContentView.List,
            Page = Math.Max(1, page),
            PageSize = 20
        };
    }

    private Dictionary<string, object?> BuildCollectionSchema(string title, string description)
    {
        return new Dictionary<string, object?>
        {
            ["@context"] = "https://schema.org",
            ["@type"] = "CollectionPage",
            ["name"] = title,
            ["description"] = description,
            ["url"] = AbsoluteUrl(ListPath),
            ["mainEntity"] = new Dictionary<string, object?>
            {
                ["@type"] = "ItemList",
                ["itemListElement"] = Cards.Items.Select((item, index) => new Dictionary<string, object?>
                {
                    ["@type"] = "ListItem",
                    ["position"] = index + 1,
                    ["url"] = AbsoluteUrl(item.Url),
                    ["name"] = item.Title
                }).ToList()
            }
        };
    }

    private string AbsoluteUrl(string path)
    {
        var normalizedPath = path.StartsWith('/') ? path : "/" + path;
        return $"{Request.Scheme}://{Request.Host}{normalizedPath}";
    }
}
