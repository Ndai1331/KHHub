using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;
using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Entities.Places;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

public abstract class PlaceTagMappingWithNavigationPropertiesDtoBase
{
    public PlaceTagMappingDto PlaceTagMapping { get; set; } = null!;
    public PlaceTagDto PlaceTag { get; set; } = null!;
    public PlaceDto Place { get; set; } = null!;
}