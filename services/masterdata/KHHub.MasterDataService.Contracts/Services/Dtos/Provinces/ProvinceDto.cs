using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Provinces;

public abstract class ProvinceDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string ConcurrencyStamp { get; set; } = null!;
}