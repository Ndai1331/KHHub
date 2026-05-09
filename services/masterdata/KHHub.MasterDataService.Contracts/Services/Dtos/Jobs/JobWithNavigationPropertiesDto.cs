using KHHub.MasterDataService.Services.Dtos.JobCategories;
using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.JobCategories;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.Jobs;

public abstract class JobWithNavigationPropertiesDtoBase
{
    public JobDto Job { get; set; } = null!;
    public ProvinceDto Province { get; set; } = null!;
    public WardDto Ward { get; set; } = null!;
    public JobCategoryDto JobCategory { get; set; } = null!;
}