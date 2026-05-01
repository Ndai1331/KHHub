using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? EntityType { get; set; }

    public Guid? EntityId { get; set; }

    public string? Collection { get; set; }

    public int? SortOrderMin { get; set; }

    public int? SortOrderMax { get; set; }

    public bool? IsPrimary { get; set; }

    public Guid? MediaFileId { get; set; }

    public EntityFileExcelDownloadDtoBase()
    {
    }
}