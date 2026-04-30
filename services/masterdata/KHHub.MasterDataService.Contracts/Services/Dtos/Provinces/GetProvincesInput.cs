using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Provinces;

public abstract class GetProvincesInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public GetProvincesInputBase()
    {
    }
}