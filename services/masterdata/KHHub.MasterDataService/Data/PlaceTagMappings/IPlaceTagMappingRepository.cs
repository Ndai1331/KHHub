using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.PlaceTagMappings;

namespace KHHub.MasterDataService.Data.PlaceTagMappings;

public partial interface IPlaceTagMappingRepository : IRepository<PlaceTagMapping, Guid>
{
    Task DeleteAllAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null, CancellationToken cancellationToken = default);
    Task<PlaceTagMappingWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<PlaceTagMappingWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<PlaceTagMapping>> GetListAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null, CancellationToken cancellationToken = default);
}