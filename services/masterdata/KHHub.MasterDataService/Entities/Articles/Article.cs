using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.Articles;

public abstract class ArticleBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Title { get; set; }

    [NotNull]
    public virtual string Slug { get; set; }

    [NotNull]
    public virtual string Summary { get; set; }

    [NotNull]
    public virtual string Content { get; set; }

    [CanBeNull]
    public virtual string? ThumbnailUrl { get; set; }

    [CanBeNull]
    public virtual string? CoverImageUrl { get; set; }

    public virtual ArticleType Type { get; set; }

    [NotNull]
    public virtual string AuthorName { get; set; }

    [CanBeNull]
    public virtual string? Source { get; set; }

    [CanBeNull]
    public virtual string? SourceUrl { get; set; }

    public virtual ArticleStatus Status { get; set; }

    public virtual DateTime PublishedAt { get; set; }

    public virtual bool IsFeatured { get; set; }

    public virtual bool IsHot { get; set; }

    public virtual bool IsTrending { get; set; }

    public virtual int ViewCount { get; set; }

    public virtual int LikeCount { get; set; }

    public virtual int ShareCount { get; set; }

    public virtual int CommentCount { get; set; }

    public virtual int ReadingTime { get; set; }

    [NotNull]
    public virtual string SeoTitle { get; set; }

    [CanBeNull]
    public virtual string? SeoDescription { get; set; }

    [CanBeNull]
    public virtual string? SeoKeywords { get; set; }

    public Guid ArticleCategoryId { get; set; }

    protected ArticleBase()
    {
    }

    public ArticleBase(Guid id, Guid articleCategoryId, string title, string slug, string summary, string content, ArticleType type, string authorName, ArticleStatus status, DateTime publishedAt, bool isFeatured, bool isHot, bool isTrending, int viewCount, int likeCount, int shareCount, int commentCount, int readingTime, string seoTitle, string? thumbnailUrl = null, string? coverImageUrl = null, string? source = null, string? sourceUrl = null, string? seoDescription = null, string? seoKeywords = null)
    {
        Id = id;
        Check.NotNull(title, nameof(title));
        Check.Length(title, nameof(title), ArticleConsts.TitleMaxLength, 0);
        Check.NotNull(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleConsts.SlugMaxLength, 0);
        Check.NotNull(summary, nameof(summary));
        Check.Length(summary, nameof(summary), ArticleConsts.SummaryMaxLength, 0);
        Check.NotNull(content, nameof(content));
        Check.NotNull(authorName, nameof(authorName));
        Check.Length(authorName, nameof(authorName), ArticleConsts.AuthorNameMaxLength, 0);
        Check.NotNull(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), ArticleConsts.SeoTitleMaxLength, 0);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), ArticleConsts.ThumbnailUrlMaxLength, 0);
        Check.Length(coverImageUrl, nameof(coverImageUrl), ArticleConsts.CoverImageUrlMaxLength, 0);
        Check.Length(source, nameof(source), ArticleConsts.SourceMaxLength, 0);
        Check.Length(sourceUrl, nameof(sourceUrl), ArticleConsts.SourceUrlMaxLength, 0);
        Title = title;
        Slug = slug;
        Summary = summary;
        Content = content;
        Type = type;
        AuthorName = authorName;
        Status = status;
        PublishedAt = publishedAt;
        IsFeatured = isFeatured;
        IsHot = isHot;
        IsTrending = isTrending;
        ViewCount = viewCount;
        LikeCount = likeCount;
        ShareCount = shareCount;
        CommentCount = commentCount;
        ReadingTime = readingTime;
        SeoTitle = seoTitle;
        ThumbnailUrl = thumbnailUrl;
        CoverImageUrl = coverImageUrl;
        Source = source;
        SourceUrl = sourceUrl;
        SeoDescription = seoDescription;
        SeoKeywords = seoKeywords;
        ArticleCategoryId = articleCategoryId;
    }
}