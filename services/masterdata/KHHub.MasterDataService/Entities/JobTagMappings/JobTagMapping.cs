using KHHub.MasterDataService.Entities.JobTags;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.JobTagMappings;

public abstract class JobTagMappingBase : FullAuditedAggregateRoot<Guid>
{
    public virtual bool IsPrimary { get; set; }

    [CanBeNull]
    public virtual string? SortOrder { get; set; }

    public Guid JobTagId { get; set; }

    public Guid JobId { get; set; }

    protected JobTagMappingBase()
    {
    }

    public JobTagMappingBase(Guid id, Guid jobTagId, Guid jobId, bool isPrimary, string? sortOrder = null)
    {
        Id = id;
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
        JobTagId = jobTagId;
        JobId = jobId;
    }
}