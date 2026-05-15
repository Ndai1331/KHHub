using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCrawler;

public class ArticleCrawlerUpsertResultDto
{
    public Guid ArticleId { get; set; }

    public bool WasCreated { get; set; }
}
