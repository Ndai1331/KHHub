using System;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public abstract class MediaFileDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}