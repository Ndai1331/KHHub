using System;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class JobTagMappingExcelDtoBase
{
    public bool IsPrimary { get; set; }

    public string? SortOrder { get; set; }
}