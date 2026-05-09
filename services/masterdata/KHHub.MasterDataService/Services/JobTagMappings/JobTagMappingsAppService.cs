using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Entities.JobTags;
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
using KHHub.MasterDataService.Services.JobTagMappings;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;
using KHHub.MasterDataService.Data.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.JobTagMappings;

[Authorize(MasterDataServicePermissions.JobTagMappings.Default)]
public abstract class JobTagMappingsAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobTagMappingDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobTagMappingRepository _jobTagMappingRepository;
    protected JobTagMappingManager _jobTagMappingManager;
    protected IRepository<KHHub.MasterDataService.Entities.JobTags.JobTag, Guid> _jobTagRepository;
    protected IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> _jobRepository;

    public JobTagMappingsAppServiceBase(IJobTagMappingRepository jobTagMappingRepository, JobTagMappingManager jobTagMappingManager, IDistributedCache<JobTagMappingDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.JobTags.JobTag, Guid> jobTagRepository, IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> jobRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobTagMappingRepository = jobTagMappingRepository;
        _jobTagMappingManager = jobTagMappingManager;
        _jobTagRepository = jobTagRepository;
        _jobRepository = jobRepository;
    }

    public virtual async Task<PagedResultDto<JobTagMappingWithNavigationPropertiesDto>> GetListAsync(GetJobTagMappingsInput input)
    {
        var totalCount = await _jobTagMappingRepository.GetCountAsync(input.FilterText, input.IsPrimary, input.SortOrder, input.JobTagId, input.JobId);
        var items = await _jobTagMappingRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsPrimary, input.SortOrder, input.JobTagId, input.JobId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobTagMappingWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobTagMappingWithNavigationProperties>, List<JobTagMappingWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<JobTagMappingWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<JobTagMappingWithNavigationProperties, JobTagMappingWithNavigationPropertiesDto>(await _jobTagMappingRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<JobTagMappingDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<JobTagMapping, JobTagMappingDto>(await _jobTagMappingRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetJobTagLookupAsync(LookupRequestDto input)
    {
        var query = (await _jobTagRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.JobTags.JobTag>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
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

    [Authorize(MasterDataServicePermissions.JobTagMappings.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobTagMappingRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.JobTagMappings.Create)]
    public virtual async Task<JobTagMappingDto> CreateAsync(JobTagMappingCreateDto input)
    {
        if (input.JobTagId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["JobTag"]]);
        }

        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobTagMapping = await _jobTagMappingManager.CreateAsync(input.JobTagId, input.JobId, input.IsPrimary, input.SortOrder);
        return ObjectMapper.Map<JobTagMapping, JobTagMappingDto>(jobTagMapping);
    }

    [Authorize(MasterDataServicePermissions.JobTagMappings.Edit)]
    public virtual async Task<JobTagMappingDto> UpdateAsync(Guid id, JobTagMappingUpdateDto input)
    {
        if (input.JobTagId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["JobTag"]]);
        }

        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobTagMapping = await _jobTagMappingManager.UpdateAsync(id, input.JobTagId, input.JobId, input.IsPrimary, input.SortOrder, input.ConcurrencyStamp);
        return ObjectMapper.Map<JobTagMapping, JobTagMappingDto>(jobTagMapping);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobTagMappingExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var jobTagMappings = await _jobTagMappingRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsPrimary, input.SortOrder, input.JobTagId, input.JobId);
        var items = jobTagMappings.Select(item => new { IsPrimary = item.JobTagMapping.IsPrimary, SortOrder = item.JobTagMapping.SortOrder, JobTag = item.JobTag?.Name, Job = item.Job?.Title, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "JobTagMappings.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.JobTagMappings.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobtagmappingIds)
    {
        await _jobTagMappingRepository.DeleteManyAsync(jobtagmappingIds);
    }

    [Authorize(MasterDataServicePermissions.JobTagMappings.Delete)]
    public virtual async Task DeleteAllAsync(GetJobTagMappingsInput input)
    {
        await _jobTagMappingRepository.DeleteAllAsync(input.FilterText, input.IsPrimary, input.SortOrder, input.JobTagId, input.JobId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobTagMappingDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}