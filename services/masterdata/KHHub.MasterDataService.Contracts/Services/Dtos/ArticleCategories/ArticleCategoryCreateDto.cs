using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCategories;

public abstract class ArticleCategoryCreateDtoBase
{
    [Required]
    [StringLength(ArticleCategoryConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(ArticleCategoryConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    [StringLength(ArticleCategoryConsts.IconMaxLength)]
    public string? Icon { get; set; }

    public Guid ParentId { get; set; }

    public int DisplayOrder { get; set; } = 1;
    public bool IsActive { get; set; }

    [StringLength(ArticleCategoryConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }
}