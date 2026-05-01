using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.PlaceCategories;

public abstract class PlaceCategoryBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    [NotNull]
    public virtual string Slug { get; set; }

    [CanBeNull]
    public virtual string? Description { get; set; }

    [CanBeNull]
    public virtual string? Icon { get; set; }

    [CanBeNull]
    public virtual string? Color { get; set; }

    public virtual Guid ParentId { get; set; }

    public virtual int DisplayOrder { get; set; }

    public virtual bool IsActive { get; set; }

    protected PlaceCategoryBase()
    {
    }

    public PlaceCategoryBase(Guid id, string name, string slug, Guid parentId, int displayOrder, bool isActive, string? description = null, string? icon = null, string? color = null)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), PlaceCategoryConsts.NameMaxLength, 0);
        Check.NotNull(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceCategoryConsts.SlugMaxLength, 0);
        Check.Length(description, nameof(description), PlaceCategoryConsts.DescriptionMaxLength, 0);
        Check.Length(icon, nameof(icon), PlaceCategoryConsts.IconMaxLength, 0);
        Check.Length(color, nameof(color), PlaceCategoryConsts.ColorMaxLength, 0);
        Name = name;
        Slug = slug;
        ParentId = parentId;
        DisplayOrder = displayOrder;
        IsActive = isActive;
        Description = description;
        Icon = icon;
        Color = color;
    }
}