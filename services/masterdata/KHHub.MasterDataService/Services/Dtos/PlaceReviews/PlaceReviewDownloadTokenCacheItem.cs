using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceReviews;

public abstract class PlaceReviewDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}