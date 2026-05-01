using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceCategories;

public abstract class PlaceCategoryExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? Color { get; set; }

    public Guid? ParentId { get; set; }

    public int? DisplayOrderMin { get; set; }

    public int? DisplayOrderMax { get; set; }

    public bool? IsActive { get; set; }

    public PlaceCategoryExcelDownloadDtoBase()
    {
    }
}