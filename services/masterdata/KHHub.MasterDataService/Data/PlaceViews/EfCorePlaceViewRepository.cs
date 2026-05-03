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
using KHHub.MasterDataService.Entities.PlaceViews;

namespace KHHub.MasterDataService.Data.PlaceViews;

public abstract class EfCorePlaceViewRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, PlaceView, Guid>
{
    public EfCorePlaceViewRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source, placeId);
        var ids = query.Select(x => x.PlaceView.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<PlaceViewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(placeView => new PlaceViewWithNavigationProperties { PlaceView = placeView, Place = dbContext.Set<Place>().FirstOrDefault(c => c.Id == placeView.PlaceId) }).FirstOrDefault();
    }

    public virtual async Task<List<PlaceViewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source, placeId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceViewConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<PlaceViewWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from placeView in (await GetDbSetAsync())
               join place in (await GetDbContextAsync()).Set<Place>() on placeView.PlaceId equals place.Id into places
               from place in places.DefaultIfEmpty()
               select new PlaceViewWithNavigationProperties
               {
                   PlaceView = placeView,
                   Place = place
               };
    }

    protected virtual IQueryable<PlaceViewWithNavigationProperties> ApplyFilter(IQueryable<PlaceViewWithNavigationProperties> query, string? filterText, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nIp = MasterDataStringSearch.NormalizeForContains(ipAddress);
        var nDevice = MasterDataStringSearch.NormalizeForContains(device);
        var nSource = MasterDataStringSearch.NormalizeForContains(source);
        return query.WhereIf(nf != null, e => (e.PlaceView.IpAddress != null && e.PlaceView.IpAddress.ToLower().Contains(nf!)) || (e.PlaceView.Device != null && e.PlaceView.Device.ToLower().Contains(nf!)) || (e.PlaceView.Source != null && e.PlaceView.Source.ToLower().Contains(nf!))).WhereIf(userId.HasValue, e => e.PlaceView.UserId == userId).WhereIf(nIp != null, e => e.PlaceView.IpAddress != null && e.PlaceView.IpAddress.ToLower().Contains(nIp!)).WhereIf(nDevice != null, e => e.PlaceView.Device != null && e.PlaceView.Device.ToLower().Contains(nDevice!)).WhereIf(viewedAtMin.HasValue, e => e.PlaceView.ViewedAt >= viewedAtMin!.Value).WhereIf(viewedAtMax.HasValue, e => e.PlaceView.ViewedAt <= viewedAtMax!.Value).WhereIf(durationMin.HasValue, e => e.PlaceView.Duration >= durationMin!.Value).WhereIf(durationMax.HasValue, e => e.PlaceView.Duration <= durationMax!.Value).WhereIf(nSource != null, e => e.PlaceView.Source != null && e.PlaceView.Source.ToLower().Contains(nSource!)).WhereIf(placeId != null && placeId != Guid.Empty, e => e.Place != null && e.Place.Id == placeId);
    }

    public virtual async Task<List<PlaceView>> GetListAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceViewConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, userId, ipAddress, device, viewedAtMin, viewedAtMax, durationMin, durationMax, source, placeId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<PlaceView> ApplyFilter(IQueryable<PlaceView> query, string? filterText = null, Guid? userId = null, string? ipAddress = null, string? device = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, string? source = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nIp = MasterDataStringSearch.NormalizeForContains(ipAddress);
        var nDevice = MasterDataStringSearch.NormalizeForContains(device);
        var nSource = MasterDataStringSearch.NormalizeForContains(source);
        return query.WhereIf(nf != null, e => (e.IpAddress != null && e.IpAddress.ToLower().Contains(nf!)) || (e.Device != null && e.Device.ToLower().Contains(nf!)) || (e.Source != null && e.Source.ToLower().Contains(nf!))).WhereIf(userId.HasValue, e => e.UserId == userId).WhereIf(nIp != null, e => e.IpAddress != null && e.IpAddress.ToLower().Contains(nIp!)).WhereIf(nDevice != null, e => e.Device != null && e.Device.ToLower().Contains(nDevice!)).WhereIf(viewedAtMin.HasValue, e => e.ViewedAt >= viewedAtMin!.Value).WhereIf(viewedAtMax.HasValue, e => e.ViewedAt <= viewedAtMax!.Value).WhereIf(durationMin.HasValue, e => e.Duration >= durationMin!.Value).WhereIf(durationMax.HasValue, e => e.Duration <= durationMax!.Value).WhereIf(nSource != null, e => e.Source != null && e.Source.ToLower().Contains(nSource!));
    }
}