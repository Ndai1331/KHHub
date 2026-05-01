using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;
using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.Places;

public abstract class PlaceWithNavigationPropertiesDtoBase
{
    public PlaceDto Place { get; set; } = null!;
    public PlaceCategoryDto PlaceCategory { get; set; } = null!;
    public ProvinceDto Province { get; set; } = null!;
    public WardDto Ward { get; set; } = null!;
}