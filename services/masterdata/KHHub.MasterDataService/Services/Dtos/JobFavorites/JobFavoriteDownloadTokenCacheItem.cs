using System;

namespace KHHub.MasterDataService.Services.Dtos.JobFavorites;

public abstract class JobFavoriteDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}