using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobFavorites;

public abstract class JobFavoriteWithNavigationPropertiesDtoBase
{
    public JobFavoriteDto JobFavorite { get; set; } = null!;
    public JobDto Job { get; set; } = null!;
}