using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string EntityType { get; set; } = null!;
    public Guid EntityId { get; set; }

    public string? Collection { get; set; }

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; }

    public Guid MediaFileId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}