using System;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}