using KHHub.MasterDataService.Entities.ArticleTags;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTags;

public abstract class ArticleTagUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(ArticleTagConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(ArticleTagConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public int UsageCount { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}