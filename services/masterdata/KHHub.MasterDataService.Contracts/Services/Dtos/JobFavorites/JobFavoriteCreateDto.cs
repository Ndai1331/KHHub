using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobFavorites;

public abstract class JobFavoriteCreateDtoBase
{
    public Guid UserId { get; set; }

    public Guid JobId { get; set; }
}