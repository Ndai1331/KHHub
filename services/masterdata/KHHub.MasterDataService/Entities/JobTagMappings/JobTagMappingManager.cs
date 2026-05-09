using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.JobTagMappings;

namespace KHHub.MasterDataService.Entities.JobTagMappings;

public abstract class JobTagMappingManagerBase : DomainService
{
    protected IJobTagMappingRepository _jobTagMappingRepository;

    public JobTagMappingManagerBase(IJobTagMappingRepository jobTagMappingRepository)
    {
        _jobTagMappingRepository = jobTagMappingRepository;
    }

    public virtual async Task<JobTagMapping> CreateAsync(Guid jobTagId, Guid jobId, bool isPrimary, string? sortOrder = null)
    {
        Check.NotNull(jobTagId, nameof(jobTagId));
        Check.NotNull(jobId, nameof(jobId));
        var jobTagMapping = new JobTagMapping(GuidGenerator.Create(), jobTagId, jobId, isPrimary, sortOrder);
        return await _jobTagMappingRepository.InsertAsync(jobTagMapping);
    }

    public virtual async Task<JobTagMapping> UpdateAsync(Guid id, Guid jobTagId, Guid jobId, bool isPrimary, string? sortOrder = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(jobTagId, nameof(jobTagId));
        Check.NotNull(jobId, nameof(jobId));
        var jobTagMapping = await _jobTagMappingRepository.GetAsync(id);
        jobTagMapping.JobTagId = jobTagId;
        jobTagMapping.JobId = jobId;
        jobTagMapping.IsPrimary = isPrimary;
        jobTagMapping.SortOrder = sortOrder;
        jobTagMapping.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobTagMappingRepository.UpdateAsync(jobTagMapping);
    }
}