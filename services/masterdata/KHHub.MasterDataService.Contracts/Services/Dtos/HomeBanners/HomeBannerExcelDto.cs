using KHHub.MasterDataService.Entities.HomeBanners;
using System;

namespace KHHub.MasterDataService.Services.Dtos.HomeBanners;

public abstract class HomeBannerExcelDtoBase
{
    public string Title { get; set; } = null!;
    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string ImageUrl { get; set; } = null!;
    public string? MobileImageUrl { get; set; }

    public string? ButtonText { get; set; }

    public string? ButtonUrl { get; set; }

    public TargetType? TargetType { get; set; }

    public Guid? TargetId { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }
}