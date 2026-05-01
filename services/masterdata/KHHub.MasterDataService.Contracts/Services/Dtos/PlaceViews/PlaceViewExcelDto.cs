using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceViews;

public abstract class PlaceViewExcelDtoBase
{
    public Guid UserId { get; set; }

    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    public string? Source { get; set; }
}