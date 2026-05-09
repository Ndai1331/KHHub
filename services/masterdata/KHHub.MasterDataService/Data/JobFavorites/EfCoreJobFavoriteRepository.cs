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
using KHHub.MasterDataService.Entities.JobFavorites;

namespace KHHub.MasterDataService.Data.JobFavorites;

public abstract class EfCoreJobFavoriteRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, JobFavorite, Guid>
{
    public EfCoreJobFavoriteRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, Guid? userId = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, jobId);
        var ids = query.Select(x => x.JobFavorite.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<JobFavoriteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(jobFavorite => new JobFavoriteWithNavigationProperties { JobFavorite = jobFavorite, Job = dbContext.Set<Job>().FirstOrDefault(c => c.Id == jobFavorite.JobId) }).FirstOrDefault();
    }

    public virtual async Task<List<JobFavoriteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, jobId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JobFavoriteConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<JobFavoriteWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from jobFavorite in (await GetDbSetAsync())
               join job in (await GetDbContextAsync()).Set<Job>() on jobFavorite.JobId equals job.Id into jobs
               from job in jobs.DefaultIfEmpty()
               select new JobFavoriteWithNavigationProperties
               {
                   JobFavorite = jobFavorite,
                   Job = job
               };
    }

    protected virtual IQueryable<JobFavoriteWithNavigationProperties> ApplyFilter(IQueryable<JobFavoriteWithNavigationProperties> query, string? filterText, Guid? userId = null, Guid? jobId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(userId.HasValue, e => e.JobFavorite.UserId == userId).WhereIf(jobId != null && jobId != Guid.Empty, e => e.Job != null && e.Job.Id == jobId);
    }

    public virtual async Task<List<JobFavorite>> GetListAsync(string? filterText = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, userId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? JobFavoriteConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, jobId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<JobFavorite> ApplyFilter(IQueryable<JobFavorite> query, string? filterText = null, Guid? userId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(userId.HasValue, e => e.UserId == userId);
    }
}