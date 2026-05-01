using KHHub.MasterDataService.Entities.PlaceTags;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTags;

public abstract class PlaceTagCreateDtoBase
{
    [Required]
    [StringLength(PlaceTagConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(PlaceTagConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    [StringLength(PlaceTagConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    public int UsageCount { get; set; } = 0;
}