using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.PlaceTagMappings;

namespace KHHub.MasterDataService.Entities.PlaceTagMappings;

public abstract class PlaceTagMappingWithNavigationPropertiesBase
{
    public PlaceTagMapping PlaceTagMapping { get; set; } = null!;
    public PlaceTag PlaceTag { get; set; } = null!;
    public Place Place { get; set; } = null!;
}