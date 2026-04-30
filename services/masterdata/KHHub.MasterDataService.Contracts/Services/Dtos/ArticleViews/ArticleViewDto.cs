using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.ArticleViews;

public abstract class ArticleViewDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public string? Source { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    public Guid UserId { get; set; }

    public Guid ArticleId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}