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
using KHHub.MasterDataService.Entities.PlaceCategories;

namespace KHHub.MasterDataService.Data.PlaceCategories;

public abstract class EfCorePlaceCategoryRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, PlaceCategory, Guid>
{
    public EfCorePlaceCategoryRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, name, slug, description, icon, color, parentId, displayOrderMin, displayOrderMax, isActive);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<PlaceCategory>> GetListAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, slug, description, icon, color, parentId, displayOrderMin, displayOrderMax, isActive);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceCategoryConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, slug, description, icon, color, parentId, displayOrderMin, displayOrderMax, isActive);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<PlaceCategory> ApplyFilter(IQueryable<PlaceCategory> query, string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null)
    {
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nName = MasterDataStringSearch.NormalizeForContains(name);
        var nSlug = MasterDataStringSearch.NormalizeForContains(slug);
        var nDescription = MasterDataStringSearch.NormalizeForContains(description);
        var nIcon = MasterDataStringSearch.NormalizeForContains(icon);
        var nColor = MasterDataStringSearch.NormalizeForContains(color);
        return query.WhereIf(nf != null, e => (e.Name != null && e.Name.ToLower().Contains(nf!)) || (e.Slug != null && e.Slug.ToLower().Contains(nf!)) || (e.Description != null && e.Description.ToLower().Contains(nf!)) || (e.Icon != null && e.Icon.ToLower().Contains(nf!)) || (e.Color != null && e.Color.ToLower().Contains(nf!))).WhereIf(nName != null, e => e.Name != null && e.Name.ToLower().Contains(nName!)).WhereIf(nSlug != null, e => e.Slug != null && e.Slug.ToLower().Contains(nSlug!)).WhereIf(nDescription != null, e => e.Description != null && e.Description.ToLower().Contains(nDescription!)).WhereIf(nIcon != null, e => e.Icon != null && e.Icon.ToLower().Contains(nIcon!)).WhereIf(nColor != null, e => e.Color != null && e.Color.ToLower().Contains(nColor!)).WhereIf(parentId.HasValue, e => e.ParentId == parentId).WhereIf(displayOrderMin.HasValue, e => e.DisplayOrder >= displayOrderMin!.Value).WhereIf(displayOrderMax.HasValue, e => e.DisplayOrder <= displayOrderMax!.Value).WhereIf(isActive.HasValue, e => e.IsActive == isActive);
    }
}