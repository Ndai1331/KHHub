using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.JobTagMappings;

public abstract class GetJobTagMappingsInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public bool? IsPrimary { get; set; }

    public string? SortOrder { get; set; }

    public Guid? JobTagId { get; set; }

    public Guid? JobId { get; set; }

    public GetJobTagMappingsInputBase()
    {
    }
}