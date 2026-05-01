using KHHub.MasterDataService.Entities.EntityFiles;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileCreateDtoBase
{
    [Required]
    [StringLength(EntityFileConsts.EntityTypeMaxLength)]
    public string EntityType { get; set; } = null!;
    public Guid EntityId { get; set; }

    [StringLength(EntityFileConsts.CollectionMaxLength)]
    public string? Collection { get; set; }

    public int SortOrder { get; set; } = 1;
    public bool IsPrimary { get; set; } = false;
    public Guid MediaFileId { get; set; }
}