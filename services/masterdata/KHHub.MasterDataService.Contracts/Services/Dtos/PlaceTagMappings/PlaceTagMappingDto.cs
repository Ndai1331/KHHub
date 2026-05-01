using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

public abstract class PlaceTagMappingDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public bool IsPrimary { get; set; }

    public int SortOrder { get; set; }

    public Guid PlaceTagId { get; set; }

    public Guid PlaceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}