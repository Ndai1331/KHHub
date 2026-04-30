using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleViews;

public abstract class ArticleViewExcelDtoBase
{
    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public string? Source { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    public Guid UserId { get; set; }
}