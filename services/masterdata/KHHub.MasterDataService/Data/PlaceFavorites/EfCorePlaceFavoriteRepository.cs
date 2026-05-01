using KHHub.MasterDataService.Entities.Places;
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
using KHHub.MasterDataService.Entities.PlaceFavorites;

namespace KHHub.MasterDataService.Data.PlaceFavorites;

public abstract class EfCorePlaceFavoriteRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, PlaceFavorite, Guid>
{
    public EfCorePlaceFavoriteRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, placeId);
        var ids = query.Select(x => x.PlaceFavorite.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<PlaceFavoriteWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(placeFavorite => new PlaceFavoriteWithNavigationProperties { PlaceFavorite = placeFavorite, Place = dbContext.Set<Place>().FirstOrDefault(c => c.Id == placeFavorite.PlaceId) }).FirstOrDefault();
    }

    public virtual async Task<List<PlaceFavoriteWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, placeId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceFavoriteConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<PlaceFavoriteWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from placeFavorite in (await GetDbSetAsync())
               join place in (await GetDbContextAsync()).Set<Place>() on placeFavorite.PlaceId equals place.Id into places
               from place in places.DefaultIfEmpty()
               select new PlaceFavoriteWithNavigationProperties
               {
                   PlaceFavorite = placeFavorite,
                   Place = place
               };
    }

    protected virtual IQueryable<PlaceFavoriteWithNavigationProperties> ApplyFilter(IQueryable<PlaceFavoriteWithNavigationProperties> query, string? filterText, Guid? userId = null, Guid? placeId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(userId.HasValue, e => e.PlaceFavorite.UserId == userId).WhereIf(placeId != null && placeId != Guid.Empty, e => e.Place != null && e.Place.Id == placeId);
    }

    public virtual async Task<List<PlaceFavorite>> GetListAsync(string? filterText = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, userId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceFavoriteConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, placeId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<PlaceFavorite> ApplyFilter(IQueryable<PlaceFavorite> query, string? filterText = null, Guid? userId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(userId.HasValue, e => e.UserId == userId);
    }
}