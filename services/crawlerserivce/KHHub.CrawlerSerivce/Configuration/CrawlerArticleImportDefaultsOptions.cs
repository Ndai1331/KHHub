using System;

namespace KHHub.CrawlerSerivce.Configuration;

/// <summary>
/// Fallback article category / author / status when import request omits them.
/// </summary>
public class CrawlerArticleImportDefaultsOptions
{
    public const string SectionKey = "Crawler:ArticleImportDefaults";

    public Guid ArticleCategoryId { get; set; }

    public string DefaultAuthorName { get; set; } = "KH Hub";

    /// <summary>
    /// <see cref="KHHub.MasterDataService.Entities.Articles.ArticleStatus" /> name, e.g. Pending, Published.
    /// </summary>
    public string DefaultStatus { get; set; } = "Pending";
}
