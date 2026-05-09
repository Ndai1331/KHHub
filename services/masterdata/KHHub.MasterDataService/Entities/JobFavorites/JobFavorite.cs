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

namespace KHHub.MasterDataService.Entities.JobFavorites;

public abstract class JobFavoriteBase : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid UserId { get; set; }

    public Guid JobId { get; set; }

    protected JobFavoriteBase()
    {
    }

    public JobFavoriteBase(Guid id, Guid jobId, Guid userId)
    {
        Id = id;
        UserId = userId;
        JobId = jobId;
    }
}