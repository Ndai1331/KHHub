using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.PlaceViews;

namespace KHHub.MasterDataService.Entities.PlaceViews;

public abstract class PlaceViewWithNavigationPropertiesBase
{
    public PlaceView PlaceView { get; set; } = null!;
    public Place Place { get; set; } = null!;
}