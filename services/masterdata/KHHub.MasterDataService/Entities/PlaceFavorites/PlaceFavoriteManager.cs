using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.PlaceFavorites;

namespace KHHub.MasterDataService.Entities.PlaceFavorites;

public abstract class PlaceFavoriteManagerBase : DomainService
{
    protected IPlaceFavoriteRepository _placeFavoriteRepository;

    public PlaceFavoriteManagerBase(IPlaceFavoriteRepository placeFavoriteRepository)
    {
        _placeFavoriteRepository = placeFavoriteRepository;
    }

    public virtual async Task<PlaceFavorite> CreateAsync(Guid placeId, Guid? userId = null)
    {
        Check.NotNull(placeId, nameof(placeId));
        var placeFavorite = new PlaceFavorite(GuidGenerator.Create(), placeId, userId);
        return await _placeFavoriteRepository.InsertAsync(placeFavorite);
    }

    public virtual async Task<PlaceFavorite> UpdateAsync(Guid id, Guid placeId, Guid? userId = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(placeId, nameof(placeId));
        var placeFavorite = await _placeFavoriteRepository.GetAsync(id);
        placeFavorite.PlaceId = placeId;
        placeFavorite.UserId = userId;
        placeFavorite.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeFavoriteRepository.UpdateAsync(placeFavorite);
    }
}