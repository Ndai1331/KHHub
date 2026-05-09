using System;

namespace KHHub.MasterDataService.Services.Dtos.JobViews;

public abstract class JobViewExcelDtoBase
{
    public Guid? UserId { get; set; }

    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    public string? Source { get; set; }
}