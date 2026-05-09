using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.JobFavorites;

namespace KHHub.MasterDataService.Data.JobFavorites;

public partial interface IJobFavoriteRepository : IRepository<JobFavorite, Guid>
{
    Task DeleteAllAsync(string? filterText = null, Guid? userId = null, Guid? jobId = null, CancellationToken cancellationToken = default);
    Task<JobFavoriteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<JobFavoriteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<JobFavorite>> GetListAsync(string? filterText = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, Guid? jobId = null, CancellationToken cancellationToken = default);
}