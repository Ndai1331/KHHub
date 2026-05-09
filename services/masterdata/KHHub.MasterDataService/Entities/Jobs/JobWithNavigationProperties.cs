using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.JobCategories;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.Jobs;

namespace KHHub.MasterDataService.Entities.Jobs;

public abstract class JobWithNavigationPropertiesBase
{
    public Job Job { get; set; } = null!;
    public Province Province { get; set; } = null!;
    public Ward Ward { get; set; } = null!;
    public JobCategory JobCategory { get; set; } = null!;
}