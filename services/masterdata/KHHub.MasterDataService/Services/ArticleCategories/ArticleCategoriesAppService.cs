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
using KHHub.MasterDataService.Services.ArticleCategories;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;
using KHHub.MasterDataService.Data.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.ArticleCategories;

[Authorize(MasterDataServicePermissions.ArticleCategories.Default)]
public abstract class ArticleCategoriesAppServiceBase : ApplicationService
{
    protected IDistributedCache<ArticleCategoryDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IArticleCategoryRepository _articleCategoryRepository;
    protected ArticleCategoryManager _articleCategoryManager;

    public ArticleCategoriesAppServiceBase(IArticleCategoryRepository articleCategoryRepository, ArticleCategoryManager articleCategoryManager, IDistributedCache<ArticleCategoryDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _articleCategoryRepository = articleCategoryRepository;
        _articleCategoryManager = articleCategoryManager;
    }

    public virtual async Task<PagedResultDto<ArticleCategoryDto>> GetListAsync(GetArticleCategoriesInput input)
    {
        var totalCount = await _articleCategoryRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive, input.ThumbnailUrl);
        var items = await _articleCategoryRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive, input.ThumbnailUrl, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<ArticleCategoryDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ArticleCategory>, List<ArticleCategoryDto>>(items)
        };
    }

    public virtual async Task<ArticleCategoryDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleCategory, ArticleCategoryDto>(await _articleCategoryRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.ArticleCategories.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _articleCategoryRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.ArticleCategories.Create)]
    public virtual async Task<ArticleCategoryDto> CreateAsync(ArticleCategoryCreateDto input)
    {
        var articleCategory = await _articleCategoryManager.CreateAsync(input.Name, input.Slug, input.ParentId, input.DisplayOrder, input.IsActive, input.Description, input.Icon, input.ThumbnailUrl);
        return ObjectMapper.Map<ArticleCategory, ArticleCategoryDto>(articleCategory);
    }

    [Authorize(MasterDataServicePermissions.ArticleCategories.Edit)]
    public virtual async Task<ArticleCategoryDto> UpdateAsync(Guid id, ArticleCategoryUpdateDto input)
    {
        var articleCategory = await _articleCategoryManager.UpdateAsync(id, input.Name, input.Slug, input.ParentId, input.DisplayOrder, input.IsActive, input.Description, input.Icon, input.ThumbnailUrl, input.ConcurrencyStamp);
        return ObjectMapper.Map<ArticleCategory, ArticleCategoryDto>(articleCategory);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleCategoryExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _articleCategoryRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive, input.ThumbnailUrl);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ArticleCategory>, List<ArticleCategoryExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "ArticleCategories.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.ArticleCategories.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> articlecategoryIds)
    {
        await _articleCategoryRepository.DeleteManyAsync(articlecategoryIds);
    }

    [Authorize(MasterDataServicePermissions.ArticleCategories.Delete)]
    public virtual async Task DeleteAllAsync(GetArticleCategoriesInput input)
    {
        await _articleCategoryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive, input.ThumbnailUrl);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new ArticleCategoryDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}