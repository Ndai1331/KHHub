using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Entities.Places;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.PlaceReviews;

public abstract class PlaceReviewBase : FullAuditedAggregateRoot<Guid>
{
    public virtual int Rating { get; set; }

    [NotNull]
    public virtual string Title { get; set; }

    [CanBeNull]
    public virtual string? Comment { get; set; }

    public virtual int LikeCount { get; set; }

    public virtual PlaceReviewStatus Status { get; set; }

    public virtual Guid? UserId { get; set; }

    public Guid PlaceId { get; set; }

    protected PlaceReviewBase()
    {
    }

    public PlaceReviewBase(Guid id, Guid placeId, int rating, string title, int likeCount, PlaceReviewStatus status, string? comment = null, Guid? userId = null)
    {
        Id = id;
        if (rating < PlaceReviewConsts.RatingMinLength)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), rating, "The value of 'rating' cannot be lower than " + PlaceReviewConsts.RatingMinLength);
        }

        if (rating > PlaceReviewConsts.RatingMaxLength)
        {
            throw new ArgumentOutOfRangeException(nameof(rating), rating, "The value of 'rating' cannot be greater than " + PlaceReviewConsts.RatingMaxLength);
        }

        Check.NotNull(title, nameof(title));
        Check.Length(title, nameof(title), PlaceReviewConsts.TitleMaxLength, 0);
        Rating = rating;
        Title = title;
        LikeCount = likeCount;
        Status = status;
        Comment = comment;
        UserId = userId;
        PlaceId = placeId;
    }
}