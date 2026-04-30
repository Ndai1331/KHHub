using System;

namespace KHHub.MasterDataService.Services.Dtos.Wards;

public abstract class WardExcelDtoBase
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}