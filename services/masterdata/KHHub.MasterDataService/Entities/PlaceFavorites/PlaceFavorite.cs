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

namespace KHHub.MasterDataService.Entities.PlaceFavorites;

public abstract class PlaceFavoriteBase : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }

    protected PlaceFavoriteBase()
    {
    }

    public PlaceFavoriteBase(Guid id, Guid placeId, Guid? userId = null)
    {
        Id = id;
        UserId = userId;
        PlaceId = placeId;
    }
}