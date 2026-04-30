using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

public abstract class ArticleTagMappingDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public bool IsPrimary { get; set; }

    public int Order { get; set; }

    public Guid ArticleTagId { get; set; }

    public Guid ArticleId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}