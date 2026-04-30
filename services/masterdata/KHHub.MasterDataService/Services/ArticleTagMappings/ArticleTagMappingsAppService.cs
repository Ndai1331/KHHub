using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.ArticleTags;
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
using KHHub.MasterDataService.Services.ArticleTagMappings;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using KHHub.MasterDataService.Data.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.ArticleTagMappings;

[Authorize(MasterDataServicePermissions.ArticleTagMappings.Default)]
public abstract class ArticleTagMappingsAppServiceBase : ApplicationService
{
    protected IDistributedCache<ArticleTagMappingDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IArticleTagMappingRepository _articleTagMappingRepository;
    protected ArticleTagMappingManager _articleTagMappingManager;
    protected IRepository<KHHub.MasterDataService.Entities.ArticleTags.ArticleTag, Guid> _articleTagRepository;
    protected IRepository<KHHub.MasterDataService.Entities.Articles.Article, Guid> _articleRepository;

    public ArticleTagMappingsAppServiceBase(IArticleTagMappingRepository articleTagMappingRepository, ArticleTagMappingManager articleTagMappingManager, IDistributedCache<ArticleTagMappingDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.ArticleTags.ArticleTag, Guid> articleTagRepository, IRepository<KHHub.MasterDataService.Entities.Articles.Article, Guid> articleRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _articleTagMappingRepository = articleTagMappingRepository;
        _articleTagMappingManager = articleTagMappingManager;
        _articleTagRepository = articleTagRepository;
        _articleRepository = articleRepository;
    }

    public virtual async Task<PagedResultDto<ArticleTagMappingWithNavigationPropertiesDto>> GetListAsync(GetArticleTagMappingsInput input)
    {
        var totalCount = await _articleTagMappingRepository.GetCountAsync(input.FilterText, input.IsPrimary, input.OrderMin, input.OrderMax, input.ArticleTagId, input.ArticleId);
        var items = await _articleTagMappingRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsPrimary, input.OrderMin, input.OrderMax, input.ArticleTagId, input.ArticleId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<ArticleTagMappingWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ArticleTagMappingWithNavigationProperties>, List<ArticleTagMappingWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<ArticleTagMappingWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleTagMappingWithNavigationProperties, ArticleTagMappingWithNavigationPropertiesDto>(await _articleTagMappingRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<ArticleTagMappingDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleTagMapping, ArticleTagMappingDto>(await _articleTagMappingRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetArticleTagLookupAsync(LookupRequestDto input)
    {
        var query = (await _articleTagRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Slug != null && x.Slug.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.ArticleTags.ArticleTag>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Slug }).ToList()
        };
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetArticleLookupAsync(LookupRequestDto input)
    {
        var query = (await _articleRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Title != null && x.Title.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Articles.Article>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Title }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.ArticleTagMappings.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _articleTagMappingRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.ArticleTagMappings.Create)]
    public virtual async Task<ArticleTagMappingDto> CreateAsync(ArticleTagMappingCreateDto input)
    {
        if (input.ArticleTagId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["ArticleTag"]]);
        }

        if (input.ArticleId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Article"]]);
        }

        var articleTagMapping = await _articleTagMappingManager.CreateAsync(input.ArticleTagId, input.ArticleId, input.IsPrimary, input.Order);
        return ObjectMapper.Map<ArticleTagMapping, ArticleTagMappingDto>(articleTagMapping);
    }

    [Authorize(MasterDataServicePermissions.ArticleTagMappings.Edit)]
    public virtual async Task<ArticleTagMappingDto> UpdateAsync(Guid id, ArticleTagMappingUpdateDto input)
    {
        if (input.ArticleTagId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["ArticleTag"]]);
        }

        if (input.ArticleId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Article"]]);
        }

        var articleTagMapping = await _articleTagMappingManager.UpdateAsync(id, input.ArticleTagId, input.ArticleId, input.IsPrimary, input.Order, input.ConcurrencyStamp);
        return ObjectMapper.Map<ArticleTagMapping, ArticleTagMappingDto>(articleTagMapping);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleTagMappingExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var articleTagMappings = await _articleTagMappingRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsPrimary, input.OrderMin, input.OrderMax, input.ArticleTagId, input.ArticleId);
        var items = articleTagMappings.Select(item => new { IsPrimary = item.ArticleTagMapping.IsPrimary, Order = item.ArticleTagMapping.Order, ArticleTag = item.ArticleTag?.Slug, Article = item.Article?.Title, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "ArticleTagMappings.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.ArticleTagMappings.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> articletagmappingIds)
    {
        await _articleTagMappingRepository.DeleteManyAsync(articletagmappingIds);
    }

    [Authorize(MasterDataServicePermissions.ArticleTagMappings.Delete)]
    public virtual async Task DeleteAllAsync(GetArticleTagMappingsInput input)
    {
        await _articleTagMappingRepository.DeleteAllAsync(input.FilterText, input.IsPrimary, input.OrderMin, input.OrderMax, input.ArticleTagId, input.ArticleId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new ArticleTagMappingDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}