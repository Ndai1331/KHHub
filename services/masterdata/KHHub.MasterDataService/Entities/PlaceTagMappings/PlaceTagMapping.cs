using KHHub.MasterDataService.Entities.PlaceTags;
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

namespace KHHub.MasterDataService.Entities.PlaceTagMappings;

public abstract class PlaceTagMappingBase : FullAuditedAggregateRoot<Guid>
{
    public virtual bool IsPrimary { get; set; }

    public virtual int SortOrder { get; set; }

    public Guid PlaceTagId { get; set; }

    public Guid PlaceId { get; set; }

    protected PlaceTagMappingBase()
    {
    }

    public PlaceTagMappingBase(Guid id, Guid placeTagId, Guid placeId, bool isPrimary, int sortOrder)
    {
        Id = id;
        IsPrimary = isPrimary;
        SortOrder = sortOrder;
        PlaceTagId = placeTagId;
        PlaceId = placeId;
    }
}