using System;

namespace KHHub.MasterDataService.Services.Dtos.HomeBanners;

public abstract class HomeBannerDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}