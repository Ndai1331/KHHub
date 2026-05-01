using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTags;

public abstract class PlaceTagDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}