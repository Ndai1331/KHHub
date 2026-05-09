using KHHub.MasterDataService.Entities.JobCategories;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobCategories;

public abstract class JobCategoryUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(JobCategoryConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(JobCategoryConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    [StringLength(JobCategoryConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    [StringLength(JobCategoryConsts.IconMaxLength)]
    public string? Icon { get; set; }

    [StringLength(JobCategoryConsts.ColorMaxLength)]
    public string? Color { get; set; }

    public Guid ParentId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}