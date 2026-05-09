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
using KHHub.MasterDataService.Services.JobTags;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.JobTags;
using KHHub.MasterDataService.Services.Dtos.JobTags;
using KHHub.MasterDataService.Data.JobTags;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.JobTags;

// Explicit per-action authorization so anonymous reads (GetListAsync) are not blocked.

public abstract class JobTagsAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobTagDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobTagRepository _jobTagRepository;
    protected JobTagManager _jobTagManager;

    public JobTagsAppServiceBase(IJobTagRepository jobTagRepository, JobTagManager jobTagManager, IDistributedCache<JobTagDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobTagRepository = jobTagRepository;
        _jobTagManager = jobTagManager;
    }

    [AllowAnonymous]
    public virtual async Task<PagedResultDto<JobTagDto>> GetListAsync(GetJobTagsInput input)
    {
        var totalCount = await _jobTagRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
        var items = await _jobTagRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobTagDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobTag>, List<JobTagDto>>(items)
        };
    }

    [Authorize(MasterDataServicePermissions.JobTags.Default)]
    public virtual async Task<JobTagDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<JobTag, JobTagDto>(await _jobTagRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.JobTags.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobTagRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.JobTags.Create)]
    public virtual async Task<JobTagDto> CreateAsync(JobTagCreateDto input)
    {
        var jobTag = await _jobTagManager.CreateAsync(input.Name, input.Slug, input.UsageCount, input.Description);
        return ObjectMapper.Map<JobTag, JobTagDto>(jobTag);
    }

    [Authorize(MasterDataServicePermissions.JobTags.Edit)]
    public virtual async Task<JobTagDto> UpdateAsync(Guid id, JobTagUpdateDto input)
    {
        var jobTag = await _jobTagManager.UpdateAsync(id, input.Name, input.Slug, input.UsageCount, input.Description, input.ConcurrencyStamp);
        return ObjectMapper.Map<JobTag, JobTagDto>(jobTag);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobTagExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _jobTagRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<JobTag>, List<JobTagExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "JobTags.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.JobTags.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobtagIds)
    {
        await _jobTagRepository.DeleteManyAsync(jobtagIds);
    }

    [Authorize(MasterDataServicePermissions.JobTags.Delete)]
    public virtual async Task DeleteAllAsync(GetJobTagsInput input)
    {
        await _jobTagRepository.DeleteAllAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
    }

    [Authorize(MasterDataServicePermissions.JobTags.Default)]
    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobTagDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}