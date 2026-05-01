using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.PlaceFavorites;

namespace KHHub.MasterDataService.Data.PlaceFavorites;

public partial interface IPlaceFavoriteRepository : IRepository<PlaceFavorite, Guid>
{
    Task DeleteAllAsync(string? filterText = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default);
    Task<PlaceFavoriteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<PlaceFavoriteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<PlaceFavorite>> GetListAsync(string? filterText = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default);
}