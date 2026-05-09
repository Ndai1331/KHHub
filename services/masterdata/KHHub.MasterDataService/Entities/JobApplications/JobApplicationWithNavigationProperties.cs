using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.JobApplications;

namespace KHHub.MasterDataService.Entities.JobApplications;

public abstract class JobApplicationWithNavigationPropertiesBase
{
    public JobApplication JobApplication { get; set; } = null!;
    public Job Job { get; set; } = null!;
}