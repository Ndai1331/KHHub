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
using KHHub.MasterDataService.Entities.PlaceTags;

namespace KHHub.MasterDataService.Data.PlaceTags;

public abstract class EfCorePlaceTagRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, PlaceTag, Guid>
{
    public EfCorePlaceTagRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, name, slug, description, usageCountMin, usageCountMax);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<PlaceTag>> GetListAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, slug, description, usageCountMin, usageCountMax);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceTagConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, slug, description, usageCountMin, usageCountMax);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<PlaceTag> ApplyFilter(IQueryable<PlaceTag> query, string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nName = MasterDataStringSearch.NormalizeForContains(name);
        var nSlug = MasterDataStringSearch.NormalizeForContains(slug);
        var nDescription = MasterDataStringSearch.NormalizeForContains(description);
        return query.WhereIf(nf != null, e => (e.Name != null && e.Name.ToLower().Contains(nf!)) || (e.Slug != null && e.Slug.ToLower().Contains(nf!)) || (e.Description != null && e.Description.ToLower().Contains(nf!))).WhereIf(nName != null, e => e.Name != null && e.Name.ToLower().Contains(nName!)).WhereIf(nSlug != null, e => e.Slug != null && e.Slug.ToLower().Contains(nSlug!)).WhereIf(nDescription != null, e => e.Description != null && e.Description.ToLower().Contains(nDescription!)).WhereIf(usageCountMin.HasValue, e => e.UsageCount >= usageCountMin!.Value).WhereIf(usageCountMax.HasValue, e => e.UsageCount <= usageCountMax!.Value);
    }
}