using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Services.Dtos.JobTags;
using KHHub.MasterDataService.Entities.JobTags;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class JobTagMappingWithNavigationPropertiesDtoBase
{
    public JobTagMappingDto JobTagMapping { get; set; } = null!;
    public JobTagDto JobTag { get; set; } = null!;
    public JobDto Job { get; set; } = null!;
}