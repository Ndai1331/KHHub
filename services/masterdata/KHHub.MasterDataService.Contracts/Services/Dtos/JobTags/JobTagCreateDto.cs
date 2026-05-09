using KHHub.MasterDataService.Entities.JobTags;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobTags;

public abstract class JobTagCreateDtoBase
{
    [Required]
    [StringLength(JobTagConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(JobTagConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    [StringLength(JobTagConsts.DescriptionMaxLength)]
    public string? Description { get; set; }

    public int UsageCount { get; set; }
}