using System.ComponentModel.DataAnnotations;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class PreviewJobListingInput
{
    /// <summary>
    /// Listing URL to crawl (e.g. https://nhatrangjob.vn/viec-lam).
    /// </summary>
    [Required]
    [StringLength(2048)]
    public string ListingUrl { get; set; } = null!;

    /// <summary>
    /// How many listing pages to follow (site-specific pagination).
    /// </summary>
    [Range(1, 20)]
    public int MaxPages { get; set; } = 1;
}
