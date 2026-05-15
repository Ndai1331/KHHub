using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceCrawler;

public class PlaceCrawlerUpsertResultDto
{
    public Guid PlaceId { get; set; }

    public bool WasCreated { get; set; }
}
