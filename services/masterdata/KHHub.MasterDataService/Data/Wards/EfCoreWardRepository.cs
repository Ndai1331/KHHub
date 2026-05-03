using KHHub.MasterDataService.Entities.Provinces;
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
using KHHub.MasterDataService.Entities.Wards;

namespace KHHub.MasterDataService.Data.Wards;

public abstract class EfCoreWardRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, Ward, Guid>
{
    public EfCoreWardRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? code = null, string? name = null, Guid? provinceId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, code, name, provinceId);
        var ids = query.Select(x => x.Ward.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<WardWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(ward => new WardWithNavigationProperties { Ward = ward, Province = dbContext.Set<Province>().FirstOrDefault(c => c.Id == ward.ProvinceId) }).FirstOrDefault();
    }

    public virtual async Task<List<WardWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? code = null, string? name = null, Guid? provinceId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, code, name, provinceId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WardConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<WardWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from ward in (await GetDbSetAsync())
               join province in (await GetDbContextAsync()).Set<Province>() on ward.ProvinceId equals province.Id into provinces
               from province in provinces.DefaultIfEmpty()
               select new WardWithNavigationProperties
               {
                   Ward = ward,
                   Province = province
               };
    }

    protected virtual IQueryable<WardWithNavigationProperties> ApplyFilter(IQueryable<WardWithNavigationProperties> query, string? filterText, string? code = null, string? name = null, Guid? provinceId = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nCode = MasterDataStringSearch.NormalizeForContains(code);
        var nName = MasterDataStringSearch.NormalizeForContains(name);
        return query.WhereIf(nf != null, e => (e.Ward.Code != null && e.Ward.Code.ToLower().Contains(nf!)) || (e.Ward.Name != null && e.Ward.Name.ToLower().Contains(nf!))).WhereIf(nCode != null, e => e.Ward.Code != null && e.Ward.Code.ToLower().Contains(nCode!)).WhereIf(nName != null, e => e.Ward.Name != null && e.Ward.Name.ToLower().Contains(nName!)).WhereIf(provinceId != null && provinceId != Guid.Empty, e => e.Province != null && e.Province.Id == provinceId);
    }

    public virtual async Task<List<Ward>> GetListAsync(string? filterText = null, string? code = null, string? name = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? WardConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? code = null, string? name = null, Guid? provinceId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, code, name, provinceId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Ward> ApplyFilter(IQueryable<Ward> query, string? filterText = null, string? code = null, string? name = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nCode = MasterDataStringSearch.NormalizeForContains(code);
        var nName = MasterDataStringSearch.NormalizeForContains(name);
        return query.WhereIf(nf != null, e => (e.Code != null && e.Code.ToLower().Contains(nf!)) || (e.Name != null && e.Name.ToLower().Contains(nf!))).WhereIf(nCode != null, e => e.Code != null && e.Code.ToLower().Contains(nCode!)).WhereIf(nName != null, e => e.Name != null && e.Name.ToLower().Contains(nName!));
    }
}