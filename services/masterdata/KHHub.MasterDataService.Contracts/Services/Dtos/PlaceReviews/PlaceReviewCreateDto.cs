using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Entities.PlaceReviews;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceReviews;

public abstract class PlaceReviewCreateDtoBase
{
    [Required]
    [Range(PlaceReviewConsts.RatingMinLength, PlaceReviewConsts.RatingMaxLength)]
    public int Rating { get; set; }

    [Required]
    [StringLength(PlaceReviewConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;
    public string? Comment { get; set; }

    public int LikeCount { get; set; } = 0;
    public PlaceReviewStatus Status { get; set; } = ((PlaceReviewStatus[])Enum.GetValues(typeof(PlaceReviewStatus)))[0];
    public Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }
}