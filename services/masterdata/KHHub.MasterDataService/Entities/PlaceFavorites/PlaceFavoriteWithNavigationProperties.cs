using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.PlaceFavorites;

namespace KHHub.MasterDataService.Entities.PlaceFavorites;

public abstract class PlaceFavoriteWithNavigationPropertiesBase
{
    public PlaceFavorite PlaceFavorite { get; set; } = null!;
    public Place Place { get; set; } = null!;
}