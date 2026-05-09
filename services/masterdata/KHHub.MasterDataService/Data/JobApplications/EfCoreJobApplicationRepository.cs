using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using KHHub.MasterDataService.Data;
using KHHub.MasterDataService.Entities.JobApplications;

namespace KHHub.MasterDataService.Data.JobApplications;

public abstract class EfCoreJobApplicationRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, JobApplication, Guid>
{
    public EfCoreJobApplicationRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, fullName, email, phoneNumber, cvUrl, portfolioUrl, appliedAtMin, appliedAtMax, status, jobId);
        var ids = query.Select(x => x.JobApplication.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<JobApplicationWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(jobApplication => new JobApplicationWithNavigationProperties { JobApplication = jobApplication, Job = dbContext.Set<Job>().FirstOrDefault(c => c.Id == jobApplication.JobId) }).FirstOrDefault();
    }

    public virtual async Task<List<JobApplicationWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, fullName, email, phoneNumber, cvUrl, portfolioUrl, appliedAtMin, appliedAtMax, status, jobId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JobApplicationConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<JobApplicationWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from jobApplication in (await GetDbSetAsync())
               join job in (await GetDbContextAsync()).Set<Job>() on jobApplication.JobId equals job.Id into jobs
               from job in jobs.DefaultIfEmpty()
               select new JobApplicationWithNavigationProperties
               {
                   JobApplication = jobApplication,
                   Job = job
               };
    }

    protected virtual IQueryable<JobApplicationWithNavigationProperties> ApplyFilter(IQueryable<JobApplicationWithNavigationProperties> query, string? filterText, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.JobApplication.FullName!.Contains(filterText!) || e.JobApplication.Email!.Contains(filterText!) || e.JobApplication.PhoneNumber!.Contains(filterText!) || e.JobApplication.CvUrl!.Contains(filterText!) || e.JobApplication.PortfolioUrl!.Contains(filterText!)).WhereIf(userId.HasValue, e => e.JobApplication.UserId == userId).WhereIf(!string.IsNullOrWhiteSpace(fullName), e => e.JobApplication.FullName.Contains(fullName)).WhereIf(!string.IsNullOrWhiteSpace(email), e => e.JobApplication.Email.Contains(email)).WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => e.JobApplication.PhoneNumber.Contains(phoneNumber)).WhereIf(!string.IsNullOrWhiteSpace(cvUrl), e => e.JobApplication.CvUrl.Contains(cvUrl)).WhereIf(!string.IsNullOrWhiteSpace(portfolioUrl), e => e.JobApplication.PortfolioUrl.Contains(portfolioUrl)).WhereIf(appliedAtMin.HasValue, e => e.JobApplication.AppliedAt >= appliedAtMin!.Value).WhereIf(appliedAtMax.HasValue, e => e.JobApplication.AppliedAt <= appliedAtMax!.Value).WhereIf(status.HasValue, e => e.JobApplication.Status == status).WhereIf(jobId != null && jobId != Guid.Empty, e => e.Job != null && e.Job.Id == jobId);
    }

    public virtual async Task<List<JobApplication>> GetListAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, userId, fullName, email, phoneNumber, cvUrl, portfolioUrl, appliedAtMin, appliedAtMax, status);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JobApplicationConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, fullName, email, phoneNumber, cvUrl, portfolioUrl, appliedAtMin, appliedAtMax, status, jobId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<JobApplication> ApplyFilter(IQueryable<JobApplication> query, string? filterText = null, Guid? userId = null, string? fullName = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, DateTime? appliedAtMin = null, DateTime? appliedAtMax = null, ApplicationStatus? status = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FullName!.Contains(filterText!) || e.Email!.Contains(filterText!) || e.PhoneNumber!.Contains(filterText!) || e.CvUrl!.Contains(filterText!) || e.PortfolioUrl!.Contains(filterText!)).WhereIf(userId.HasValue, e => e.UserId == userId).WhereIf(!string.IsNullOrWhiteSpace(fullName), e => e.FullName.Contains(fullName)).WhereIf(!string.IsNullOrWhiteSpace(email), e => e.Email.Contains(email)).WhereIf(!string.IsNullOrWhiteSpace(phoneNumber), e => e.PhoneNumber.Contains(phoneNumber)).WhereIf(!string.IsNullOrWhiteSpace(cvUrl), e => e.CvUrl.Contains(cvUrl)).WhereIf(!string.IsNullOrWhiteSpace(portfolioUrl), e => e.PortfolioUrl.Contains(portfolioUrl)).WhereIf(appliedAtMin.HasValue, e => e.AppliedAt >= appliedAtMin!.Value).WhereIf(appliedAtMax.HasValue, e => e.AppliedAt <= appliedAtMax!.Value).WhereIf(status.HasValue, e => e.Status == status);
    }
}