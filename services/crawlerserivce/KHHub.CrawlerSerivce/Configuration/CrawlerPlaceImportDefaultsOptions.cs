using System;

namespace KHHub.CrawlerSerivce.Configuration;

/// <summary>
/// Fallback province / ward / place category / status for place import.
/// </summary>
public class CrawlerPlaceImportDefaultsOptions
{
    public const string SectionKey = "Crawler:PlaceImportDefaults";

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid PlaceCategoryId { get; set; }

    /// <summary>
    /// <see cref="KHHub.MasterDataService.Entities.Places.PlaceStatus" /> name, e.g. Draft, Published.
    /// </summary>
    public string DefaultStatus { get; set; } = "Draft";
}
