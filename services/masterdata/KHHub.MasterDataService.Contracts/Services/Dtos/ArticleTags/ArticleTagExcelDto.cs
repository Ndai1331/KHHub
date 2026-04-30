using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTags;

public abstract class ArticleTagExcelDtoBase
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public int UsageCount { get; set; }
}