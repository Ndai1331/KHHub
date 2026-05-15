using KHHub.CrawlerSerivce.Services.Dtos.Crawling;

namespace KHHub.CrawlerSerivce.Configuration;

/// <summary>
/// Scheduled crawl windows (cron in <see cref="TimeZoneId"/>) that enqueue RabbitMQ background jobs.
/// </summary>
public class CrawlerScheduledImportsOptions
{
    public const string SectionKey = "Crawler:ScheduledImports";

    /// <summary>
    /// When false, the trigger worker still runs but skips scheduling.
    /// </summary>
    public bool Enabled { get; set; }

    public string TimeZoneId { get; set; } = "Asia/Ho_Chi_Minh";

    /// <summary>Việc làm (job board import).</summary>
    public ScheduledJobBoardImportSection Jobs { get; set; } = new();

    /// <summary>Tin tức.</summary>
    public ScheduledArticleImportSection Articles { get; set; } = new();

    /// <summary>Địa điểm.</summary>
    public ScheduledPlaceImportSection Places { get; set; } = new();
}

public class ScheduledJobBoardImportSection
{
    public bool Enabled { get; set; }

    /// <summary>Standard 5-field cron (minute hour dom month dow).</summary>
    public string Cron { get; set; } = "0 7,17 * * *";

    public ImportJobListingInput Import { get; set; } = new();
}

public class ScheduledArticleImportSection
{
    public bool Enabled { get; set; }

    public string Cron { get; set; } = "0 * * * *";

    public ImportArticleListingInput Import { get; set; } = new();
}

public class ScheduledPlaceImportSection
{
    public bool Enabled { get; set; }

    public string Cron { get; set; } = "0 12 * * *";

    public ImportPlaceListingInput Import { get; set; } = new();
}
