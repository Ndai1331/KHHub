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

namespace KHHub.MasterDataService.Entities.JobViews;

public abstract class JobViewBase : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid? UserId { get; set; }

    [CanBeNull]
    public virtual string? IpAddress { get; set; }

    [CanBeNull]
    public virtual string? Device { get; set; }

    public virtual DateTime ViewedAt { get; set; }

    public virtual int Duration { get; set; }

    [CanBeNull]
    public virtual string? Source { get; set; }

    public Guid JobId { get; set; }

    protected JobViewBase()
    {
    }

    public JobViewBase(Guid id, Guid jobId, DateTime viewedAt, int duration, Guid? userId = null, string? ipAddress = null, string? device = null, string? source = null)
    {
        Id = id;
        Check.Length(ipAddress, nameof(ipAddress), JobViewConsts.IpAddressMaxLength, 0);
        Check.Length(device, nameof(device), JobViewConsts.DeviceMaxLength, 0);
        Check.Length(source, nameof(source), JobViewConsts.SourceMaxLength, 0);
        ViewedAt = viewedAt;
        Duration = duration;
        UserId = userId;
        IpAddress = ipAddress;
        Device = device;
        Source = source;
        JobId = jobId;
    }
}