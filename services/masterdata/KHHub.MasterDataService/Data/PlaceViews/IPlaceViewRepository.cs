using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.PlaceViews;

namespace KHHub.MasterDataService.Data.PlaceViews;

public partial interface IPlaceViewRepository : IRepository<PlaceView, Guid>
{
    Task DeleteAllAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null, CancellationToken cancellationToken = default);
    Task<PlaceViewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<PlaceViewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<PlaceView>> GetListAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null, CancellationToken cancellationToken = default);
}