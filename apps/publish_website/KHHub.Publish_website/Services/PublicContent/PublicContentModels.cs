namespace KHHub.Publish_website.Services.PublicContent;

public enum PublicContentKind
{
    News,
    Job,
    Location
}

public sealed class PublicContentQuery
{
    public string? Search { get; set; }

    public string? Province { get; set; }

    public string? Ward { get; set; }

    public string? Category { get; set; }

    public List<string> Tags { get; set; } = [];

    public string Sort { get; set; } = PublicContentSort.Newest;

    public string View { get; set; } = PublicContentView.List;

    public int Page { get; set; } = 1;

    public int PageSize { get; set; } = 6;
}

public static class PublicContentSort
{
    public const string Newest = "newest";
    public const string Popular = "popular";
    public const string Relevant = "relevant";
}

public static class PublicContentView
{
    public const string Grid = "grid";
    public const string List = "list";
}

public sealed record FilterOption(string Value, string Label, int Count);

public sealed record SeoMetadata(
    string Title,
    string Description,
    string CanonicalUrl,
    string OgType,
    string? ImageUrl = null);

public sealed class FilterSidebarViewModel
{
    public required string ActionPath { get; init; }

    public required string Title { get; init; }

    public required PublicContentQuery Query { get; init; }

    public IReadOnlyList<FilterOption> Provinces { get; init; } = [];

    public IReadOnlyList<FilterOption> Wards { get; init; } = [];

    public IReadOnlyList<FilterOption> Categories { get; init; } = [];

    public IReadOnlyList<FilterOption> Tags { get; init; } = [];
}

public sealed class PublicJobListingResult
{
    public required PagedContentResult<PublicContentCardViewModel> Cards { get; init; }

    public required FilterSidebarViewModel Filters { get; init; }
}

public sealed class SortBarViewModel
{
    public required string ActionPath { get; init; }

    public required PublicContentQuery Query { get; init; }

    public int TotalCount { get; init; }

    public string Heading { get; init; } = string.Empty;
}

public sealed class PublicContentCardViewModel
{
    public required PublicContentKind Kind { get; init; }

    public required string Title { get; init; }

    public required string Slug { get; init; }

    public required string EnglishSlug { get; init; }

    public required string Description { get; init; }

    public required string Url { get; init; }

    public required string ThumbnailUrl { get; init; }

    public required string Category { get; init; }

    public string? CategoryIcon { get; init; } = null;

    public string? CategoryIconColor { get; init; } = null;

    public string? Province { get; init; }

    public string? Ward { get; init; }

    /// <summary>Ward administrative code from MasterData (used for stable badge color).</summary>
    public string? WardCode { get; init; }

    public string? MetaLabel { get; init; }

    public DateTimeOffset CreatedAt { get; init; }

    public int Popularity { get; init; }

    public decimal? Rating { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];
}

public sealed class PagedContentResult<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];

    public int TotalCount { get; init; }

    public int CurrentPage { get; init; }

    public int PageSize { get; init; }

    public int TotalPages => PageSize <= 0 ? 1 : Math.Max(1, (int)Math.Ceiling(TotalCount / (double)PageSize));
}

public sealed class NewsItem
{
    public required string Title { get; init; }

    public required string Slug { get; init; }

    public required string EnglishSlug { get; init; }

    public required string Description { get; init; }

    public required string Content { get; init; }

    public required string Author { get; init; }

    public required string Category { get; init; }

    public required string Province { get; init; }

    public required string Ward { get; init; }

    public required string ThumbnailUrl { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public int Popularity { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];
}

public sealed class JobItem
{
    public required string Title { get; init; }

    public required string Slug { get; init; }

    public required string EnglishSlug { get; init; }

    public required string Category { get; init; }

    public required string Company { get; init; }

    public required string Salary { get; init; }

    public required string Province { get; init; }

    public required string Ward { get; init; }

    public string? WardCode { get; init; }

    public required string Description { get; init; }

    public required string Requirements { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public int Popularity { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];
}

public sealed class LocationItem
{
    public required string Name { get; init; }

    public required string Slug { get; init; }

    public required string EnglishSlug { get; init; }

    public required string Province { get; init; }

    public required string Ward { get; init; }

    public required string Address { get; init; }

    public required string Description { get; init; }

    public required decimal Rating { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public int Popularity { get; init; }

    public IReadOnlyList<string> Images { get; init; } = [];

    public IReadOnlyList<string> Tags { get; init; } = [];

    public IReadOnlyList<string> Categories { get; init; } = [];
}

public sealed record CommentViewModel(string Author, string Body, DateTimeOffset CreatedAt, decimal Rating);

/// <summary>Job tag for detail pills: <see cref="Slug"/> in filter URLs, <see cref="Name"/> for display.</summary>
public sealed record JobDetailTagLink(string Slug, string Name);

public sealed class DetailPageViewModel
{
    public required PublicContentKind Kind { get; init; }

    public required string Title { get; init; }

    public required string Description { get; init; }

    public required string ContentHtml { get; init; }

    public required string Category { get; init; }

    public required string ThumbnailUrl { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public string? AuthorOrCompany { get; init; }

    public string? Salary { get; init; }

    public string? Requirements { get; init; }

    public string? Province { get; init; }

    public string? Ward { get; init; }

    public string? Address { get; init; }

    /// <summary>Free-text workplace / employer line from MasterData Job.Location.</summary>
    public string? JobLocation { get; init; }

    /// <summary>HTML body for benefits section (optional).</summary>
    public string? BenefitsHtml { get; init; }

    public string? ApplicationUrl { get; init; }

    public string? ContactEmail { get; init; }

    public string? ContactPhone { get; init; }

    public string? EmploymentTypeLabel { get; init; }

    public string? ExperienceLevelLabel { get; init; }

    public string? WorkModeLabel { get; init; }

    public decimal Rating { get; init; }

    public IReadOnlyList<string> Tags { get; init; } = [];

    /// <summary>Job tags with slug for URLs and name for UI (empty for news/locations).</summary>
    public IReadOnlyList<JobDetailTagLink> JobTagLinks { get; init; } = [];

    public IReadOnlyList<string> Images { get; init; } = [];

    public IReadOnlyList<PublicContentCardViewModel> RelatedItems { get; init; } = [];

    public IReadOnlyList<CommentViewModel> Comments { get; init; } = [];
}
