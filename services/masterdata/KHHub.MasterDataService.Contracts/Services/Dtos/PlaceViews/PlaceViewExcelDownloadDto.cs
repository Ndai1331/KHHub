using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceViews;

public abstract class PlaceViewExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public Guid? UserId { get; set; }

    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public DateTime? ViewedAtMin { get; set; }

    public DateTime? ViewedAtMax { get; set; }

    public int? DurationMin { get; set; }

    public int? DurationMax { get; set; }

    public string? Source { get; set; }

    public Guid? PlaceId { get; set; }

    public PlaceViewExcelDownloadDtoBase()
    {
    }
}