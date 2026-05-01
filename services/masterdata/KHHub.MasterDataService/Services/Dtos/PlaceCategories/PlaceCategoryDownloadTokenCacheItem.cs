using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceCategories;

public abstract class PlaceCategoryDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}