using System.ComponentModel.DataAnnotations;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class PreviewArticleListingInput
{
    [Required]
    [StringLength(2048)]
    public string ListingUrl { get; set; } = null!;

    [Range(1, 1000)]
    public int MaxPages { get; set; } = 1;
}
