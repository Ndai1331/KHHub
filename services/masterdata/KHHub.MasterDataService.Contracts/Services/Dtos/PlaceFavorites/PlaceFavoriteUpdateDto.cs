using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

public abstract class PlaceFavoriteUpdateDtoBase : IHasConcurrencyStamp
{
    public Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}