using System;

namespace KHHub.MasterDataService.Services.Dtos.JobCrawler;

public class JobCrawlerUpsertResultDto
{
    public Guid JobId { get; set; }

    public bool WasCreated { get; set; }
}
