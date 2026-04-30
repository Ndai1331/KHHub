using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Articles;
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
using KHHub.MasterDataService.Services.ArticleViews;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;
using KHHub.MasterDataService.Data.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.ArticleViews;

[Authorize(MasterDataServicePermissions.ArticleViews.Default)]
public abstract class ArticleViewsAppServiceBase : ApplicationService
{
    protected IDistributedCache<ArticleViewDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IArticleViewRepository _articleViewRepository;
    protected ArticleViewManager _articleViewManager;
    protected IRepository<KHHub.MasterDataService.Entities.Articles.Article, Guid> _articleRepository;

    public ArticleViewsAppServiceBase(IArticleViewRepository articleViewRepository, ArticleViewManager articleViewManager, IDistributedCache<ArticleViewDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Articles.Article, Guid> articleRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _articleViewRepository = articleViewRepository;
        _articleViewManager = articleViewManager;
        _articleRepository = articleRepository;
    }

    public virtual async Task<PagedResultDto<ArticleViewWithNavigationPropertiesDto>> GetListAsync(GetArticleViewsInput input)
    {
        var totalCount = await _articleViewRepository.GetCountAsync(input.FilterText, input.IpAddress, input.Device, input.Source, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.UserId, input.ArticleId);
        var items = await _articleViewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IpAddress, input.Device, input.Source, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.UserId, input.ArticleId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<ArticleViewWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<ArticleViewWithNavigationProperties>, List<ArticleViewWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<ArticleViewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleViewWithNavigationProperties, ArticleViewWithNavigationPropertiesDto>(await _articleViewRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<ArticleViewDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<ArticleView, ArticleViewDto>(await _articleViewRepository.GetAsync(id));
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

    [Authorize(MasterDataServicePermissions.ArticleViews.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _articleViewRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.ArticleViews.Create)]
    public virtual async Task<ArticleViewDto> CreateAsync(ArticleViewCreateDto input)
    {
        if (input.ArticleId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Article"]]);
        }

        var articleView = await _articleViewManager.CreateAsync(input.ArticleId, input.ViewedAt, input.Duration, input.UserId, input.IpAddress, input.Device, input.Source);
        return ObjectMapper.Map<ArticleView, ArticleViewDto>(articleView);
    }

    [Authorize(MasterDataServicePermissions.ArticleViews.Edit)]
    public virtual async Task<ArticleViewDto> UpdateAsync(Guid id, ArticleViewUpdateDto input)
    {
        if (input.ArticleId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Article"]]);
        }

        var articleView = await _articleViewManager.UpdateAsync(id, input.ArticleId, input.ViewedAt, input.Duration, input.UserId, input.IpAddress, input.Device, input.Source, input.ConcurrencyStamp);
        return ObjectMapper.Map<ArticleView, ArticleViewDto>(articleView);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleViewExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var articleViews = await _articleViewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IpAddress, input.Device, input.Source, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.UserId, input.ArticleId);
        var items = articleViews.Select(item => new { IpAddress = item.ArticleView.IpAddress, Device = item.ArticleView.Device, Source = item.ArticleView.Source, ViewedAt = item.ArticleView.ViewedAt, Duration = item.ArticleView.Duration, UserId = item.ArticleView.UserId, Article = item.Article?.Title, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "ArticleViews.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.ArticleViews.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> articleviewIds)
    {
        await _articleViewRepository.DeleteManyAsync(articleviewIds);
    }

    [Authorize(MasterDataServicePermissions.ArticleViews.Delete)]
    public virtual async Task DeleteAllAsync(GetArticleViewsInput input)
    {
        await _articleViewRepository.DeleteAllAsync(input.FilterText, input.IpAddress, input.Device, input.Source, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.UserId, input.ArticleId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new ArticleViewDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}