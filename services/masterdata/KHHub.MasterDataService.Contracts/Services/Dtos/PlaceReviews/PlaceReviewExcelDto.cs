using KHHub.MasterDataService.Entities.PlaceReviews;
using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceReviews;

public abstract class PlaceReviewExcelDtoBase
{
    public int Rating { get; set; }

    public string Title { get; set; } = null!;
    public string? Comment { get; set; }

    public int LikeCount { get; set; }

    public PlaceReviewStatus Status { get; set; }

    public Guid? UserId { get; set; }
}