using KHHub.MasterDataService.Entities.EntityFiles;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(EntityFileConsts.EntityTypeMaxLength)]
    public string EntityType { get; set; } = null!;
    public Guid EntityId { get; set; }

    [StringLength(EntityFileConsts.CollectionMaxLength)]
    public string? Collection { get; set; }

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; }

    public Guid MediaFileId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}