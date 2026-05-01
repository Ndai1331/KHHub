using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

public abstract class PlaceTagMappingDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}