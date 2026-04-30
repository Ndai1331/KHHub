using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Provinces;

public abstract class ProvinceExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public ProvinceExcelDownloadDtoBase()
    {
    }
}