using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class ImportPlaceListingInput
{
    [Required]
    [StringLength(2048)]
    public string ListingUrl { get; set; } = null!;

    [Range(1, 1000)]
    public int MaxPages { get; set; } = 1;

    [Range(1, 500)]
    public int? MaxPlacesToImport { get; set; }

    [Range(0, 60_000)]
    public int DelayBetweenDetailRequestsMs { get; set; } = 1000;

    public Guid? ProvinceId { get; set; }

    public Guid? WardId { get; set; }

    public Guid? PlaceCategoryId { get; set; }

    public List<string> ExtraTagNames { get; set; } = new();
}
