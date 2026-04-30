using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

public abstract class ArticleTagMappingExcelDtoBase
{
    public bool IsPrimary { get; set; }

    public int Order { get; set; }
}