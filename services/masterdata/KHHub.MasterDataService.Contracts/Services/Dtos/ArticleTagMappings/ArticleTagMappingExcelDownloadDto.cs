using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

public abstract class ArticleTagMappingExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public bool? IsPrimary { get; set; }

    public int? OrderMin { get; set; }

    public int? OrderMax { get; set; }

    public Guid? ArticleTagId { get; set; }

    public Guid? ArticleId { get; set; }

    public ArticleTagMappingExcelDownloadDtoBase()
    {
    }
}