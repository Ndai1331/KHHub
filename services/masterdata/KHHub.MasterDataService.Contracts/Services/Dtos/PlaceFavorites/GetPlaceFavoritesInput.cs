using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

public abstract class GetPlaceFavoritesInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public Guid? UserId { get; set; }

    public Guid? PlaceId { get; set; }

    public GetPlaceFavoritesInputBase()
    {
    }
}