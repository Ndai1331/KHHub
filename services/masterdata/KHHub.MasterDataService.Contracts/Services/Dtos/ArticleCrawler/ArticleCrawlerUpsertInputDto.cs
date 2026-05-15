using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KHHub.MasterDataService.Entities.Articles;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCrawler;

/// <summary>
/// Payload from article news crawler after listing + detail normalization (machine-to-machine).
/// </summary>
public class ArticleCrawlerUpsertInputDto
{
    [Required]
    [StringLength(ArticleConsts.SourceUrlMaxLength)]
    public string SourceUrl { get; set; } = null!;

    [Required]
    [StringLength(ArticleConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;

    [Required]
    [StringLength(ArticleConsts.SummaryMaxLength)]
    public string Summary { get; set; } = null!;

    [Required]
    public string Content { get; set; } = null!;

    [StringLength(ArticleConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }

    [StringLength(ArticleConsts.CoverImageUrlMaxLength)]
    public string? CoverImageUrl { get; set; }

    public ArticleType Type { get; set; } = ArticleType.News;

    [Required]
    [StringLength(ArticleConsts.AuthorNameMaxLength)]
    public string AuthorName { get; set; } = null!;

    [StringLength(ArticleConsts.SourceMaxLength)]
    public string? Source { get; set; }

    public DateTime? PublishedAt { get; set; }

    public ArticleStatus Status { get; set; } = ArticleStatus.Pending;

    public bool IsFeatured { get; set; }

    public bool IsHot { get; set; }

    public bool IsTrending { get; set; }

    public int ReadingTime { get; set; }

    [Required]
    [StringLength(ArticleConsts.SeoTitleMaxLength)]
    public string SeoTitle { get; set; } = null!;

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid ArticleCategoryId { get; set; }

    /// <summary>
    /// Resolved before fallback <see cref="ArticleCategoryId"/> when set (find-or-create active category).
    /// </summary>
    public string? PrimaryArticleCategoryName { get; set; }

    /// <summary>
    /// Extra hints (most specific first); used with <see cref="PrimaryArticleCategoryName"/>.
    /// </summary>
    public List<string> ArticleCategoryNameCandidates { get; set; } = new();

    /// <summary>
    /// Tag display names; service creates tags and mappings as needed.
    /// </summary>
    public List<string> TagNames { get; set; } = new();
}
