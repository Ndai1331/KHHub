using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceViews;

public abstract class PlaceViewDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid UserId { get; set; }

    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    public string? Source { get; set; }

    public Guid PlaceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}