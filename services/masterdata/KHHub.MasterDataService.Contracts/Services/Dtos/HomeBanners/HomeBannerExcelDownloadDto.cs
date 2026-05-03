using KHHub.MasterDataService.Entities.HomeBanners;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.HomeBanners;

public abstract class HomeBannerExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? Title { get; set; }

    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? MobileImageUrl { get; set; }

    public string? ButtonText { get; set; }

    public string? ButtonUrl { get; set; }

    public TargetType? TargetType { get; set; }

    public Guid? TargetId { get; set; }

    public int? SortOrderMin { get; set; }

    public int? SortOrderMax { get; set; }

    public bool? IsActive { get; set; }

    public DateTime? StartDateMin { get; set; }

    public DateTime? StartDateMax { get; set; }

    public DateTime? EndDateMin { get; set; }

    public DateTime? EndDateMax { get; set; }

    public HomeBannerExcelDownloadDtoBase()
    {
    }
}