using KHHub.MasterDataService.Entities.Articles;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Articles;

public abstract class GetArticlesInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public string? Summary { get; set; }

    public string? Content { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public ArticleType? Type { get; set; }

    public string? AuthorName { get; set; }

    public string? Source { get; set; }

    public string? SourceUrl { get; set; }

    public ArticleStatus? Status { get; set; }

    public DateTime? PublishedAtMin { get; set; }

    public DateTime? PublishedAtMax { get; set; }

    public bool? IsFeatured { get; set; }

    public bool? IsHot { get; set; }

    public bool? IsTrending { get; set; }

    public int? ViewCountMin { get; set; }

    public int? ViewCountMax { get; set; }

    public int? LikeCountMin { get; set; }

    public int? LikeCountMax { get; set; }

    public int? ShareCountMin { get; set; }

    public int? ShareCountMax { get; set; }

    public int? CommentCountMin { get; set; }

    public int? CommentCountMax { get; set; }

    public int? ReadingTimeMin { get; set; }

    public int? ReadingTimeMax { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid? ArticleCategoryId { get; set; }

    public GetArticlesInputBase()
    {
    }
}