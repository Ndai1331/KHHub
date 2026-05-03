using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using KHHub.MasterDataService.Data;
using KHHub.MasterDataService.Entities.Articles;

namespace KHHub.MasterDataService.Data.Articles;

public abstract class EfCoreArticleRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, Article, Guid>
{
    public EfCoreArticleRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? title = null, string? slug = null, string? summary = null, string? content = null, string? thumbnailUrl = null, string? coverImageUrl = null, ArticleType? type = null, string? authorName = null, string? source = null, string? sourceUrl = null, ArticleStatus? status = null, DateTime? publishedAtMin = null, DateTime? publishedAtMax = null, bool? isFeatured = null, bool? isHot = null, bool? isTrending = null, int? viewCountMin = null, int? viewCountMax = null, int? likeCountMin = null, int? likeCountMax = null, int? shareCountMin = null, int? shareCountMax = null, int? commentCountMin = null, int? commentCountMax = null, int? readingTimeMin = null, int? readingTimeMax = null, string? seoTitle = null, string? seoDescription = null, string? seoKeywords = null, Guid? articleCategoryId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, title, slug, summary, content, thumbnailUrl, coverImageUrl, type, authorName, source, sourceUrl, status, publishedAtMin, publishedAtMax, isFeatured, isHot, isTrending, viewCountMin, viewCountMax, likeCountMin, likeCountMax, shareCountMin, shareCountMax, commentCountMin, commentCountMax, readingTimeMin, readingTimeMax, seoTitle, seoDescription, seoKeywords, articleCategoryId);
        var ids = query.Select(x => x.Article.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<ArticleWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(article => new ArticleWithNavigationProperties { Article = article, ArticleCategory = dbContext.Set<ArticleCategory>().FirstOrDefault(c => c.Id == article.ArticleCategoryId) }).FirstOrDefault();
    }

    public virtual async Task<List<ArticleWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? title = null, string? slug = null, string? summary = null, string? content = null, string? thumbnailUrl = null, string? coverImageUrl = null, ArticleType? type = null, string? authorName = null, string? source = null, string? sourceUrl = null, ArticleStatus? status = null, DateTime? publishedAtMin = null, DateTime? publishedAtMax = null, bool? isFeatured = null, bool? isHot = null, bool? isTrending = null, int? viewCountMin = null, int? viewCountMax = null, int? likeCountMin = null, int? likeCountMax = null, int? shareCountMin = null, int? shareCountMax = null, int? commentCountMin = null, int? commentCountMax = null, int? readingTimeMin = null, int? readingTimeMax = null, string? seoTitle = null, string? seoDescription = null, string? seoKeywords = null, Guid? articleCategoryId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, title, slug, summary, content, thumbnailUrl, coverImageUrl, type, authorName, source, sourceUrl, status, publishedAtMin, publishedAtMax, isFeatured, isHot, isTrending, viewCountMin, viewCountMax, likeCountMin, likeCountMax, shareCountMin, shareCountMax, commentCountMin, commentCountMax, readingTimeMin, readingTimeMax, seoTitle, seoDescription, seoKeywords, articleCategoryId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<ArticleWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from article in (await GetDbSetAsync())
               join articleCategory in (await GetDbContextAsync()).Set<ArticleCategory>() on article.ArticleCategoryId equals articleCategory.Id into articleCategories
               from articleCategory in articleCategories.DefaultIfEmpty()
               select new ArticleWithNavigationProperties
               {
                   Article = article,
                   ArticleCategory = articleCategory
               };
    }

    protected virtual IQueryable<ArticleWithNavigationProperties> ApplyFilter(IQueryable<ArticleWithNavigationProperties> query, string? filterText, string? title = null, string? slug = null, string? summary = null, string? content = null, string? thumbnailUrl = null, string? coverImageUrl = null, ArticleType? type = null, string? authorName = null, string? source = null, string? sourceUrl = null, ArticleStatus? status = null, DateTime? publishedAtMin = null, DateTime? publishedAtMax = null, bool? isFeatured = null, bool? isHot = null, bool? isTrending = null, int? viewCountMin = null, int? viewCountMax = null, int? likeCountMin = null, int? likeCountMax = null, int? shareCountMin = null, int? shareCountMax = null, int? commentCountMin = null, int? commentCountMax = null, int? readingTimeMin = null, int? readingTimeMax = null, string? seoTitle = null, string? seoDescription = null, string? seoKeywords = null, Guid? articleCategoryId = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nTitle = MasterDataStringSearch.NormalizeForContains(title);
        var nSlug = MasterDataStringSearch.NormalizeForContains(slug);
        var nSummary = MasterDataStringSearch.NormalizeForContains(summary);
        var nContent = MasterDataStringSearch.NormalizeForContains(content);
        var nThumbnailUrl = MasterDataStringSearch.NormalizeForContains(thumbnailUrl);
        var nCoverImageUrl = MasterDataStringSearch.NormalizeForContains(coverImageUrl);
        var nAuthorName = MasterDataStringSearch.NormalizeForContains(authorName);
        var nSource = MasterDataStringSearch.NormalizeForContains(source);
        var nSourceUrl = MasterDataStringSearch.NormalizeForContains(sourceUrl);
        var nSeoTitle = MasterDataStringSearch.NormalizeForContains(seoTitle);
        var nSeoDescription = MasterDataStringSearch.NormalizeForContains(seoDescription);
        var nSeoKeywords = MasterDataStringSearch.NormalizeForContains(seoKeywords);
        return query.WhereIf(nf != null, e => (e.Article.Title != null && e.Article.Title.ToLower().Contains(nf!)) || (e.Article.Slug != null && e.Article.Slug.ToLower().Contains(nf!)) || (e.Article.Summary != null && e.Article.Summary.ToLower().Contains(nf!)) || (e.Article.Content != null && e.Article.Content.ToLower().Contains(nf!)) || (e.Article.ThumbnailUrl != null && e.Article.ThumbnailUrl.ToLower().Contains(nf!)) || (e.Article.CoverImageUrl != null && e.Article.CoverImageUrl.ToLower().Contains(nf!)) || (e.Article.AuthorName != null && e.Article.AuthorName.ToLower().Contains(nf!)) || (e.Article.Source != null && e.Article.Source.ToLower().Contains(nf!)) || (e.Article.SourceUrl != null && e.Article.SourceUrl.ToLower().Contains(nf!)) || (e.Article.SeoTitle != null && e.Article.SeoTitle.ToLower().Contains(nf!)) || (e.Article.SeoDescription != null && e.Article.SeoDescription.ToLower().Contains(nf!)) || (e.Article.SeoKeywords != null && e.Article.SeoKeywords.ToLower().Contains(nf!))).WhereIf(nTitle != null, e => e.Article.Title != null && e.Article.Title.ToLower().Contains(nTitle!)).WhereIf(nSlug != null, e => e.Article.Slug != null && e.Article.Slug.ToLower().Contains(nSlug!)).WhereIf(nSummary != null, e => e.Article.Summary != null && e.Article.Summary.ToLower().Contains(nSummary!)).WhereIf(nContent != null, e => e.Article.Content != null && e.Article.Content.ToLower().Contains(nContent!)).WhereIf(nThumbnailUrl != null, e => e.Article.ThumbnailUrl != null && e.Article.ThumbnailUrl.ToLower().Contains(nThumbnailUrl!)).WhereIf(nCoverImageUrl != null, e => e.Article.CoverImageUrl != null && e.Article.CoverImageUrl.ToLower().Contains(nCoverImageUrl!)).WhereIf(type.HasValue, e => e.Article.Type == type).WhereIf(nAuthorName != null, e => e.Article.AuthorName != null && e.Article.AuthorName.ToLower().Contains(nAuthorName!)).WhereIf(nSource != null, e => e.Article.Source != null && e.Article.Source.ToLower().Contains(nSource!)).WhereIf(nSourceUrl != null, e => e.Article.SourceUrl != null && e.Article.SourceUrl.ToLower().Contains(nSourceUrl!)).WhereIf(status.HasValue, e => e.Article.Status == status).WhereIf(publishedAtMin.HasValue, e => e.Article.PublishedAt >= publishedAtMin!.Value).WhereIf(publishedAtMax.HasValue, e => e.Article.PublishedAt <= publishedAtMax!.Value).WhereIf(isFeatured.HasValue, e => e.Article.IsFeatured == isFeatured).WhereIf(isHot.HasValue, e => e.Article.IsHot == isHot).WhereIf(isTrending.HasValue, e => e.Article.IsTrending == isTrending).WhereIf(viewCountMin.HasValue, e => e.Article.ViewCount >= viewCountMin!.Value).WhereIf(viewCountMax.HasValue, e => e.Article.ViewCount <= viewCountMax!.Value).WhereIf(likeCountMin.HasValue, e => e.Article.LikeCount >= likeCountMin!.Value).WhereIf(likeCountMax.HasValue, e => e.Article.LikeCount <= likeCountMax!.Value).WhereIf(shareCountMin.HasValue, e => e.Article.ShareCount >= shareCountMin!.Value).WhereIf(shareCountMax.HasValue, e => e.Article.ShareCount <= shareCountMax!.Value).WhereIf(commentCountMin.HasValue, e => e.Article.CommentCount >= commentCountMin!.Value).WhereIf(commentCountMax.HasValue, e => e.Article.CommentCount <= commentCountMax!.Value).WhereIf(readingTimeMin.HasValue, e => e.Article.ReadingTime >= readingTimeMin!.Value).WhereIf(readingTimeMax.HasValue, e => e.Article.ReadingTime <= readingTimeMax!.Value).WhereIf(nSeoTitle != null, e => e.Article.SeoTitle != null && e.Article.SeoTitle.ToLower().Contains(nSeoTitle!)).WhereIf(nSeoDescription != null, e => e.Article.SeoDescription != null && e.Article.SeoDescription.ToLower().Contains(nSeoDescription!)).WhereIf(nSeoKeywords != null, e => e.Article.SeoKeywords != null && e.Article.SeoKeywords.ToLower().Contains(nSeoKeywords!)).WhereIf(articleCategoryId != null && articleCategoryId != Guid.Empty, e => e.ArticleCategory != null && e.ArticleCategory.Id == articleCategoryId);
    }

    public virtual async Task<List<Article>> GetListAsync(string? filterText = null, string? title = null, string? slug = null, string? summary = null, string? content = null, string? thumbnailUrl = null, string? coverImageUrl = null, ArticleType? type = null, string? authorName = null, string? source = null, string? sourceUrl = null, ArticleStatus? status = null, DateTime? publishedAtMin = null, DateTime? publishedAtMax = null, bool? isFeatured = null, bool? isHot = null, bool? isTrending = null, int? viewCountMin = null, int? viewCountMax = null, int? likeCountMin = null, int? likeCountMax = null, int? shareCountMin = null, int? shareCountMax = null, int? commentCountMin = null, int? commentCountMax = null, int? readingTimeMin = null, int? readingTimeMax = null, string? seoTitle = null, string? seoDescription = null, string? seoKeywords = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, title, slug, summary, content, thumbnailUrl, coverImageUrl, type, authorName, source, sourceUrl, status, publishedAtMin, publishedAtMax, isFeatured, isHot, isTrending, viewCountMin, viewCountMax, likeCountMin, likeCountMax, shareCountMin, shareCountMax, commentCountMin, commentCountMax, readingTimeMin, readingTimeMax, seoTitle, seoDescription, seoKeywords);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? title = null, string? slug = null, string? summary = null, string? content = null, string? thumbnailUrl = null, string? coverImageUrl = null, ArticleType? type = null, string? authorName = null, string? source = null, string? sourceUrl = null, ArticleStatus? status = null, DateTime? publishedAtMin = null, DateTime? publishedAtMax = null, bool? isFeatured = null, bool? isHot = null, bool? isTrending = null, int? viewCountMin = null, int? viewCountMax = null, int? likeCountMin = null, int? likeCountMax = null, int? shareCountMin = null, int? shareCountMax = null, int? commentCountMin = null, int? commentCountMax = null, int? readingTimeMin = null, int? readingTimeMax = null, string? seoTitle = null, string? seoDescription = null, string? seoKeywords = null, Guid? articleCategoryId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, title, slug, summary, content, thumbnailUrl, coverImageUrl, type, authorName, source, sourceUrl, status, publishedAtMin, publishedAtMax, isFeatured, isHot, isTrending, viewCountMin, viewCountMax, likeCountMin, likeCountMax, shareCountMin, shareCountMax, commentCountMin, commentCountMax, readingTimeMin, readingTimeMax, seoTitle, seoDescription, seoKeywords, articleCategoryId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Article> ApplyFilter(IQueryable<Article> query, string? filterText = null, string? title = null, string? slug = null, string? summary = null, string? content = null, string? thumbnailUrl = null, string? coverImageUrl = null, ArticleType? type = null, string? authorName = null, string? source = null, string? sourceUrl = null, ArticleStatus? status = null, DateTime? publishedAtMin = null, DateTime? publishedAtMax = null, bool? isFeatured = null, bool? isHot = null, bool? isTrending = null, int? viewCountMin = null, int? viewCountMax = null, int? likeCountMin = null, int? likeCountMax = null, int? shareCountMin = null, int? shareCountMax = null, int? commentCountMin = null, int? commentCountMax = null, int? readingTimeMin = null, int? readingTimeMax = null, string? seoTitle = null, string? seoDescription = null, string? seoKeywords = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nTitle = MasterDataStringSearch.NormalizeForContains(title);
        var nSlug = MasterDataStringSearch.NormalizeForContains(slug);
        var nSummary = MasterDataStringSearch.NormalizeForContains(summary);
        var nContent = MasterDataStringSearch.NormalizeForContains(content);
        var nThumbnailUrl = MasterDataStringSearch.NormalizeForContains(thumbnailUrl);
        var nCoverImageUrl = MasterDataStringSearch.NormalizeForContains(coverImageUrl);
        var nAuthorName = MasterDataStringSearch.NormalizeForContains(authorName);
        var nSource = MasterDataStringSearch.NormalizeForContains(source);
        var nSourceUrl = MasterDataStringSearch.NormalizeForContains(sourceUrl);
        var nSeoTitle = MasterDataStringSearch.NormalizeForContains(seoTitle);
        var nSeoDescription = MasterDataStringSearch.NormalizeForContains(seoDescription);
        var nSeoKeywords = MasterDataStringSearch.NormalizeForContains(seoKeywords);
        return query.WhereIf(nf != null, e => (e.Title != null && e.Title.ToLower().Contains(nf!)) || (e.Slug != null && e.Slug.ToLower().Contains(nf!)) || (e.Summary != null && e.Summary.ToLower().Contains(nf!)) || (e.Content != null && e.Content.ToLower().Contains(nf!)) || (e.ThumbnailUrl != null && e.ThumbnailUrl.ToLower().Contains(nf!)) || (e.CoverImageUrl != null && e.CoverImageUrl.ToLower().Contains(nf!)) || (e.AuthorName != null && e.AuthorName.ToLower().Contains(nf!)) || (e.Source != null && e.Source.ToLower().Contains(nf!)) || (e.SourceUrl != null && e.SourceUrl.ToLower().Contains(nf!)) || (e.SeoTitle != null && e.SeoTitle.ToLower().Contains(nf!)) || (e.SeoDescription != null && e.SeoDescription.ToLower().Contains(nf!)) || (e.SeoKeywords != null && e.SeoKeywords.ToLower().Contains(nf!))).WhereIf(nTitle != null, e => e.Title != null && e.Title.ToLower().Contains(nTitle!)).WhereIf(nSlug != null, e => e.Slug != null && e.Slug.ToLower().Contains(nSlug!)).WhereIf(nSummary != null, e => e.Summary != null && e.Summary.ToLower().Contains(nSummary!)).WhereIf(nContent != null, e => e.Content != null && e.Content.ToLower().Contains(nContent!)).WhereIf(nThumbnailUrl != null, e => e.ThumbnailUrl != null && e.ThumbnailUrl.ToLower().Contains(nThumbnailUrl!)).WhereIf(nCoverImageUrl != null, e => e.CoverImageUrl != null && e.CoverImageUrl.ToLower().Contains(nCoverImageUrl!)).WhereIf(type.HasValue, e => e.Type == type).WhereIf(nAuthorName != null, e => e.AuthorName != null && e.AuthorName.ToLower().Contains(nAuthorName!)).WhereIf(nSource != null, e => e.Source != null && e.Source.ToLower().Contains(nSource!)).WhereIf(nSourceUrl != null, e => e.SourceUrl != null && e.SourceUrl.ToLower().Contains(nSourceUrl!)).WhereIf(status.HasValue, e => e.Status == status).WhereIf(publishedAtMin.HasValue, e => e.PublishedAt >= publishedAtMin!.Value).WhereIf(publishedAtMax.HasValue, e => e.PublishedAt <= publishedAtMax!.Value).WhereIf(isFeatured.HasValue, e => e.IsFeatured == isFeatured).WhereIf(isHot.HasValue, e => e.IsHot == isHot).WhereIf(isTrending.HasValue, e => e.IsTrending == isTrending).WhereIf(viewCountMin.HasValue, e => e.ViewCount >= viewCountMin!.Value).WhereIf(viewCountMax.HasValue, e => e.ViewCount <= viewCountMax!.Value).WhereIf(likeCountMin.HasValue, e => e.LikeCount >= likeCountMin!.Value).WhereIf(likeCountMax.HasValue, e => e.LikeCount <= likeCountMax!.Value).WhereIf(shareCountMin.HasValue, e => e.ShareCount >= shareCountMin!.Value).WhereIf(shareCountMax.HasValue, e => e.ShareCount <= shareCountMax!.Value).WhereIf(commentCountMin.HasValue, e => e.CommentCount >= commentCountMin!.Value).WhereIf(commentCountMax.HasValue, e => e.CommentCount <= commentCountMax!.Value).WhereIf(readingTimeMin.HasValue, e => e.ReadingTime >= readingTimeMin!.Value).WhereIf(readingTimeMax.HasValue, e => e.ReadingTime <= readingTimeMax!.Value).WhereIf(nSeoTitle != null, e => e.SeoTitle != null && e.SeoTitle.ToLower().Contains(nSeoTitle!)).WhereIf(nSeoDescription != null, e => e.SeoDescription != null && e.SeoDescription.ToLower().Contains(nSeoDescription!)).WhereIf(nSeoKeywords != null, e => e.SeoKeywords != null && e.SeoKeywords.ToLower().Contains(nSeoKeywords!));
    }
}