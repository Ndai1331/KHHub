using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

public abstract class PlaceTagMappingExcelDtoBase
{
    public bool IsPrimary { get; set; }

    public int SortOrder { get; set; }
}