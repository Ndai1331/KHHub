using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.JobTags;

namespace KHHub.MasterDataService.Entities.JobTags;

public abstract class JobTagManagerBase : DomainService
{
    protected IJobTagRepository _jobTagRepository;

    public JobTagManagerBase(IJobTagRepository jobTagRepository)
    {
        _jobTagRepository = jobTagRepository;
    }

    public virtual async Task<JobTag> CreateAsync(string name, string slug, int usageCount, string? description = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), JobTagConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobTagConsts.SlugMaxLength);
        Check.Length(description, nameof(description), JobTagConsts.DescriptionMaxLength);
        var jobTag = new JobTag(GuidGenerator.Create(), name, slug, usageCount, description);
        return await _jobTagRepository.InsertAsync(jobTag);
    }

    public virtual async Task<JobTag> UpdateAsync(Guid id, string name, string slug, int usageCount, string? description = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), JobTagConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobTagConsts.SlugMaxLength);
        Check.Length(description, nameof(description), JobTagConsts.DescriptionMaxLength);
        var jobTag = await _jobTagRepository.GetAsync(id);
        jobTag.Name = name;
        jobTag.Slug = slug;
        jobTag.UsageCount = usageCount;
        jobTag.Description = description;
        jobTag.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobTagRepository.UpdateAsync(jobTag);
    }
}