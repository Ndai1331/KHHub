using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.Wards;

namespace KHHub.MasterDataService.Data.Wards;

public partial interface IWardRepository : IRepository<Ward, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? code = null, string? name = null, Guid? provinceId = null, CancellationToken cancellationToken = default);
    Task<WardWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<WardWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? code = null, string? name = null, Guid? provinceId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<Ward>> GetListAsync(string? filterText = null, string? code = null, string? name = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? code = null, string? name = null, Guid? provinceId = null, CancellationToken cancellationToken = default);
}