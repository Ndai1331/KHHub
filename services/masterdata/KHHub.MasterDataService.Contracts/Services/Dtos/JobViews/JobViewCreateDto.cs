using KHHub.MasterDataService.Entities.JobViews;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobViews;

public abstract class JobViewCreateDtoBase
{
    public Guid? UserId { get; set; }

    [StringLength(JobViewConsts.IpAddressMaxLength)]
    public string? IpAddress { get; set; }

    [StringLength(JobViewConsts.DeviceMaxLength)]
    public string? Device { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; } = 0;
    [StringLength(JobViewConsts.SourceMaxLength)]
    public string? Source { get; set; }

    public Guid JobId { get; set; }
}