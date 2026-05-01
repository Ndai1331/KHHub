using KHHub.MasterDataService.Entities.PlaceCategories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceCategories;

public abstract class PlaceCategoryUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(PlaceCategoryConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(PlaceCategoryConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    [StringLength(PlaceCategoryConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    [StringLength(PlaceCategoryConsts.IconMaxLength)]
    public string? Icon { get; set; }

    [StringLength(PlaceCategoryConsts.ColorMaxLength)]
    public string? Color { get; set; }

    public Guid ParentId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}