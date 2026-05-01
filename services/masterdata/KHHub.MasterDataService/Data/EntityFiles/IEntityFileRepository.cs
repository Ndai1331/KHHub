using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.EntityFiles;

namespace KHHub.MasterDataService.Data.EntityFiles;

public partial interface IEntityFileRepository : IRepository<EntityFile, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null, CancellationToken cancellationToken = default);
    Task<EntityFileWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<EntityFileWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<EntityFile>> GetListAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null, CancellationToken cancellationToken = default);
}