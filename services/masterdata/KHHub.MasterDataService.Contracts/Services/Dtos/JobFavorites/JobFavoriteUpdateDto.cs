using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobFavorites;

public abstract class JobFavoriteUpdateDtoBase : IHasConcurrencyStamp
{
    public Guid UserId { get; set; }

    public Guid JobId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}