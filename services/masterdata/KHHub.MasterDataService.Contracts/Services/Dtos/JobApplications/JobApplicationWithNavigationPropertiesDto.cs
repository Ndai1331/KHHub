using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobApplications;

public abstract class JobApplicationWithNavigationPropertiesDtoBase
{
    public JobApplicationDto JobApplication { get; set; } = null!;
    public JobDto Job { get; set; } = null!;
}