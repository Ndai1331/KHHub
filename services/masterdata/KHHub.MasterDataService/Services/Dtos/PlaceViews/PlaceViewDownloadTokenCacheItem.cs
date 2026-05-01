using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceViews;

public abstract class PlaceViewDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}