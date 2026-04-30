using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.Articles;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Articles;

public abstract class ArticleUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(ArticleConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;
    [Required]
    [StringLength(ArticleConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    [Required]
    [StringLength(ArticleConsts.SummaryMaxLength)]
    public string Summary { get; set; } = null!;
    [Required]
    public string Content { get; set; } = null!;
    [StringLength(ArticleConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }

    [StringLength(ArticleConsts.CoverImageUrlMaxLength)]
    public string? CoverImageUrl { get; set; }

    public ArticleType Type { get; set; }

    [Required]
    [StringLength(ArticleConsts.AuthorNameMaxLength)]
    public string AuthorName { get; set; } = null!;
    [StringLength(ArticleConsts.SourceMaxLength)]
    public string? Source { get; set; }

    [StringLength(ArticleConsts.SourceUrlMaxLength)]
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

    [Required]
    [StringLength(ArticleConsts.SeoTitleMaxLength)]
    public string SeoTitle { get; set; } = null!;
    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid ArticleCategoryId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}