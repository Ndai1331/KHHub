using System;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class JobTagMappingDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}