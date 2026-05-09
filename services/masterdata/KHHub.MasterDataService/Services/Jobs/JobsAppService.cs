using KHHub.MasterDataService.Entities.JobCategories;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Data.Jobs;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Services.Dtos.Shared;
using MiniExcelLibs;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Volo.Abp.Domain.Repositories;

namespace KHHub.MasterDataService.Services.Jobs;

// Do not authorize at type level — explicit attributes per action so anonymous public reads stay reliable.

public abstract class JobsAppServiceBase : ApplicationService
{
    protected IDistributedCache<JobDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IJobRepository _jobRepository;
    protected JobManager _jobManager;
    protected IRepository<KHHub.MasterDataService.Entities.Provinces.Province, Guid> _provinceRepository;
    protected IRepository<KHHub.MasterDataService.Entities.Wards.Ward, Guid> _wardRepository;
    protected IRepository<KHHub.MasterDataService.Entities.JobCategories.JobCategory, Guid> _jobCategoryRepository;

    public JobsAppServiceBase(IJobRepository jobRepository, JobManager jobManager, IDistributedCache<JobDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Provinces.Province, Guid> provinceRepository, IRepository<KHHub.MasterDataService.Entities.Wards.Ward, Guid> wardRepository, IRepository<KHHub.MasterDataService.Entities.JobCategories.JobCategory, Guid> jobCategoryRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _jobRepository = jobRepository;
        _jobManager = jobManager;
        _provinceRepository = provinceRepository;
        _wardRepository = wardRepository;
        _jobCategoryRepository = jobCategoryRepository;
    }

    [AllowAnonymous]
    public virtual async Task<PagedResultDto<JobWithNavigationPropertiesDto>> GetListAsync(GetJobsInput input)
    {
        var totalCount = await _jobRepository.GetCountAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Description, input.Requirements, input.Benefits, input.ThumbnailUrl, input.CoverImageUrl, input.EmploymentType, input.WorkMode, input.ExperienceLevel, input.SalaryMinMin, input.SalaryMinMax, input.SalaryMaxMin, input.SalaryMaxMax, input.SalaryText, input.SalaryCurrency, input.Location, input.ContactEmail, input.ContactPhone, input.ApplicationUrl, input.PublishedAtMin, input.PublishedAtMax, input.Status, input.ViewCountMin, input.ViewCountMax, input.ApplicationCountMin, input.ApplicationCountMax, input.FavoriteCountMin, input.FavoriteCountMax, input.ShareCountMin, input.ShareCountMax, input.IsFeatured, input.IsUrgent, input.IsHot, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ProvinceId, input.WardId, input.JobCategoryId);
        var items = await _jobRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Description, input.Requirements, input.Benefits, input.ThumbnailUrl, input.CoverImageUrl, input.EmploymentType, input.WorkMode, input.ExperienceLevel, input.SalaryMinMin, input.SalaryMinMax, input.SalaryMaxMin, input.SalaryMaxMax, input.SalaryText, input.SalaryCurrency, input.Location, input.ContactEmail, input.ContactPhone, input.ApplicationUrl, input.PublishedAtMin, input.PublishedAtMax, input.Status, input.ViewCountMin, input.ViewCountMax, input.ApplicationCountMin, input.ApplicationCountMax, input.FavoriteCountMin, input.FavoriteCountMax, input.ShareCountMin, input.ShareCountMax, input.IsFeatured, input.IsUrgent, input.IsHot, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ProvinceId, input.WardId, input.JobCategoryId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<JobWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<JobWithNavigationProperties>, List<JobWithNavigationPropertiesDto>>(items)
        };
    }

    [Authorize(MasterDataServicePermissions.Jobs.Default)]
    public virtual async Task<JobWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<JobWithNavigationProperties, JobWithNavigationPropertiesDto>(await _jobRepository.GetWithNavigationPropertiesAsync(id));
    }

    [Authorize(MasterDataServicePermissions.Jobs.Default)]
    public virtual async Task<JobDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Job, JobDto>(await _jobRepository.GetAsync(id));
    }

    [AllowAnonymous]
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input)
    {
        var query = (await _provinceRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Provinces.Province>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [AllowAnonymous]
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetWardLookupAsync(LookupRequestDto input)
    {
        var query = (await _wardRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Wards.Ward>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [AllowAnonymous]
    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetJobCategoryLookupAsync(LookupRequestDto input)
    {
        var query = (await _jobCategoryRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.JobCategories.JobCategory>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.Jobs.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _jobRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.Jobs.Create)]
    public virtual async Task<JobDto> CreateAsync(JobCreateDto input)
    {
        if (input.ProvinceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Province"]]);
        }

        if (input.WardId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Ward"]]);
        }

        if (input.JobCategoryId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["JobCategory"]]);
        }

        var job = await _jobManager.CreateAsync(input.ProvinceId, input.WardId, input.JobCategoryId, input.Title, input.Slug, input.Status, input.ViewCount, input.ApplicationCount, input.FavoriteCount, input.ShareCount, input.IsFeatured, input.IsUrgent, input.IsHot, input.SeoTitle, input.Summary, input.Description, input.Requirements, input.Benefits, input.ThumbnailUrl, input.CoverImageUrl, input.EmploymentType, input.WorkMode, input.ExperienceLevel, input.SalaryMin, input.SalaryMax, input.SalaryText, input.SalaryCurrency, input.Location, input.ContactEmail, input.ContactPhone, input.ApplicationUrl, input.PublishedAt, input.SeoDescription, input.SeoKeywords);
        return ObjectMapper.Map<Job, JobDto>(job);
    }

    [Authorize(MasterDataServicePermissions.Jobs.Edit)]
    public virtual async Task<JobDto> UpdateAsync(Guid id, JobUpdateDto input)
    {
        if (input.ProvinceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Province"]]);
        }

        if (input.WardId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Ward"]]);
        }

        if (input.JobCategoryId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["JobCategory"]]);
        }

        var job = await _jobManager.UpdateAsync(id, input.ProvinceId, input.WardId, input.JobCategoryId, input.Title, input.Slug, input.Status, input.ViewCount, input.ApplicationCount, input.FavoriteCount, input.ShareCount, input.IsFeatured, input.IsUrgent, input.IsHot, input.SeoTitle, input.Summary, input.Description, input.Requirements, input.Benefits, input.ThumbnailUrl, input.CoverImageUrl, input.EmploymentType, input.WorkMode, input.ExperienceLevel, input.SalaryMin, input.SalaryMax, input.SalaryText, input.SalaryCurrency, input.Location, input.ContactEmail, input.ContactPhone, input.ApplicationUrl, input.PublishedAt, input.SeoDescription, input.SeoKeywords, input.ConcurrencyStamp);
        return ObjectMapper.Map<Job, JobDto>(job);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var jobs = await _jobRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Description, input.Requirements, input.Benefits, input.ThumbnailUrl, input.CoverImageUrl, input.EmploymentType, input.WorkMode, input.ExperienceLevel, input.SalaryMinMin, input.SalaryMinMax, input.SalaryMaxMin, input.SalaryMaxMax, input.SalaryText, input.SalaryCurrency, input.Location, input.ContactEmail, input.ContactPhone, input.ApplicationUrl, input.PublishedAtMin, input.PublishedAtMax, input.Status, input.ViewCountMin, input.ViewCountMax, input.ApplicationCountMin, input.ApplicationCountMax, input.FavoriteCountMin, input.FavoriteCountMax, input.ShareCountMin, input.ShareCountMax, input.IsFeatured, input.IsUrgent, input.IsHot, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ProvinceId, input.WardId, input.JobCategoryId);
        var items = jobs.Select(item => new { Title = item.Job.Title, Slug = item.Job.Slug, Summary = item.Job.Summary, Description = item.Job.Description, Requirements = item.Job.Requirements, Benefits = item.Job.Benefits, ThumbnailUrl = item.Job.ThumbnailUrl, CoverImageUrl = item.Job.CoverImageUrl, EmploymentType = item.Job.EmploymentType, WorkMode = item.Job.WorkMode, ExperienceLevel = item.Job.ExperienceLevel, SalaryMin = item.Job.SalaryMin, SalaryMax = item.Job.SalaryMax, SalaryText = item.Job.SalaryText, SalaryCurrency = item.Job.SalaryCurrency, Location = item.Job.Location, ContactEmail = item.Job.ContactEmail, ContactPhone = item.Job.ContactPhone, ApplicationUrl = item.Job.ApplicationUrl, PublishedAt = item.Job.PublishedAt, Status = item.Job.Status, ViewCount = item.Job.ViewCount, ApplicationCount = item.Job.ApplicationCount, FavoriteCount = item.Job.FavoriteCount, ShareCount = item.Job.ShareCount, IsFeatured = item.Job.IsFeatured, IsUrgent = item.Job.IsUrgent, IsHot = item.Job.IsHot, SeoTitle = item.Job.SeoTitle, SeoDescription = item.Job.SeoDescription, SeoKeywords = item.Job.SeoKeywords, Province = item.Province?.Name, Ward = item.Ward?.Name, JobCategory = item.JobCategory?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "Jobs.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.Jobs.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> jobIds)
    {
        await _jobRepository.DeleteManyAsync(jobIds);
    }

    [Authorize(MasterDataServicePermissions.Jobs.Delete)]
    public virtual async Task DeleteAllAsync(GetJobsInput input)
    {
        await _jobRepository.DeleteAllAsync(input.FilterText, input.Title, input.Slug, input.Summary, input.Description, input.Requirements, input.Benefits, input.ThumbnailUrl, input.CoverImageUrl, input.EmploymentType, input.WorkMode, input.ExperienceLevel, input.SalaryMinMin, input.SalaryMinMax, input.SalaryMaxMin, input.SalaryMaxMax, input.SalaryText, input.SalaryCurrency, input.Location, input.ContactEmail, input.ContactPhone, input.ApplicationUrl, input.PublishedAtMin, input.PublishedAtMax, input.Status, input.ViewCountMin, input.ViewCountMax, input.ApplicationCountMin, input.ApplicationCountMax, input.FavoriteCountMin, input.FavoriteCountMax, input.ShareCountMin, input.ShareCountMax, input.IsFeatured, input.IsUrgent, input.IsHot, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.ProvinceId, input.WardId, input.JobCategoryId);
    }

    [Authorize(MasterDataServicePermissions.Jobs.Default)]
    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new JobDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}