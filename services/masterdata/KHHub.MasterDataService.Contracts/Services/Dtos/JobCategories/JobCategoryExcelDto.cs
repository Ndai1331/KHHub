using System;

namespace KHHub.MasterDataService.Services.Dtos.JobCategories;

public abstract class JobCategoryExcelDtoBase
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? Color { get; set; }

    public Guid ParentId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }
}