using KHHub.MasterDataService.Entities.PlaceReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.PlaceReviews;

namespace KHHub.MasterDataService.Entities.PlaceReviews;

public abstract class PlaceReviewManagerBase : DomainService
{
    protected IPlaceReviewRepository _placeReviewRepository;

    public PlaceReviewManagerBase(IPlaceReviewRepository placeReviewRepository)
    {
        _placeReviewRepository = placeReviewRepository;
    }

    public virtual async Task<PlaceReview> CreateAsync(Guid placeId, int rating, string title, int likeCount, PlaceReviewStatus status, string? comment = null, Guid? userId = null)
    {
        Check.NotNull(placeId, nameof(placeId));
        Check.Range(rating, nameof(rating), PlaceReviewConsts.RatingMinLength, PlaceReviewConsts.RatingMaxLength);
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), PlaceReviewConsts.TitleMaxLength);
        Check.NotNull(status, nameof(status));
        var placeReview = new PlaceReview(GuidGenerator.Create(), placeId, rating, title, likeCount, status, comment, userId);
        return await _placeReviewRepository.InsertAsync(placeReview);
    }

    public virtual async Task<PlaceReview> UpdateAsync(Guid id, Guid placeId, int rating, string title, int likeCount, PlaceReviewStatus status, string? comment = null, Guid? userId = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(placeId, nameof(placeId));
        Check.Range(rating, nameof(rating), PlaceReviewConsts.RatingMinLength, PlaceReviewConsts.RatingMaxLength);
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), PlaceReviewConsts.TitleMaxLength);
        Check.NotNull(status, nameof(status));
        var placeReview = await _placeReviewRepository.GetAsync(id);
        placeReview.PlaceId = placeId;
        placeReview.Rating = rating;
        placeReview.Title = title;
        placeReview.LikeCount = likeCount;
        placeReview.Status = status;
        placeReview.Comment = comment;
        placeReview.UserId = userId;
        placeReview.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeReviewRepository.UpdateAsync(placeReview);
    }
}