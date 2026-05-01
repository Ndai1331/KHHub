using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.Places;

namespace KHHub.MasterDataService.Entities.Places;

public abstract class PlaceWithNavigationPropertiesBase
{
    public Place Place { get; set; } = null!;
    public PlaceCategory PlaceCategory { get; set; } = null!;
    public Province Province { get; set; } = null!;
    public Ward Ward { get; set; } = null!;
}