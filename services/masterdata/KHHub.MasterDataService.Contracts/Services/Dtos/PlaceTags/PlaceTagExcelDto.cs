using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTags;

public abstract class PlaceTagExcelDtoBase
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public int UsageCount { get; set; }
}