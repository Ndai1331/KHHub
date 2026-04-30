using System;

namespace KHHub.MasterDataService.Services.Dtos.Provinces;

public abstract class ProvinceExcelDtoBase
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
}