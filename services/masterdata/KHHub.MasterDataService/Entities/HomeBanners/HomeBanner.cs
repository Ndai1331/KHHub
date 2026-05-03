using KHHub.MasterDataService.Entities.HomeBanners;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.HomeBanners;

public abstract class HomeBannerBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Title { get; set; }

    [CanBeNull]
    public virtual string? Subtitle { get; set; }

    [CanBeNull]
    public virtual string? Description { get; set; }

    [NotNull]
    public virtual string ImageUrl { get; set; }

    [CanBeNull]
    public virtual string? MobileImageUrl { get; set; }

    [CanBeNull]
    public virtual string? ButtonText { get; set; }

    [CanBeNull]
    public virtual string? ButtonUrl { get; set; }

    public virtual TargetType? TargetType { get; set; }

    public virtual Guid? TargetId { get; set; }

    public virtual int SortOrder { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual DateTime? StartDate { get; set; }

    public virtual DateTime? EndDate { get; set; }

    protected HomeBannerBase()
    {
    }

    public HomeBannerBase(Guid id, string title, string imageUrl, int sortOrder, bool isActive, string? subtitle = null, string? description = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        Id = id;
        Check.NotNull(title, nameof(title));
        Check.Length(title, nameof(title), HomeBannerConsts.TitleMaxLength, 0);
        Check.NotNull(imageUrl, nameof(imageUrl));
        Check.Length(imageUrl, nameof(imageUrl), HomeBannerConsts.ImageUrlMaxLength, 0);
        Check.Length(subtitle, nameof(subtitle), HomeBannerConsts.SubtitleMaxLength, 0);
        Check.Length(mobileImageUrl, nameof(mobileImageUrl), HomeBannerConsts.MobileImageUrlMaxLength, 0);
        Check.Length(buttonText, nameof(buttonText), HomeBannerConsts.ButtonTextMaxLength, 0);
        Check.Length(buttonUrl, nameof(buttonUrl), HomeBannerConsts.ButtonUrlMaxLength, 0);
        Title = title;
        ImageUrl = imageUrl;
        SortOrder = sortOrder;
        IsActive = isActive;
        Subtitle = subtitle;
        Description = description;
        MobileImageUrl = mobileImageUrl;
        ButtonText = buttonText;
        ButtonUrl = buttonUrl;
        TargetType = targetType;
        TargetId = targetId;
        StartDate = startDate;
        EndDate = endDate;
    }
}