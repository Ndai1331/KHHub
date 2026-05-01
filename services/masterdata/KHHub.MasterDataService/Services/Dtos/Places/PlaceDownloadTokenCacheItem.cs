using System;

namespace KHHub.MasterDataService.Services.Dtos.Places;

public abstract class PlaceDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}