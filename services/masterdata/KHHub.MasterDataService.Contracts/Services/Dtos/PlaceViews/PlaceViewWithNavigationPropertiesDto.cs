using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Entities.Places;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceViews;

public abstract class PlaceViewWithNavigationPropertiesDtoBase
{
    public PlaceViewDto PlaceView { get; set; } = null!;
    public PlaceDto Place { get; set; } = null!;
}