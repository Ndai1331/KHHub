using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.ArticleTags;
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
using KHHub.MasterDataService.Entities.ArticleTagMappings;

namespace KHHub.MasterDataService.Data.ArticleTagMappings;

public abstract class EfCoreArticleTagMappingRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, ArticleTagMapping, Guid>
{
    public EfCoreArticleTagMappingRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, orderMin, orderMax, articleTagId, articleId);
        var ids = query.Select(x => x.ArticleTagMapping.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<ArticleTagMappingWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(articleTagMapping => new ArticleTagMappingWithNavigationProperties { ArticleTagMapping = articleTagMapping, ArticleTag = dbContext.Set<ArticleTag>().FirstOrDefault(c => c.Id == articleTagMapping.ArticleTagId), Article = dbContext.Set<Article>().FirstOrDefault(c => c.Id == articleTagMapping.ArticleId) }).FirstOrDefault();
    }

    public virtual async Task<List<ArticleTagMappingWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, orderMin, orderMax, articleTagId, articleId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleTagMappingConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<ArticleTagMappingWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from articleTagMapping in (await GetDbSetAsync())
               join articleTag in (await GetDbContextAsync()).Set<ArticleTag>() on articleTagMapping.ArticleTagId equals articleTag.Id into articleTags
               from articleTag in articleTags.DefaultIfEmpty()
               join article in (await GetDbContextAsync()).Set<Article>() on articleTagMapping.ArticleId equals article.Id into articles
               from article in articles.DefaultIfEmpty()
               select new ArticleTagMappingWithNavigationProperties
               {
                   ArticleTagMapping = articleTagMapping,
                   ArticleTag = articleTag,
                   Article = article
               };
    }

    protected virtual IQueryable<ArticleTagMappingWithNavigationProperties> ApplyFilter(IQueryable<ArticleTagMappingWithNavigationProperties> query, string? filterText, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(isPrimary.HasValue, e => e.ArticleTagMapping.IsPrimary == isPrimary).WhereIf(orderMin.HasValue, e => e.ArticleTagMapping.Order >= orderMin!.Value).WhereIf(orderMax.HasValue, e => e.ArticleTagMapping.Order <= orderMax!.Value).WhereIf(articleTagId != null && articleTagId != Guid.Empty, e => e.ArticleTag != null && e.ArticleTag.Id == articleTagId).WhereIf(articleId != null && articleId != Guid.Empty, e => e.Article != null && e.Article.Id == articleId);
    }

    public virtual async Task<List<ArticleTagMapping>> GetListAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, isPrimary, orderMin, orderMax);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleTagMappingConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, isPrimary, orderMin, orderMax, articleTagId, articleId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<ArticleTagMapping> ApplyFilter(IQueryable<ArticleTagMapping> query, string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => true).WhereIf(isPrimary.HasValue, e => e.IsPrimary == isPrimary).WhereIf(orderMin.HasValue, e => e.Order >= orderMin!.Value).WhereIf(orderMax.HasValue, e => e.Order <= orderMax!.Value);
    }
}