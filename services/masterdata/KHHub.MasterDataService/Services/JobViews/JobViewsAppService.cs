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
using KHHub.MasterDataService.Services.JobViews;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.JobViews;
using KHHub.MasterDataService.Services.Dtos.JobViews;
using KHHub.MasterDataService.Data.JobViews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.JobViews;

[Authorize(MasterDataServicePermissions.JobViews.Default)]
public abstract class JobViewsAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobViewDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobViewRepository _jobViewRepository;
    protected JobViewManager _jobViewManager;
    protected IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> _jobRepository;

    public JobViewsAppServiceBase(IJobViewRepository jobViewRepository, JobViewManager jobViewManager, IDistributedCache<JobViewDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> jobRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobViewRepository = jobViewRepository;
        _jobViewManager = jobViewManager;
        _jobRepository = jobRepository;
    }

    public virtual async Task<PagedResultDto<JobViewWithNavigationPropertiesDto>> GetListAsync(GetJobViewsInput input)
    {
        var totalCount = await _jobViewRepository.GetCountAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.JobId);
        var items = await _jobViewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.JobId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobViewWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobViewWithNavigationProperties>, List<JobViewWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<JobViewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<JobViewWithNavigationProperties, JobViewWithNavigationPropertiesDto>(await _jobViewRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<JobViewDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<JobView, JobViewDto>(await _jobViewRepository.GetAsync(id));
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

    [Authorize(MasterDataServicePermissions.JobViews.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobViewRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.JobViews.Create)]
    public virtual async Task<JobViewDto> CreateAsync(JobViewCreateDto input)
    {
        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobView = await _jobViewManager.CreateAsync(input.JobId, input.ViewedAt, input.Duration, input.UserId, input.IpAddress, input.Device, input.Source);
        return ObjectMapper.Map<JobView, JobViewDto>(jobView);
    }

    [Authorize(MasterDataServicePermissions.JobViews.Edit)]
    public virtual async Task<JobViewDto> UpdateAsync(Guid id, JobViewUpdateDto input)
    {
        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobView = await _jobViewManager.UpdateAsync(id, input.JobId, input.ViewedAt, input.Duration, input.UserId, input.IpAddress, input.Device, input.Source, input.ConcurrencyStamp);
        return ObjectMapper.Map<JobView, JobViewDto>(jobView);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobViewExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var jobViews = await _jobViewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.JobId);
        var items = jobViews.Select(item => new { UserId = item.JobView.UserId, IpAddress = item.JobView.IpAddress, Device = item.JobView.Device, ViewedAt = item.JobView.ViewedAt, Duration = item.JobView.Duration, Source = item.JobView.Source, Job = item.Job?.Title, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "JobViews.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.JobViews.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobviewIds)
    {
        await _jobViewRepository.DeleteManyAsync(jobviewIds);
    }

    [Authorize(MasterDataServicePermissions.JobViews.Delete)]
    public virtual async Task DeleteAllAsync(GetJobViewsInput input)
    {
        await _jobViewRepository.DeleteAllAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.JobId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobViewDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}