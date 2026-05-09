using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class JobTagMappingCreateDtoBase
{
    public bool IsPrimary { get; set; } = false;
    public string? SortOrder { get; set; } = "1";
    public Guid JobTagId { get; set; }

    public Guid JobId { get; set; }
}