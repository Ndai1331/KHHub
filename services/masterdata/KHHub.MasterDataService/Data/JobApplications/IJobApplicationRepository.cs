using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.JobApplications;

namespace KHHub.MasterDataService.Data.JobApplications;

public partial interface IJobApplicationRepository : IRepository<JobApplication, Guid>
{
    Task DeleteAllAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null, CancellationToken cancellationToken = default);
    Task<JobApplicationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<JobApplicationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<JobApplication>> GetListAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null, CancellationToken cancellationToken = default);
}