using System;

namespace KHHub.MasterDataService.Services.Dtos.Wards;

public abstract class WardDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}