using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Entities.Places;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

public abstract class PlaceFavoriteWithNavigationPropertiesDtoBase
{
    public PlaceFavoriteDto PlaceFavorite { get; set; } = null!;
    public PlaceDto Place { get; set; } = null!;
}