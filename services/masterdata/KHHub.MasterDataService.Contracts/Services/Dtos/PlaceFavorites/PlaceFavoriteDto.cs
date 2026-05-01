using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

public abstract class PlaceFavoriteDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}