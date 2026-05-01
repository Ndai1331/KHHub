using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Entities.PlaceReviews;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceReviews;

public abstract class PlaceReviewUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [Range(PlaceReviewConsts.RatingMinLength, PlaceReviewConsts.RatingMaxLength)]
    public int Rating { get; set; }

    [Required]
    [StringLength(PlaceReviewConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;
    public string? Comment { get; set; }

    public int LikeCount { get; set; }

    public PlaceReviewStatus Status { get; set; }

    public Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}