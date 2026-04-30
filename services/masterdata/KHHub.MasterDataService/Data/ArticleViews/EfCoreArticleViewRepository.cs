using KHHub.MasterDataService.Entities.Articles;
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
using KHHub.MasterDataService.Entities.ArticleViews;

namespace KHHub.MasterDataService.Data.ArticleViews;

public abstract class EfCoreArticleViewRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, ArticleView, Guid>
{
    public EfCoreArticleViewRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, ipAddress, device, source, viewedAtMin, viewedAtMax, durationMin, durationMax, userId, articleId);
        var ids = query.Select(x => x.ArticleView.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<ArticleViewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(articleView => new ArticleViewWithNavigationProperties { ArticleView = articleView, Article = dbContext.Set<Article>().FirstOrDefault(c => c.Id == articleView.ArticleId) }).FirstOrDefault();
    }

    public virtual async Task<List<ArticleViewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, ipAddress, device, source, viewedAtMin, viewedAtMax, durationMin, durationMax, userId, articleId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleViewConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<ArticleViewWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from articleView in (await GetDbSetAsync())
               join article in (await GetDbContextAsync()).Set<Article>() on articleView.ArticleId equals article.Id into articles
               from article in articles.DefaultIfEmpty()
               select new ArticleViewWithNavigationProperties
               {
                   ArticleView = articleView,
                   Article = article
               };
    }

    protected virtual IQueryable<ArticleViewWithNavigationProperties> ApplyFilter(IQueryable<ArticleViewWithNavigationProperties> query, string? filterText, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.ArticleView.IpAddress!.Contains(filterText!) || e.ArticleView.Device!.Contains(filterText!) || e.ArticleView.Source!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(ipAddress), e => e.ArticleView.IpAddress.Contains(ipAddress)).WhereIf(!string.IsNullOrWhiteSpace(device), e => e.ArticleView.Device.Contains(device)).WhereIf(!string.IsNullOrWhiteSpace(source), e => e.ArticleView.Source.Contains(source)).WhereIf(viewedAtMin.HasValue, e => e.ArticleView.ViewedAt >= viewedAtMin!.Value).WhereIf(viewedAtMax.HasValue, e => e.ArticleView.ViewedAt <= viewedAtMax!.Value).WhereIf(durationMin.HasValue, e => e.ArticleView.Duration >= durationMin!.Value).WhereIf(durationMax.HasValue, e => e.ArticleView.Duration <= durationMax!.Value).WhereIf(userId.HasValue, e => e.ArticleView.UserId == userId).WhereIf(articleId != null && articleId != Guid.Empty, e => e.Article != null && e.Article.Id == articleId);
    }

    public virtual async Task<List<ArticleView>> GetListAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, ipAddress, device, source, viewedAtMin, viewedAtMax, durationMin, durationMax, userId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? ArticleViewConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, ipAddress, device, source, viewedAtMin, viewedAtMax, durationMin, durationMax, userId, articleId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<ArticleView> ApplyFilter(IQueryable<ArticleView> query, string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.IpAddress!.Contains(filterText!) || e.Device!.Contains(filterText!) || e.Source!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(ipAddress), e => e.IpAddress.Contains(ipAddress)).WhereIf(!string.IsNullOrWhiteSpace(device), e => e.Device.Contains(device)).WhereIf(!string.IsNullOrWhiteSpace(source), e => e.Source.Contains(source)).WhereIf(viewedAtMin.HasValue, e => e.ViewedAt >= viewedAtMin!.Value).WhereIf(viewedAtMax.HasValue, e => e.ViewedAt <= viewedAtMax!.Value).WhereIf(durationMin.HasValue, e => e.Duration >= durationMin!.Value).WhereIf(durationMax.HasValue, e => e.Duration <= durationMax!.Value).WhereIf(userId.HasValue, e => e.UserId == userId);
    }
}