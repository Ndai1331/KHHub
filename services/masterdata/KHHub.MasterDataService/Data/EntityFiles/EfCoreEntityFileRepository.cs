using KHHub.MasterDataService.Entities.MediaFiles;
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
using KHHub.MasterDataService.Entities.EntityFiles;

namespace KHHub.MasterDataService.Data.EntityFiles;

public abstract class EfCoreEntityFileRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, EntityFile, Guid>
{
    public EfCoreEntityFileRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, entityType, entityId, collection, sortOrderMin, sortOrderMax, isPrimary, mediaFileId);
        var ids = query.Select(x => x.EntityFile.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<EntityFileWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(entityFile => new EntityFileWithNavigationProperties { EntityFile = entityFile, MediaFile = dbContext.Set<MediaFile>().FirstOrDefault(c => c.Id == entityFile.MediaFileId) }).FirstOrDefault();
    }

    public virtual async Task<List<EntityFileWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, entityType, entityId, collection, sortOrderMin, sortOrderMax, isPrimary, mediaFileId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EntityFileConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<EntityFileWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from entityFile in (await GetDbSetAsync())
               join mediaFile in (await GetDbContextAsync()).Set<MediaFile>() on entityFile.MediaFileId equals mediaFile.Id into mediaFiles
               from mediaFile in mediaFiles.DefaultIfEmpty()
               select new EntityFileWithNavigationProperties
               {
                   EntityFile = entityFile,
                   MediaFile = mediaFile
               };
    }

    protected virtual IQueryable<EntityFileWithNavigationProperties> ApplyFilter(IQueryable<EntityFileWithNavigationProperties> query, string? filterText, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EntityFile.EntityType!.Contains(filterText!) || e.EntityFile.Collection!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(entityType), e => e.EntityFile.EntityType.Contains(entityType)).WhereIf(entityId.HasValue, e => e.EntityFile.EntityId == entityId).WhereIf(!string.IsNullOrWhiteSpace(collection), e => e.EntityFile.Collection.Contains(collection)).WhereIf(sortOrderMin.HasValue, e => e.EntityFile.SortOrder >= sortOrderMin!.Value).WhereIf(sortOrderMax.HasValue, e => e.EntityFile.SortOrder <= sortOrderMax!.Value).WhereIf(isPrimary.HasValue, e => e.EntityFile.IsPrimary == isPrimary).WhereIf(mediaFileId != null && mediaFileId != Guid.Empty, e => e.MediaFile != null && e.MediaFile.Id == mediaFileId);
    }

    public virtual async Task<List<EntityFile>> GetListAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, entityType, entityId, collection, sortOrderMin, sortOrderMax, isPrimary);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? EntityFileConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null, Guid? mediaFileId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, entityType, entityId, collection, sortOrderMin, sortOrderMax, isPrimary, mediaFileId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<EntityFile> ApplyFilter(IQueryable<EntityFile> query, string? filterText = null, string? entityType = null, Guid? entityId = null, string? collection = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isPrimary = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.EntityType!.Contains(filterText!) || e.Collection!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(entityType), e => e.EntityType.Contains(entityType)).WhereIf(entityId.HasValue, e => e.EntityId == entityId).WhereIf(!string.IsNullOrWhiteSpace(collection), e => e.Collection.Contains(collection)).WhereIf(sortOrderMin.HasValue, e => e.SortOrder >= sortOrderMin!.Value).WhereIf(sortOrderMax.HasValue, e => e.SortOrder <= sortOrderMax!.Value).WhereIf(isPrimary.HasValue, e => e.IsPrimary == isPrimary);
    }
}