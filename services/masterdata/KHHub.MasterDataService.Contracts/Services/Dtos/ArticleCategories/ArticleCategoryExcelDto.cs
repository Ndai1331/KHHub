using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCategories;

public abstract class ArticleCategoryExcelDtoBase
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public string? Icon { get; set; }

    public Guid ParentId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public string? ThumbnailUrl { get; set; }
}