using System;

namespace KHHub.MasterDataService.Services.Dtos.JobViews;

public abstract class JobViewDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}