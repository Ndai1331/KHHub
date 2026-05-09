using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class JobTagMappingUpdateDtoBase : IHasConcurrencyStamp
{
    public bool IsPrimary { get; set; }

    public string? SortOrder { get; set; }

    public Guid JobTagId { get; set; }

    public Guid JobId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}