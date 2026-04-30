using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.MasterDataService.Entities.Provinces;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.Wards;

public abstract class WardWithNavigationPropertiesDtoBase
{
    public WardDto Ward { get; set; } = null!;
    public ProvinceDto Province { get; set; } = null!;
}