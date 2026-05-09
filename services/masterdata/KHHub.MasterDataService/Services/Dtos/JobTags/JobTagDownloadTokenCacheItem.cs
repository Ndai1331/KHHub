using System;

namespace KHHub.MasterDataService.Services.Dtos.JobTags;

public abstract class JobTagDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}