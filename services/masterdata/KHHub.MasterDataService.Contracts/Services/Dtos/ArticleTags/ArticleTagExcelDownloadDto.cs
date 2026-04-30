using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTags;

public abstract class ArticleTagExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public int? UsageCountMin { get; set; }

    public int? UsageCountMax { get; set; }

    public ArticleTagExcelDownloadDtoBase()
    {
    }
}