namespace KHHub.MasterDataService.Entities.PlaceReviews;

public static class PlaceReviewConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "PlaceReview." : string.Empty);
    }

    public const int RatingMinLength = 1;
    public const int RatingMaxLength = 5;
    public const int TitleMaxLength = 255;
}