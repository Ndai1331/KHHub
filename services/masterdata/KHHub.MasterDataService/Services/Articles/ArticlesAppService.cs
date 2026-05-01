using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Articles;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;
using KHHub.MasterDataService.Data.Articles;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.Articles;

[Authorize(MasterDataServicePermissions.Articles.Default)]
public abstract class ArticlesAppServiceBase : ApplicationService
{
    protected IDistributedCache<ArticleDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IArticleRepository _articleRepository;
    protected ArticleManager _articleManager;
    protected IRepository<KHHub.MasterDataService.Entities.ArticleCategories.ArticleCategory, Guid> _articleCategoryRepository;

    public ArticlesAppServiceBase(IArticleRepository articleRepository, ArticleManager articleManager, IDistributedCache<ArticleDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.ArticleCategories.ArticleCategory, Guid> articleCategoryRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _articleRepository = articleRepository;
        _articleManager = articleManager;
        _articleCategoryRepository = articleCategoryRepository;
    }

    public virtual async Task<PagedResultDto<ArticleWithNavigationPropertiesDto>> GetListAsync(GetArticlesInput input)
    {
        var totalCount = await _articleRepository.GetCountAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Content, input.ThumbnailUrl, input.CoverImageUrl, input.Type, input.AuthorName, input.Source, input.SourceUrl, input.Status, input.PublishedAtMin, input.PublishedAtMax, input.IsFeatured, input.IsHot, input.IsTrending, input.ViewCountMin, input.ViewCountMax, input.LikeCountMin, input.LikeCountMax, input.ShareCountMin, input.ShareCountMax, input.CommentCountMin, input.CommentCountMax, input.ReadingTimeMin, input.ReadingTimeMax, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ArticleCategoryId);
        var items = await _articleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Content, input.ThumbnailUrl, input.CoverImageUrl, input.Type, input.AuthorName, input.Source, input.SourceUrl, input.Status, input.PublishedAtMin, input.PublishedAtMax, input.IsFeatured, input.IsHot, input.IsTrending, input.ViewCountMin, input.ViewCountMax, input.LikeCountMin, input.LikeCountMax, input.ShareCountMin, input.ShareCountMax, input.CommentCountMin, input.CommentCountMax, input.ReadingTimeMin, input.ReadingTimeMax, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ArticleCategoryId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<ArticleWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ArticleWithNavigationProperties>, List<ArticleWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<ArticleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleWithNavigationProperties, ArticleWithNavigationPropertiesDto>(await _articleRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<ArticleDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Article, ArticleDto>(await _articleRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetArticleCategoryLookupAsync(LookupRequestDto input)
    {
        var normalizedFilter = input.Filter?.Trim();
        var query = (await _articleCategoryRepository.GetQueryableAsync())
            .WhereIf(
                !string.IsNullOrWhiteSpace(normalizedFilter),
                x => x.Name != null && x.Name.ToUpper().Contains(normalizedFilter!.ToUpper())
            );
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.ArticleCategories.ArticleCategory>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.Articles.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _articleRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.Articles.Create)]
    public virtual async Task<ArticleDto> CreateAsync(ArticleCreateDto input)
    {
        if (input.ArticleCategoryId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["ArticleCategory"]]);
        }

        var article = await _articleManager.CreateAsync(input.ArticleCategoryId, input.Title, input.Slug, input.Summary, input.Content, input.Type, input.AuthorName, input.Status, input.PublishedAt, input.IsFeatured, input.IsHot, input.IsTrending, input.ViewCount, input.LikeCount, input.ShareCount, input.CommentCount, input.ReadingTime, input.SeoTitle, input.ThumbnailUrl, input.CoverImageUrl, input.Source, input.SourceUrl, input.SeoDescription, input.SeoKeywords);
        return ObjectMapper.Map<Article, ArticleDto>(article);
    }

    [Authorize(MasterDataServicePermissions.Articles.Edit)]
    public virtual async Task<ArticleDto> UpdateAsync(Guid id, ArticleUpdateDto input)
    {
        if (input.ArticleCategoryId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["ArticleCategory"]]);
        }

        var article = await _articleManager.UpdateAsync(id, input.ArticleCategoryId, input.Title, input.Slug, input.Summary, input.Content, input.Type, input.AuthorName, input.Status, input.PublishedAt, input.IsFeatured, input.IsHot, input.IsTrending, input.ViewCount, input.LikeCount, input.ShareCount, input.CommentCount, input.ReadingTime, input.SeoTitle, input.ThumbnailUrl, input.CoverImageUrl, input.Source, input.SourceUrl, input.SeoDescription, input.SeoKeywords, input.ConcurrencyStamp);
        return ObjectMapper.Map<Article, ArticleDto>(article);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var articles = await _articleRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Content, input.ThumbnailUrl, input.CoverImageUrl, input.Type, input.AuthorName, input.Source, input.SourceUrl, input.Status, input.PublishedAtMin, input.PublishedAtMax, input.IsFeatured, input.IsHot, input.IsTrending, input.ViewCountMin, input.ViewCountMax, input.LikeCountMin, input.LikeCountMax, input.ShareCountMin, input.ShareCountMax, input.CommentCountMin, input.CommentCountMax, input.ReadingTimeMin, input.ReadingTimeMax, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ArticleCategoryId);
        var items = articles.Select(item => new { Title = item.Article.Title, Slug = item.Article.Slug, Summary = item.Article.Summary, Content = item.Article.Content, ThumbnailUrl = item.Article.ThumbnailUrl, CoverImageUrl = item.Article.CoverImageUrl, Type = item.Article.Type, AuthorName = item.Article.AuthorName, Source = item.Article.Source, SourceUrl = item.Article.SourceUrl, Status = item.Article.Status, PublishedAt = item.Article.PublishedAt, IsFeatured = item.Article.IsFeatured, IsHot = item.Article.IsHot, IsTrending = item.Article.IsTrending, ViewCount = item.Article.ViewCount, LikeCount = item.Article.LikeCount, ShareCount = item.Article.ShareCount, CommentCount = item.Article.CommentCount, ReadingTime = item.Article.ReadingTime, SeoTitle = item.Article.SeoTitle, SeoDescription = item.Article.SeoDescription, SeoKeywords = item.Article.SeoKeywords, ArticleCategory = item.ArticleCategory?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "Articles.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.Articles.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> articleIds)
    {
        await _articleRepository.DeleteManyAsync(articleIds);
    }

    [Authorize(MasterDataServicePermissions.Articles.Delete)]
    public virtual async Task DeleteAllAsync(GetArticlesInput input)
    {
        await _articleRepository.DeleteAllAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Content, input.ThumbnailUrl, input.CoverImageUrl, input.Type, input.AuthorName, input.Source, input.SourceUrl, input.Status, input.PublishedAtMin, input.PublishedAtMax, input.IsFeatured, input.IsHot, input.IsTrending, input.ViewCountMin, input.ViewCountMax, input.LikeCountMin, input.LikeCountMax, input.ShareCountMin, input.ShareCountMax, input.CommentCountMin, input.CommentCountMax, input.ReadingTimeMin, input.ReadingTimeMax, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ArticleCategoryId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new ArticleDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}