using System;

namespace KHHub.CrawlerSerivce.Configuration;

/// <summary>
/// Fallback Province / Ward / JobCategory when import request omits them (see appsettings Crawler:ImportDefaults).
/// </summary>
public class CrawlerImportDefaultsOptions
{
    public const string SectionKey = "Crawler:ImportDefaults";

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid JobCategoryId { get; set; }
}
