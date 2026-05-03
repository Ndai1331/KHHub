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
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.Icon!.Contains(filterText!) || e.Color!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name)).WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug)).WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description)).WhereIf(!string.IsNullOrWhiteSpace(icon), e => e.Icon.Contains(icon)).WhereIf(!string.IsNullOrWhiteSpace(color), e => e.Color.Contains(color)).WhereIf(parentId.HasValue, e => e.ParentId == parentId).WhereIf(displayOrderMin.HasValue, e => e.DisplayOrder >= displayOrderMin!.Value).WhereIf(displayOrderMax.HasValue, e => e.DisplayOrder <= displayOrderMax!.Value).WhereIf(isActive.HasValue, e => e.IsActive == isActive);
    }
}