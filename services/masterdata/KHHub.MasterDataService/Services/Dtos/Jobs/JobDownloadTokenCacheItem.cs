using System;

namespace KHHub.MasterDataService.Services.Dtos.Jobs;

public abstract class JobDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}