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
using KHHub.MasterDataService.Entities.JobViews;

namespace KHHub.MasterDataService.Data.JobViews;

public abstract class EfCoreJobViewRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, JobView, Guid>
{
    public EfCoreJobViewRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source, jobId);
        var ids = query.Select(x => x.JobView.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<JobViewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(jobView => new JobViewWithNavigationProperties { JobView = jobView, Job = dbContext.Set<Job>().FirstOrDefault(c => c.Id == jobView.JobId) }).FirstOrDefault();
    }

    public virtual async Task<List<JobViewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source, jobId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JobViewConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<JobViewWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from jobView in (await GetDbSetAsync())
               join job in (await GetDbContextAsync()).Set<Job>() on jobView.JobId equals job.Id into jobs
               from job in jobs.DefaultIfEmpty()
               select new JobViewWithNavigationProperties
               {
                   JobView = jobView,
                   Job = job
               };
    }

    protected virtual IQueryable<JobViewWithNavigationProperties> ApplyFilter(IQueryable<JobViewWithNavigationProperties> query, string? filterText, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.JobView.IpAddress!.Contains(filterText!) || e.JobView.Device!.Contains(filterText!) || e.JobView.Source!.Contains(filterText!)).WhereIf(userId.HasValue, e => e.JobView.UserId == userId).WhereIf(!string.IsNullOrWhiteSpace(ipAddress), e => e.JobView.IpAddress.Contains(ipAddress)).WhereIf(!string.IsNullOrWhiteSpace(device), e => e.JobView.Device.Contains(device)).WhereIf(viewedAtMin.HasValue, e => e.JobView.ViewedAt >= viewedAtMin!.Value).WhereIf(viewedAtMax.HasValue, e => e.JobView.ViewedAt <= viewedAtMax!.Value).WhereIf(durationMin.HasValue, e => e.JobView.Duration >= durationMin!.Value).WhereIf(durationMax.HasValue, e => e.JobView.Duration <= durationMax!.Value).WhereIf(!string.IsNullOrWhiteSpace(source), e => e.JobView.Source.Contains(source)).WhereIf(jobId != null && jobId != Guid.Empty, e => e.Job != null && e.Job.Id == jobId);
    }

    public virtual async Task<List<JobView>> GetListAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JobViewConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source, jobId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<JobView> ApplyFilter(IQueryable<JobView> query, string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.IpAddress!.Contains(filterText!) || e.Device!.Contains(filterText!) || e.Source!.Contains(filterText!)).WhereIf(userId.HasValue, e => e.UserId == userId).WhereIf(!string.IsNullOrWhiteSpace(ipAddress), e => e.IpAddress.Contains(ipAddress)).WhereIf(!string.IsNullOrWhiteSpace(device), e => e.Device.Contains(device)).WhereIf(viewedAtMin.HasValue, e => e.ViewedAt >= viewedAtMin!.Value).WhereIf(viewedAtMax.HasValue, e => e.ViewedAt <= viewedAtMax!.Value).WhereIf(durationMin.HasValue, e => e.Duration >= durationMin!.Value).WhereIf(durationMax.HasValue, e => e.Duration <= durationMax!.Value).WhereIf(!string.IsNullOrWhiteSpace(source), e => e.Source.Contains(source));
    }
}