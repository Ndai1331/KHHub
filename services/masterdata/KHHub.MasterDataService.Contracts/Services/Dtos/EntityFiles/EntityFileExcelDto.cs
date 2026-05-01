using System;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileExcelDtoBase
{
    public string EntityType { get; set; } = null!;
    public Guid EntityId { get; set; }

    public string? Collection { get; set; }

    public int SortOrder { get; set; }

    public bool IsPrimary { get; set; }
}