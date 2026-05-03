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
using KHHub.MasterDataService.Entities.Provinces;

namespace KHHub.MasterDataService.Data.Provinces;

public abstract class EfCoreProvinceRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, Province, Guid>
{
    public EfCoreProvinceRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? code = null, string? name = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, code, name);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<Province>> GetListAsync(string? filterText = null, string? code = null, string? name = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, code, name);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ProvinceConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? code = null, string? name = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, code, name);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<Province> ApplyFilter(IQueryable<Province> query, string? filterText = null, string? code = null, string? name = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nCode = MasterDataStringSearch.NormalizeForContains(code);
        var nName = MasterDataStringSearch.NormalizeForContains(name);
        return query.WhereIf(nf != null, e => (e.Code != null && e.Code.ToLower().Contains(nf!)) || (e.Name != null && e.Name.ToLower().Contains(nf!))).WhereIf(nCode != null, e => e.Code != null && e.Code.ToLower().Contains(nCode!)).WhereIf(nName != null, e => e.Name != null && e.Name.ToLower().Contains(nName!));
    }
}