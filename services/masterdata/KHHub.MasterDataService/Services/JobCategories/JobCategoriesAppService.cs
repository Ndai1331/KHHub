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
using KHHub.MasterDataService.Services.JobCategories;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.JobCategories;
using KHHub.MasterDataService.Services.Dtos.JobCategories;
using KHHub.MasterDataService.Data.JobCategories;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.JobCategories;

[Authorize(MasterDataServicePermissions.JobCategories.Default)]
public abstract class JobCategoriesAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobCategoryDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobCategoryRepository _jobCategoryRepository;
    protected JobCategoryManager _jobCategoryManager;

    public JobCategoriesAppServiceBase(IJobCategoryRepository jobCategoryRepository, JobCategoryManager jobCategoryManager, IDistributedCache<JobCategoryDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobCategoryRepository = jobCategoryRepository;
        _jobCategoryManager = jobCategoryManager;
    }

    public virtual async Task<PagedResultDto<JobCategoryDto>> GetListAsync(GetJobCategoriesInput input)
    {
        var totalCount = await _jobCategoryRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive);
        var items = await _jobCategoryRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobCategoryDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobCategory>, List<JobCategoryDto>>(items)
        };
    }

    public virtual async Task<JobCategoryDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<JobCategory, JobCategoryDto>(await _jobCategoryRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.JobCategories.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobCategoryRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.JobCategories.Create)]
    public virtual async Task<JobCategoryDto> CreateAsync(JobCategoryCreateDto input)
    {
        var jobCategory = await _jobCategoryManager.CreateAsync(input.Name, input.Slug, input.ParentId, input.DisplayOrder, input.IsActive, input.Description, input.Icon, input.Color);
        return ObjectMapper.Map<JobCategory, JobCategoryDto>(jobCategory);
    }

    [Authorize(MasterDataServicePermissions.JobCategories.Edit)]
    public virtual async Task<JobCategoryDto> UpdateAsync(Guid id, JobCategoryUpdateDto input)
    {
        var jobCategory = await _jobCategoryManager.UpdateAsync(id, input.Name, input.Slug, input.ParentId, input.DisplayOrder, input.IsActive, input.Description, input.Icon, input.Color, input.ConcurrencyStamp);
        return ObjectMapper.Map<JobCategory, JobCategoryDto>(jobCategory);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobCategoryExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _jobCategoryRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<JobCategory>, List<JobCategoryExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "JobCategories.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.JobCategories.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobcategoryIds)
    {
        await _jobCategoryRepository.DeleteManyAsync(jobcategoryIds);
    }

    [Authorize(MasterDataServicePermissions.JobCategories.Delete)]
    public virtual async Task DeleteAllAsync(GetJobCategoriesInput input)
    {
        await _jobCategoryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobCategoryDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}