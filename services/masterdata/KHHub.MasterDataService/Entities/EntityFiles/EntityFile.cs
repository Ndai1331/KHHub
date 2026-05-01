using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.EntityFiles;

public abstract class EntityFileBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string EntityType { get; set; }

    public virtual Guid EntityId { get; set; }

    [CanBeNull]
    public virtual string? Collection { get; set; }

    public virtual int SortOrder { get; set; }

    public virtual bool IsPrimary { get; set; }

    public Guid MediaFileId { get; set; }

    protected EntityFileBase()
    {
    }

    public EntityFileBase(Guid id, Guid mediaFileId, string entityType, Guid entityId, int sortOrder, bool isPrimary, string? collection = null)
    {
        Id = id;
        Check.NotNull(entityType, nameof(entityType));
        Check.Length(entityType, nameof(entityType), EntityFileConsts.EntityTypeMaxLength, 0);
        Check.Length(collection, nameof(collection), EntityFileConsts.CollectionMaxLength, 0);
        EntityType = entityType;
        EntityId = entityId;
        SortOrder = sortOrder;
        IsPrimary = isPrimary;
        Collection = collection;
        MediaFileId = mediaFileId;
    }
}