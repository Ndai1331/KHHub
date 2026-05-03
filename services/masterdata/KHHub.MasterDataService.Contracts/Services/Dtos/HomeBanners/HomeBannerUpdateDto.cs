using KHHub.MasterDataService.Entities.HomeBanners;
using KHHub.MasterDataService.Entities.HomeBanners;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.HomeBanners;

public abstract class HomeBannerUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(HomeBannerConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;
    [StringLength(HomeBannerConsts.SubtitleMaxLength)]
    public string? Subtitle { get; set; }

    public string? Description { get; set; }

    [Required]
    [StringLength(HomeBannerConsts.ImageUrlMaxLength)]
    public string ImageUrl { get; set; } = null!;
    [StringLength(HomeBannerConsts.MobileImageUrlMaxLength)]
    public string? MobileImageUrl { get; set; }

    [StringLength(HomeBannerConsts.ButtonTextMaxLength)]
    public string? ButtonText { get; set; }

    [StringLength(HomeBannerConsts.ButtonUrlMaxLength)]
    public string? ButtonUrl { get; set; }

    public TargetType? TargetType { get; set; }

    public Guid? TargetId { get; set; }

    public int SortOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}