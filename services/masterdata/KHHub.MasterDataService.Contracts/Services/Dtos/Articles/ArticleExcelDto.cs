using KHHub.MasterDataService.Entities.Articles;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Articles;

public abstract class ArticleExcelDtoBase
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string Summary { get; set; } = null!;
    public string Content { get; set; } = null!;
    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public ArticleType Type { get; set; }

    public string AuthorName { get; set; } = null!;
    public string? Source { get; set; }

    public string? SourceUrl { get; set; }

    public ArticleStatus Status { get; set; }

    public DateTime PublishedAt { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsHot { get; set; }

    public bool IsTrending { get; set; }

    public int ViewCount { get; set; }

    public int LikeCount { get; set; }

    public int ShareCount { get; set; }

    public int CommentCount { get; set; }

    public int ReadingTime { get; set; }

    public string SeoTitle { get; set; } = null!;
    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }
}