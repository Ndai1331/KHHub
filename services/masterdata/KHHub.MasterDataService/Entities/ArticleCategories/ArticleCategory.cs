using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.ArticleCategories;

public abstract class ArticleCategoryBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    [NotNull]
    public virtual string Slug { get; set; }

    [CanBeNull]
    public virtual string? Description { get; set; }

    [CanBeNull]
    public virtual string? Icon { get; set; }

    public virtual Guid ParentId { get; set; }

    public virtual int DisplayOrder { get; set; }

    public virtual bool IsActive { get; set; }

    [CanBeNull]
    public virtual string? ThumbnailUrl { get; set; }

    protected ArticleCategoryBase()
    {
    }

    public ArticleCategoryBase(Guid id, string name, string slug, Guid parentId, int displayOrder, bool isActive, string? description = null, string? icon = null, string? thumbnailUrl = null)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), ArticleCategoryConsts.NameMaxLength, 0);
        Check.NotNull(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleCategoryConsts.SlugMaxLength, 0);
        Check.Length(icon, nameof(icon), ArticleCategoryConsts.IconMaxLength, 0);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), ArticleCategoryConsts.ThumbnailUrlMaxLength, 0);
        Name = name;
        Slug = slug;
        ParentId = parentId;
        DisplayOrder = displayOrder;
        IsActive = isActive;
        Description = description;
        Icon = icon;
        ThumbnailUrl = thumbnailUrl;
    }
}