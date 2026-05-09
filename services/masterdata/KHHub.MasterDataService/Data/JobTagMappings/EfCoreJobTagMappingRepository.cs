using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Entities.JobTags;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Dynamic.Core.Exceptions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using KHHub.MasterDataService.Data;
using KHHub.MasterDataService.Entities.JobTagMappings;

namespace KHHub.MasterDataService.Data.JobTagMappings;

public abstract class EfCoreJobTagMappingRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, JobTagMapping, Guid>
{
    public EfCoreJobTagMappingRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, sortOrder, jobTagId, jobId);
        var ids = query.Select(x => x.JobTagMapping.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<JobTagMappingWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(jobTagMapping => new JobTagMappingWithNavigationProperties { JobTagMapping = jobTagMapping, JobTag = dbContext.Set<JobTag>().FirstOrDefault(c => c.Id == jobTagMapping.JobTagId), Job = dbContext.Set<Job>().FirstOrDefault(c => c.Id == jobTagMapping.JobId) }).FirstOrDefault();
    }

    public virtual async Task<List<JobTagMappingWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, sortOrder, jobTagId, jobId);
        query = ApplySortingWithFallback(
            query,
            sorting,
            JobTagMappingConsts.GetDefaultSorting(true));
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<JobTagMappingWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from jobTagMapping in (await GetDbSetAsync())
               join jobTag in (await GetDbContextAsync()).Set<JobTag>() on jobTagMapping.JobTagId equals jobTag.Id into jobTags
               from jobTag in jobTags.DefaultIfEmpty()
               join job in (await GetDbContextAsync()).Set<Job>() on jobTagMapping.JobId equals job.Id into jobs
               from job in jobs.DefaultIfEmpty()
               select new JobTagMappingWithNavigationProperties
               {
                   JobTagMapping = jobTagMapping,
                   JobTag = jobTag,
                   Job = job
               };
    }

    protected virtual IQueryable<JobTagMappingWithNavigationProperties> ApplyFilter(IQueryable<JobTagMappingWithNavigationProperties> query, string? filterText, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.JobTagMapping.SortOrder!.Contains(filterText!)).WhereIf(isPrimary.HasValue, e => e.JobTagMapping.IsPrimary == isPrimary).WhereIf(!string.IsNullOrWhiteSpace(sortOrder), e => e.JobTagMapping.SortOrder.Contains(sortOrder)).WhereIf(jobTagId != null && jobTagId != Guid.Empty, e => e.JobTag != null && e.JobTag.Id == jobTagId).WhereIf(jobId != null && jobId != Guid.Empty, e => e.Job != null && e.Job.Id == jobId);
    }

    public virtual async Task<List<JobTagMapping>> GetListAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, isPrimary, sortOrder);
        query = ApplySortingWithFallback(
            query,
            sorting,
            JobTagMappingConsts.GetDefaultSorting(false));
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, bool? isPrimary = null, string? sortOrder = null, Guid? jobTagId = null, Guid? jobId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, sortOrder, jobTagId, jobId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<JobTagMapping> ApplyFilter(IQueryable<JobTagMapping> query, string? filterText = null, bool? isPrimary = null, string? sortOrder = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.SortOrder!.Contains(filterText!)).WhereIf(isPrimary.HasValue, e => e.IsPrimary == isPrimary).WhereIf(!string.IsNullOrWhiteSpace(sortOrder), e => e.SortOrder.Contains(sortOrder));
    }

    private static IQueryable<T> ApplySortingWithFallback<T>(
        IQueryable<T> query,
        string? sorting,
        string fallbackSorting)
    {
        var requestedSorting = string.IsNullOrWhiteSpace(sorting) ? fallbackSorting : sorting;
        try
        {
            return query.OrderBy(requestedSorting);
        }
        catch (ParseException)
        {
            // Keep API stable even when caller sends invalid dynamic-linq field names.
            return query.OrderBy(fallbackSorting);
        }
    }
}