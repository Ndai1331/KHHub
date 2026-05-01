using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

public abstract class PlaceTagMappingExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public bool? IsPrimary { get; set; }

    public int? SortOrderMin { get; set; }

    public int? SortOrderMax { get; set; }

    public Guid? PlaceTagId { get; set; }

    public Guid? PlaceId { get; set; }

    public PlaceTagMappingExcelDownloadDtoBase()
    {
    }
}