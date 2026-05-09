using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.JobFavorites;

public abstract class GetJobFavoritesInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public Guid? UserId { get; set; }

    public Guid? JobId { get; set; }

    public GetJobFavoritesInputBase()
    {
    }
}