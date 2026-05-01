using KHHub.MasterDataService.Entities.Places;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.PlaceViews;

public abstract class PlaceViewBase : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid UserId { get; set; }

    [CanBeNull]
    public virtual string? IpAddress { get; set; }

    [CanBeNull]
    public virtual string? Device { get; set; }

    public virtual DateTime ViewedAt { get; set; }

    public virtual int Duration { get; set; }

    [CanBeNull]
    public virtual string? Source { get; set; }

    public Guid PlaceId { get; set; }

    protected PlaceViewBase()
    {
    }

    public PlaceViewBase(Guid id, Guid placeId, Guid userId, DateTime viewedAt, int duration, string? ipAddress = null, string? device = null, string? source = null)
    {
        Id = id;
        Check.Length(ipAddress, nameof(ipAddress), PlaceViewConsts.IpAddressMaxLength, 0);
        Check.Length(device, nameof(device), PlaceViewConsts.DeviceMaxLength, 0);
        Check.Length(source, nameof(source), PlaceViewConsts.SourceMaxLength, 0);
        UserId = userId;
        ViewedAt = viewedAt;
        Duration = duration;
        IpAddress = ipAddress;
        Device = device;
        Source = source;
        PlaceId = placeId;
    }
}