using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class JobTagMappingDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public bool IsPrimary { get; set; }

    public string? SortOrder { get; set; }

    public Guid JobTagId { get; set; }

    public Guid JobId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}