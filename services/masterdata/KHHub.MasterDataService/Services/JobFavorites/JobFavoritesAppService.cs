using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Jobs;
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
using KHHub.MasterDataService.Services.JobFavorites;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.JobFavorites;
using KHHub.MasterDataService.Data.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.JobFavorites;

[Authorize(MasterDataServicePermissions.JobFavorites.Default)]
public abstract class JobFavoritesAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobFavoriteDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobFavoriteRepository _jobFavoriteRepository;
    protected JobFavoriteManager _jobFavoriteManager;
    protected IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> _jobRepository;

    public JobFavoritesAppServiceBase(IJobFavoriteRepository jobFavoriteRepository, JobFavoriteManager jobFavoriteManager, IDistributedCache<JobFavoriteDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> jobRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobFavoriteRepository = jobFavoriteRepository;
        _jobFavoriteManager = jobFavoriteManager;
        _jobRepository = jobRepository;
    }

    public virtual async Task<PagedResultDto<JobFavoriteWithNavigationPropertiesDto>> GetListAsync(GetJobFavoritesInput input)
    {
        var totalCount = await _jobFavoriteRepository.GetCountAsync(input.FilterText, input.UserId, input.JobId);
        var items = await _jobFavoriteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.JobId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobFavoriteWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobFavoriteWithNavigationProperties>, List<JobFavoriteWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<JobFavoriteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<JobFavoriteWithNavigationProperties, JobFavoriteWithNavigationPropertiesDto>(await _jobFavoriteRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<JobFavoriteDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<JobFavorite, JobFavoriteDto>(await _jobFavoriteRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetJobLookupAsync(LookupRequestDto input)
    {
        var query = (await _jobRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Title != null && x.Title.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Jobs.Job>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Title }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.JobFavorites.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobFavoriteRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.JobFavorites.Create)]
    public virtual async Task<JobFavoriteDto> CreateAsync(JobFavoriteCreateDto input)
    {
        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobFavorite = await _jobFavoriteManager.CreateAsync(input.JobId, input.UserId);
        return ObjectMapper.Map<JobFavorite, JobFavoriteDto>(jobFavorite);
    }

    [Authorize(MasterDataServicePermissions.JobFavorites.Edit)]
    public virtual async Task<JobFavoriteDto> UpdateAsync(Guid id, JobFavoriteUpdateDto input)
    {
        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobFavorite = await _jobFavoriteManager.UpdateAsync(id, input.JobId, input.UserId, input.ConcurrencyStamp);
        return ObjectMapper.Map<JobFavorite, JobFavoriteDto>(jobFavorite);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobFavoriteExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var jobFavorites = await _jobFavoriteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.JobId);
        var items = jobFavorites.Select(item => new { UserId = item.JobFavorite.UserId, Job = item.Job?.Title, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "JobFavorites.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.JobFavorites.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobfavoriteIds)
    {
        await _jobFavoriteRepository.DeleteManyAsync(jobfavoriteIds);
    }

    [Authorize(MasterDataServicePermissions.JobFavorites.Delete)]
    public virtual async Task DeleteAllAsync(GetJobFavoritesInput input)
    {
        await _jobFavoriteRepository.DeleteAllAsync(input.FilterText, input.UserId, input.JobId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobFavoriteDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}