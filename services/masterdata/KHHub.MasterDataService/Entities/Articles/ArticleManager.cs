using KHHub.MasterDataService.Entities.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.Articles;

namespace KHHub.MasterDataService.Entities.Articles;

public abstract class ArticleManagerBase : DomainService
{
    protected IArticleRepository _articleRepository;

    public ArticleManagerBase(IArticleRepository articleRepository)
    {
        _articleRepository = articleRepository;
    }

    public virtual async Task<Article> CreateAsync(Guid articleCategoryId, string title, string slug, string summary, string content, ArticleType type, string authorName, ArticleStatus status, DateTime publishedAt, bool isFeatured, bool isHot, bool isTrending, int viewCount, int likeCount, int shareCount, int commentCount, int readingTime, string seoTitle, string? thumbnailUrl = null, string? coverImageUrl = null, string? source = null, string? sourceUrl = null, string? seoDescription = null, string? seoKeywords = null)
    {
        Check.NotNull(articleCategoryId, nameof(articleCategoryId));
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), ArticleConsts.TitleMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleConsts.SlugMaxLength);
        Check.NotNullOrWhiteSpace(summary, nameof(summary));
        Check.Length(summary, nameof(summary), ArticleConsts.SummaryMaxLength);
        Check.NotNullOrWhiteSpace(content, nameof(content));
        Check.NotNull(type, nameof(type));
        Check.NotNullOrWhiteSpace(authorName, nameof(authorName));
        Check.Length(authorName, nameof(authorName), ArticleConsts.AuthorNameMaxLength);
        Check.NotNull(status, nameof(status));
        Check.NotNull(publishedAt, nameof(publishedAt));
        Check.NotNullOrWhiteSpace(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), ArticleConsts.SeoTitleMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), ArticleConsts.ThumbnailUrlMaxLength);
        Check.Length(coverImageUrl, nameof(coverImageUrl), ArticleConsts.CoverImageUrlMaxLength);
        Check.Length(source, nameof(source), ArticleConsts.SourceMaxLength);
        Check.Length(sourceUrl, nameof(sourceUrl), ArticleConsts.SourceUrlMaxLength);
        var article = new Article(GuidGenerator.Create(), articleCategoryId, title, slug, summary, content, type, authorName, status, publishedAt, isFeatured, isHot, isTrending, viewCount, likeCount, shareCount, commentCount, readingTime, seoTitle, thumbnailUrl, coverImageUrl, source, sourceUrl, seoDescription, seoKeywords);
        return await _articleRepository.InsertAsync(article);
    }

    public virtual async Task<Article> UpdateAsync(Guid id, Guid articleCategoryId, string title, string slug, string summary, string content, ArticleType type, string authorName, ArticleStatus status, DateTime publishedAt, bool isFeatured, bool isHot, bool isTrending, int viewCount, int likeCount, int shareCount, int commentCount, int readingTime, string seoTitle, string? thumbnailUrl = null, string? coverImageUrl = null, string? source = null, string? sourceUrl = null, string? seoDescription = null, string? seoKeywords = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(articleCategoryId, nameof(articleCategoryId));
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), ArticleConsts.TitleMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleConsts.SlugMaxLength);
        Check.NotNullOrWhiteSpace(summary, nameof(summary));
        Check.Length(summary, nameof(summary), ArticleConsts.SummaryMaxLength);
        Check.NotNullOrWhiteSpace(content, nameof(content));
        Check.NotNull(type, nameof(type));
        Check.NotNullOrWhiteSpace(authorName, nameof(authorName));
        Check.Length(authorName, nameof(authorName), ArticleConsts.AuthorNameMaxLength);
        Check.NotNull(status, nameof(status));
        Check.NotNull(publishedAt, nameof(publishedAt));
        Check.NotNullOrWhiteSpace(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), ArticleConsts.SeoTitleMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), ArticleConsts.ThumbnailUrlMaxLength);
        Check.Length(coverImageUrl, nameof(coverImageUrl), ArticleConsts.CoverImageUrlMaxLength);
        Check.Length(source, nameof(source), ArticleConsts.SourceMaxLength);
        Check.Length(sourceUrl, nameof(sourceUrl), ArticleConsts.SourceUrlMaxLength);
        var article = await _articleRepository.GetAsync(id);
        article.ArticleCategoryId = articleCategoryId;
        article.Title = title;
        article.Slug = slug;
        article.Summary = summary;
        article.Content = content;
        article.Type = type;
        article.AuthorName = authorName;
        article.Status = status;
        article.PublishedAt = publishedAt;
        article.IsFeatured = isFeatured;
        article.IsHot = isHot;
        article.IsTrending = isTrending;
        article.ViewCount = viewCount;
        article.LikeCount = likeCount;
        article.ShareCount = shareCount;
        article.CommentCount = commentCount;
        article.ReadingTime = readingTime;
        article.SeoTitle = seoTitle;
        article.ThumbnailUrl = thumbnailUrl;
        article.CoverImageUrl = coverImageUrl;
        article.Source = source;
        article.SourceUrl = sourceUrl;
        article.SeoDescription = seoDescription;
        article.SeoKeywords = seoKeywords;
        article.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _articleRepository.UpdateAsync(article);
    }
}