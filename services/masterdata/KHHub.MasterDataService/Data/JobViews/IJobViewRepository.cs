using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.JobViews;

namespace KHHub.MasterDataService.Data.JobViews;

public partial interface IJobViewRepository : IRepository<JobView, Guid>
{
    Task DeleteAllAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null, CancellationToken cancellationToken = default);
    Task<JobViewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<JobViewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<JobView>> GetListAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null, CancellationToken cancellationToken = default);
}