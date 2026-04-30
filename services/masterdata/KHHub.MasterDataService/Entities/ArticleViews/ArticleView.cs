using KHHub.MasterDataService.Entities.Articles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.ArticleViews;

public abstract class ArticleViewBase : FullAuditedAggregateRoot<Guid>
{
    [CanBeNull]
    public virtual string? IpAddress { get; set; }

    [CanBeNull]
    public virtual string? Device { get; set; }

    [CanBeNull]
    public virtual string? Source { get; set; }

    public virtual DateTime ViewedAt { get; set; }

    public virtual int Duration { get; set; }

    public virtual Guid UserId { get; set; }

    public Guid ArticleId { get; set; }

    protected ArticleViewBase()
    {
    }

    public ArticleViewBase(Guid id, Guid articleId, DateTime viewedAt, int duration, Guid userId, string? ipAddress = null, string? device = null, string? source = null)
    {
        Id = id;
        Check.Length(ipAddress, nameof(ipAddress), ArticleViewConsts.IpAddressMaxLength, 0);
        Check.Length(device, nameof(device), ArticleViewConsts.DeviceMaxLength, 0);
        Check.Length(source, nameof(source), ArticleViewConsts.SourceMaxLength, 0);
        ViewedAt = viewedAt;
        Duration = duration;
        UserId = userId;
        IpAddress = ipAddress;
        Device = device;
        Source = source;
        ArticleId = articleId;
    }
}