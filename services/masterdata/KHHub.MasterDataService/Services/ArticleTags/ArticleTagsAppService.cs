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
using KHHub.MasterDataService.Services.ArticleTags;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;
using KHHub.MasterDataService.Data.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.ArticleTags;

[Authorize(MasterDataServicePermissions.ArticleTags.Default)]
public abstract class ArticleTagsAppServiceBase : ApplicationService
{
    protected IDistributedCache<ArticleTagDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IArticleTagRepository _articleTagRepository;
    protected ArticleTagManager _articleTagManager;

    public ArticleTagsAppServiceBase(IArticleTagRepository articleTagRepository, ArticleTagManager articleTagManager, IDistributedCache<ArticleTagDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _articleTagRepository = articleTagRepository;
        _articleTagManager = articleTagManager;
    }

    public virtual async Task<PagedResultDto<ArticleTagDto>> GetListAsync(GetArticleTagsInput input)
    {
        var totalCount = await _articleTagRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
        var items = await _articleTagRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<ArticleTagDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ArticleTag>, List<ArticleTagDto>>(items)
        };
    }

    public virtual async Task<ArticleTagDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleTag, ArticleTagDto>(await _articleTagRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.ArticleTags.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _articleTagRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.ArticleTags.Create)]
    public virtual async Task<ArticleTagDto> CreateAsync(ArticleTagCreateDto input)
    {
        var articleTag = await _articleTagManager.CreateAsync(input.Name, input.Slug, input.UsageCount, input.Description);
        return ObjectMapper.Map<ArticleTag, ArticleTagDto>(articleTag);
    }

    [Authorize(MasterDataServicePermissions.ArticleTags.Edit)]
    public virtual async Task<ArticleTagDto> UpdateAsync(Guid id, ArticleTagUpdateDto input)
    {
        var articleTag = await _articleTagManager.UpdateAsync(id, input.Name, input.Slug, input.UsageCount, input.Description, input.ConcurrencyStamp);
        return ObjectMapper.Map<ArticleTag, ArticleTagDto>(articleTag);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleTagExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _articleTagRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<ArticleTag>, List<ArticleTagExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "ArticleTags.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.ArticleTags.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> articletagIds)
    {
        await _articleTagRepository.DeleteManyAsync(articletagIds);
    }

    [Authorize(MasterDataServicePermissions.ArticleTags.Delete)]
    public virtual async Task DeleteAllAsync(GetArticleTagsInput input)
    {
        await _articleTagRepository.DeleteAllAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new ArticleTagDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}