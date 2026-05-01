using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

public abstract class PlaceFavoriteCreateDtoBase
{
    public Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }
}