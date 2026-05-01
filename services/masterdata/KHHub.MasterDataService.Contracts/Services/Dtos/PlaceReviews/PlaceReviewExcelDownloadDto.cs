using KHHub.MasterDataService.Entities.PlaceReviews;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceReviews;

public abstract class PlaceReviewExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public int? RatingMin { get; set; }

    public int? RatingMax { get; set; }

    public string? Title { get; set; }

    public string? Comment { get; set; }

    public int? LikeCountMin { get; set; }

    public int? LikeCountMax { get; set; }

    public PlaceReviewStatus? Status { get; set; }

    public Guid? UserId { get; set; }

    public Guid? PlaceId { get; set; }

    public PlaceReviewExcelDownloadDtoBase()
    {
    }
}