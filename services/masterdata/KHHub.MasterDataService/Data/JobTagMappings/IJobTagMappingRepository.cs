using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.JobTagMappings;

namespace KHHub.MasterDataService.Data.JobTagMappings;

public partial interface IJobTagMappingRepository : IRepository<JobTagMapping, Guid>
{
    Task DeleteAllAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null, CancellationToken cancellationToken = default);
    Task<JobTagMappingWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<JobTagMappingWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<JobTagMapping>> GetListAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null, CancellationToken cancellationToken = default);
}