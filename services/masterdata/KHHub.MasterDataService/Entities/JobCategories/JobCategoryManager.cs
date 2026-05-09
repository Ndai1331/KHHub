using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.JobCategories;

namespace KHHub.MasterDataService.Entities.JobCategories;

public abstract class JobCategoryManagerBase : DomainService
{
    protected IJobCategoryRepository _jobCategoryRepository;

    public JobCategoryManagerBase(IJobCategoryRepository jobCategoryRepository)
    {
        _jobCategoryRepository = jobCategoryRepository;
    }

    public virtual async Task<JobCategory> CreateAsync(string name, string slug, int displayOrder, bool isActive, string? description = null, string? icon = null, string? color = null, Guid? parentId = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), JobCategoryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobCategoryConsts.SlugMaxLength);
        Check.Length(description, nameof(description), JobCategoryConsts.DescriptionMaxLength);
        Check.Length(icon, nameof(icon), JobCategoryConsts.IconMaxLength);
        Check.Length(color, nameof(color), JobCategoryConsts.ColorMaxLength);
        var jobCategory = new JobCategory(GuidGenerator.Create(), name, slug, displayOrder, isActive, description, icon, color, parentId);
        return await _jobCategoryRepository.InsertAsync(jobCategory);
    }

    public virtual async Task<JobCategory> UpdateAsync(Guid id, string name, string slug, int displayOrder, bool isActive, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), JobCategoryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobCategoryConsts.SlugMaxLength);
        Check.Length(description, nameof(description), JobCategoryConsts.DescriptionMaxLength);
        Check.Length(icon, nameof(icon), JobCategoryConsts.IconMaxLength);
        Check.Length(color, nameof(color), JobCategoryConsts.ColorMaxLength);
        var jobCategory = await _jobCategoryRepository.GetAsync(id);
        jobCategory.Name = name;
        jobCategory.Slug = slug;
        jobCategory.DisplayOrder = displayOrder;
        jobCategory.IsActive = isActive;
        jobCategory.Description = description;
        jobCategory.Icon = icon;
        jobCategory.Color = color;
        jobCategory.ParentId = parentId;
        jobCategory.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobCategoryRepository.UpdateAsync(jobCategory);
    }
}