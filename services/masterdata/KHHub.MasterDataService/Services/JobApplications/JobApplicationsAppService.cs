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
using KHHub.MasterDataService.Services.JobApplications;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.JobApplications;
using KHHub.MasterDataService.Services.Dtos.JobApplications;
using KHHub.MasterDataService.Data.JobApplications;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.JobApplications;

[Authorize(MasterDataServicePermissions.JobApplications.Default)]
public abstract class JobApplicationsAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobApplicationDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobApplicationRepository _jobApplicationRepository;
    protected JobApplicationManager _jobApplicationManager;
    protected IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> _jobRepository;

    public JobApplicationsAppServiceBase(IJobApplicationRepository jobApplicationRepository, JobApplicationManager jobApplicationManager, IDistributedCache<JobApplicationDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Jobs.Job, Guid> jobRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobApplicationRepository = jobApplicationRepository;
        _jobApplicationManager = jobApplicationManager;
        _jobRepository = jobRepository;
    }

    public virtual async Task<PagedResultDto<JobApplicationWithNavigationPropertiesDto>> GetListAsync(GetJobApplicationsInput input)
    {
        var totalCount = await _jobApplicationRepository.GetCountAsync(input.FilterText, input.UserId, input.FullName, input.Email, input.PhoneNumber, input.CvUrl, input.PortfolioUrl, input.AppliedAtMin, input.AppliedAtMax, input.Status, input.JobId);
        var items = await _jobApplicationRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.FullName, input.Email, input.PhoneNumber, input.CvUrl, input.PortfolioUrl, input.AppliedAtMin, input.AppliedAtMax, input.Status, input.JobId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobApplicationWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobApplicationWithNavigationProperties>, List<JobApplicationWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<JobApplicationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<JobApplicationWithNavigationProperties, JobApplicationWithNavigationPropertiesDto>(await _jobApplicationRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<JobApplicationDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<JobApplication, JobApplicationDto>(await _jobApplicationRepository.GetAsync(id));
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

    [Authorize(MasterDataServicePermissions.JobApplications.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobApplicationRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.JobApplications.Create)]
    public virtual async Task<JobApplicationDto> CreateAsync(JobApplicationCreateDto input)
    {
        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobApplication = await _jobApplicationManager.CreateAsync(input.JobId, input.FullName, input.AppliedAt, input.Status, input.UserId, input.Email, input.PhoneNumber, input.CvUrl, input.PortfolioUrl);
        return ObjectMapper.Map<JobApplication, JobApplicationDto>(jobApplication);
    }

    [Authorize(MasterDataServicePermissions.JobApplications.Edit)]
    public virtual async Task<JobApplicationDto> UpdateAsync(Guid id, JobApplicationUpdateDto input)
    {
        if (input.JobId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Job"]]);
        }

        var jobApplication = await _jobApplicationManager.UpdateAsync(id, input.JobId, input.FullName, input.AppliedAt, input.Status, input.UserId, input.Email, input.PhoneNumber, input.CvUrl, input.PortfolioUrl, input.ConcurrencyStamp);
        return ObjectMapper.Map<JobApplication, JobApplicationDto>(jobApplication);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobApplicationExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var jobApplications = await _jobApplicationRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.FullName, input.Email, input.PhoneNumber, input.CvUrl, input.PortfolioUrl, input.AppliedAtMin, input.AppliedAtMax, input.Status, input.JobId);
        var items = jobApplications.Select(item => new { UserId = item.JobApplication.UserId, FullName = item.JobApplication.FullName, Email = item.JobApplication.Email, PhoneNumber = item.JobApplication.PhoneNumber, CvUrl = item.JobApplication.CvUrl, PortfolioUrl = item.JobApplication.PortfolioUrl, AppliedAt = item.JobApplication.AppliedAt, Status = item.JobApplication.Status, Job = item.Job?.Title, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "JobApplications.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.JobApplications.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobapplicationIds)
    {
        await _jobApplicationRepository.DeleteManyAsync(jobapplicationIds);
    }

    [Authorize(MasterDataServicePermissions.JobApplications.Delete)]
    public virtual async Task DeleteAllAsync(GetJobApplicationsInput input)
    {
        await _jobApplicationRepository.DeleteAllAsync(input.FilterText, input.UserId, input.FullName, input.Email, input.PhoneNumber, input.CvUrl, input.PortfolioUrl, input.AppliedAtMin, input.AppliedAtMax, input.Status, input.JobId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobApplicationDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}