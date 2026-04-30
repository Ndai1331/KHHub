using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Wards;

public abstract class GetWardsInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public Guid? ProvinceId { get; set; }

    public GetWardsInputBase()
    {
    }
}