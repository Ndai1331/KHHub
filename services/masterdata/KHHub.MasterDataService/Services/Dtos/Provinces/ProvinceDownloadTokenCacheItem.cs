using System;

namespace KHHub.MasterDataService.Services.Dtos.Provinces;

public abstract class ProvinceDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}