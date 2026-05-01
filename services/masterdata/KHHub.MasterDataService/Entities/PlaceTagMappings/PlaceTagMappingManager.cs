using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.PlaceTagMappings;

namespace KHHub.MasterDataService.Entities.PlaceTagMappings;

public abstract class PlaceTagMappingManagerBase : DomainService
{
    protected IPlaceTagMappingRepository _placeTagMappingRepository;

    public PlaceTagMappingManagerBase(IPlaceTagMappingRepository placeTagMappingRepository)
    {
        _placeTagMappingRepository = placeTagMappingRepository;
    }

    public virtual async Task<PlaceTagMapping> CreateAsync(Guid placeTagId, Guid placeId, bool isPrimary, int sortOrder)
    {
        Check.NotNull(placeTagId, nameof(placeTagId));
        Check.NotNull(placeId, nameof(placeId));
        var placeTagMapping = new PlaceTagMapping(GuidGenerator.Create(), placeTagId, placeId, isPrimary, sortOrder);
        return await _placeTagMappingRepository.InsertAsync(placeTagMapping);
    }

    public virtual async Task<PlaceTagMapping> UpdateAsync(Guid id, Guid placeTagId, Guid placeId, bool isPrimary, int sortOrder, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(placeTagId, nameof(placeTagId));
        Check.NotNull(placeId, nameof(placeId));
        var placeTagMapping = await _placeTagMappingRepository.GetAsync(id);
        placeTagMapping.PlaceTagId = placeTagId;
        placeTagMapping.PlaceId = placeId;
        placeTagMapping.IsPrimary = isPrimary;
        placeTagMapping.SortOrder = sortOrder;
        placeTagMapping.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeTagMappingRepository.UpdateAsync(placeTagMapping);
    }
}