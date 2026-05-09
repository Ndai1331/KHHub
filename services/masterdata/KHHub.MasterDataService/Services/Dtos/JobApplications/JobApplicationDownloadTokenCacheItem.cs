using System;

namespace KHHub.MasterDataService.Services.Dtos.JobApplications;

public abstract class JobApplicationDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}