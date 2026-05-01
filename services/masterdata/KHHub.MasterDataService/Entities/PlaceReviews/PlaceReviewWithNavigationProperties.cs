using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.PlaceReviews;

namespace KHHub.MasterDataService.Entities.PlaceReviews;

public abstract class PlaceReviewWithNavigationPropertiesBase
{
    public PlaceReview PlaceReview { get; set; } = null!;
    public Place Place { get; set; } = null!;
}