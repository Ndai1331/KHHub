using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.PlaceTags;
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
using KHHub.MasterDataService.Entities.PlaceTagMappings;

namespace KHHub.MasterDataService.Data.PlaceTagMappings;

public abstract class EfCorePlaceTagMappingRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, PlaceTagMapping, Guid>
{
    public EfCorePlaceTagMappingRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, sortOrderMin, sortOrderMax, placeTagId, placeId);
        var ids = query.Select(x => x.PlaceTagMapping.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<PlaceTagMappingWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(placeTagMapping => new PlaceTagMappingWithNavigationProperties { PlaceTagMapping = placeTagMapping, PlaceTag = dbContext.Set<PlaceTag>().FirstOrDefault(c => c.Id == placeTagMapping.PlaceTagId), Place = dbContext.Set<Place>().FirstOrDefault(c => c.Id == placeTagMapping.PlaceId) }).FirstOrDefault();
    }

    public virtual async Task<List<PlaceTagMappingWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, sortOrderMin, sortOrderMax, placeTagId, placeId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceTagMappingConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<PlaceTagMappingWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from placeTagMapping in (await GetDbSetAsync())
               join placeTag in (await GetDbContextAsync()).Set<PlaceTag>() on placeTagMapping.PlaceTagId equals placeTag.Id into placeTags
               from placeTag in placeTags.DefaultIfEmpty()
               join place in (await GetDbContextAsync()).Set<Place>() on placeTagMapping.PlaceId equals place.Id into places
               from place in places.DefaultIfEmpty()
               select new PlaceTagMappingWithNavigationProperties
               {
                   PlaceTagMapping = placeTagMapping,
                   PlaceTag = placeTag,
                   Place = place
               };
    }

    protected virtual IQueryable<PlaceTagMappingWithNavigationProperties> ApplyFilter(IQueryable<PlaceTagMappingWithNavigationProperties> query, string? filterText, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(isPrimary.HasValue, e => e.PlaceTagMapping.IsPrimary == isPrimary).WhereIf(sortOrderMin.HasValue, e => e.PlaceTagMapping.SortOrder >= sortOrderMin!.Value).WhereIf(sortOrderMax.HasValue, e => e.PlaceTagMapping.SortOrder <= sortOrderMax!.Value).WhereIf(placeTagId != null && placeTagId != Guid.Empty, e => e.PlaceTag != null && e.PlaceTag.Id == placeTagId).WhereIf(placeId != null && placeId != Guid.Empty, e => e.Place != null && e.Place.Id == placeId);
    }

    public virtual async Task<List<PlaceTagMapping>> GetListAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, isPrimary, sortOrderMin, sortOrderMax);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceTagMappingConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null, Guid? placeTagId = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, sortOrderMin, sortOrderMax, placeTagId, placeId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<PlaceTagMapping> ApplyFilter(IQueryable<PlaceTagMapping> query, string? filterText = null, bool? isPrimary = null, int? sortOrderMin = null, int? sortOrderMax = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(isPrimary.HasValue, e => e.IsPrimary == isPrimary).WhereIf(sortOrderMin.HasValue, e => e.SortOrder >= sortOrderMin!.Value).WhereIf(sortOrderMax.HasValue, e => e.SortOrder <= sortOrderMax!.Value);
    }
}