using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.PlaceCategories;

namespace KHHub.MasterDataService.Data.PlaceCategories;

public partial interface IPlaceCategoryRepository : IRepository<PlaceCategory, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, CancellationToken cancellationToken = default);
    Task<List<PlaceCategory>> GetListAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, CancellationToken cancellationToken = default);
}