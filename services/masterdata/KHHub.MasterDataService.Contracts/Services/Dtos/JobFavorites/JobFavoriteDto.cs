using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobFavorites;

public abstract class JobFavoriteDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid UserId { get; set; }

    public Guid JobId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}