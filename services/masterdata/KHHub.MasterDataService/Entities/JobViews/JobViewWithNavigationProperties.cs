using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.JobViews;

namespace KHHub.MasterDataService.Entities.JobViews;

public abstract class JobViewWithNavigationPropertiesBase
{
    public JobView JobView { get; set; } = null!;
    public Job Job { get; set; } = null!;
}