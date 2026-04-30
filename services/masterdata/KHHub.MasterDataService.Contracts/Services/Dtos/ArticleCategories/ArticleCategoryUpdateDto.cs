using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCategories;

public abstract class ArticleCategoryUpdateDtoBase : IHasConcurrencyStamp
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

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    [StringLength(ArticleCategoryConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}