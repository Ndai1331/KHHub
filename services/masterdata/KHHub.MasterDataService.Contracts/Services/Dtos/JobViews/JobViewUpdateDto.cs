using KHHub.MasterDataService.Entities.JobViews;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobViews;

public abstract class JobViewUpdateDtoBase : IHasConcurrencyStamp
{
    public Guid? UserId { get; set; }

    [StringLength(JobViewConsts.IpAddressMaxLength)]
    public string? IpAddress { get; set; }

    [StringLength(JobViewConsts.DeviceMaxLength)]
    public string? Device { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    [StringLength(JobViewConsts.SourceMaxLength)]
    public string? Source { get; set; }

    public Guid JobId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}