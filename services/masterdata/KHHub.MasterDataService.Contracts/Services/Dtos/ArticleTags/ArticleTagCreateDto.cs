using KHHub.MasterDataService.Entities.ArticleTags;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTags;

public abstract class ArticleTagCreateDtoBase
{
    [Required]
    [StringLength(ArticleTagConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(ArticleTagConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public int UsageCount { get; set; } = 0;
}