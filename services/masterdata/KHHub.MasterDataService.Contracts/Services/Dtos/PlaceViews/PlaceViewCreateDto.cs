using KHHub.MasterDataService.Entities.PlaceViews;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceViews;

public abstract class PlaceViewCreateDtoBase
{
    public Guid UserId { get; set; }

    [StringLength(PlaceViewConsts.IpAddressMaxLength)]
    public string? IpAddress { get; set; }

    [StringLength(PlaceViewConsts.DeviceMaxLength)]
    public string? Device { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; } = 0;
    [StringLength(PlaceViewConsts.SourceMaxLength)]
    public string? Source { get; set; }

    public Guid PlaceId { get; set; }
}