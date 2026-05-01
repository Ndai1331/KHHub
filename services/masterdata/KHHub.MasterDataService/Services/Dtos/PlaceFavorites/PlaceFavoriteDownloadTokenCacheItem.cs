using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

public abstract class PlaceFavoriteDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}