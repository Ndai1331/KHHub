using KHHub.MasterDataService.Entities.PlaceReviews;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceReviews;

public abstract class PlaceReviewDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public int Rating { get; set; }

    public string Title { get; set; } = null!;
    public string? Comment { get; set; }

    public int LikeCount { get; set; }

    public PlaceReviewStatus Status { get; set; }

    public Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}