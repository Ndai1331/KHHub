using System;
using System.Collections.Generic;
using System.Linq;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Services.Dtos.PlaceCrawler;

namespace KHHub.CrawlerSerivce.Crawling.Mapping;

public static class PlaceCrawlerMergeMapper
{
    public static PlaceCrawlerUpsertInputDto ToUpsertDto(
        CrawledPlaceListItem listing,
        CrawledPlaceDetailPatch detail,
        Guid provinceId,
        Guid wardId,
        Guid placeCategoryId,
        PlaceStatus defaultStatus,
        IReadOnlyList<string>? extraTagNames)
    {
        var tags = new List<string>();
        if (extraTagNames != null)
        {
            tags.AddRange(extraTagNames.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Trim()));
        }

        tags = tags.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        var priceRange = PriceRangeNormalizer.TryMap(detail.PriceRangeLabel);

        var wardHints = new List<string>();
        foreach (var h in detail.WardNameHints.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            wardHints.Add(h.Trim());
        }

        var shortDesc = FirstNonEmpty(detail.ShortDescription, listing.ShortDescription, listing.Name);
        var name = listing.Name;
        var seoTitle = name.Length > PlaceConsts.SeoTitleMaxLength ? name[..PlaceConsts.SeoTitleMaxLength] : name;

        var categoryCandidates = new List<string>();
        if (!string.IsNullOrWhiteSpace(detail.CategoryLabel))
        {
            categoryCandidates.Add(detail.CategoryLabel.Trim());
        }

        return new PlaceCrawlerUpsertInputDto
        {
            SourceUrl = listing.SourceAbsoluteUrl,
            Name = name,
            ShortDescription = shortDesc,
            Description = detail.Description,
            ThumbnailUrl = FirstNonEmpty(detail.ThumbnailUrl, listing.ThumbnailUrl),
            CoverImageUrl = FirstNonEmpty(detail.CoverImageUrl, detail.ThumbnailUrl, listing.ThumbnailUrl),
            Address = FirstNonEmpty(detail.Address, listing.AddressSnippet),
            Latitude = detail.Latitude,
            Longituded = detail.Longituded,
            PhoneNumber = detail.PhoneNumber,
            Website = detail.Website,
            OpeningHours = detail.OpeningHours,
            GoogleMapUrl = detail.GoogleMapUrl,
            PriceRange = priceRange,
            RatingAveraged = detail.RatingAveraged ?? listing.Rating,
            ReviewCount = detail.ReviewCount,
            RatingTotal = detail.ReviewCount,
            Status = defaultStatus,
            IsFeatured = false,
            IsHot = false,
            IsVerified = false,
            SeoTitle = seoTitle,
            SeoDescription = shortDesc,
            SeoKeywords = detail.CategoryLabel,
            ProvinceId = provinceId,
            WardId = wardId,
            PlaceCategoryId = placeCategoryId,
            PrimaryPlaceCategoryName = detail.CategoryLabel,
            PlaceCategoryNameCandidates = categoryCandidates,
            WardNameCandidates = wardHints,
            TagNames = tags,
            CrawlerSourceLabel = new Uri(listing.SourceAbsoluteUrl).Host
        };
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        return values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
    }
}
