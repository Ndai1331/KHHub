using KHHub.MasterDataService.Entities.JobTags;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.JobTagMappings;

namespace KHHub.MasterDataService.Entities.JobTagMappings;

public abstract class JobTagMappingWithNavigationPropertiesBase
{
    public JobTagMapping JobTagMapping { get; set; } = null!;
    public JobTag JobTag { get; set; } = null!;
    public Job Job { get; set; } = null!;
}