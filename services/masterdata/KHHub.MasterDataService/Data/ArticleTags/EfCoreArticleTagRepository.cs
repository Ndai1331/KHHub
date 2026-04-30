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
using KHHub.MasterDataService.Entities.ArticleTags;

namespace KHHub.MasterDataService.Data.ArticleTags;

public abstract class EfCoreArticleTagRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, ArticleTag, Guid>
{
    public EfCoreArticleTagRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, name, slug, description, usageCountMin, usageCountMax);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<ArticleTag>> GetListAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, name, slug, description, usageCountMin, usageCountMax);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleTagConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, name, slug, description, usageCountMin, usageCountMax);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<ArticleTag> ApplyFilter(IQueryable<ArticleTag> query, string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Name!.Contains(filterText!) || e.Slug!.Contains(filterText!) || e.Description!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(name), e => e.Name.Contains(name)).WhereIf(!string.IsNullOrWhiteSpace(slug), e => e.Slug.Contains(slug)).WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description)).WhereIf(usageCountMin.HasValue, e => e.UsageCount >= usageCountMin!.Value).WhereIf(usageCountMax.HasValue, e => e.UsageCount <= usageCountMax!.Value);
    }
}