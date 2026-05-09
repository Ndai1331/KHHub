using System;

namespace KHHub.MasterDataService.Services.Dtos.JobCategories;

public abstract class JobCategoryDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}