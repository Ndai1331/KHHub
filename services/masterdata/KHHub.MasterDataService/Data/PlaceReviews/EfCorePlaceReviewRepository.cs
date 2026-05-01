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
using KHHub.MasterDataService.Entities.PlaceReviews;

namespace KHHub.MasterDataService.Data.PlaceReviews;

public abstract class EfCorePlaceReviewRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, PlaceReview, Guid>
{
    public EfCorePlaceReviewRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, ratingMin, ratingMax, title, comment, likeCountMin, likeCountMax, status, userId, placeId);
        var ids = query.Select(x => x.PlaceReview.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<PlaceReviewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var dbContext = await GetDbContextAsync();
        return (await GetDbSetAsync()).Where(b => b.Id == id).Select(placeReview => new PlaceReviewWithNavigationProperties { PlaceReview = placeReview, Place = dbContext.Set<Place>().FirstOrDefault(c => c.Id == placeReview.PlaceId) }).FirstOrDefault();
    }

    public virtual async Task<List<PlaceReviewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, ratingMin, ratingMax, title, comment, likeCountMin, likeCountMax, status, userId, placeId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceReviewConsts.GetDefaultSorting(true) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    protected virtual async Task<IQueryable<PlaceReviewWithNavigationProperties>> GetQueryForNavigationPropertiesAsync()
    {
        return from placeReview in (await GetDbSetAsync())
               join place in (await GetDbContextAsync()).Set<Place>() on placeReview.PlaceId equals place.Id into places
               from place in places.DefaultIfEmpty()
               select new PlaceReviewWithNavigationProperties
               {
                   PlaceReview = placeReview,
                   Place = place
               };
    }

    protected virtual IQueryable<PlaceReviewWithNavigationProperties> ApplyFilter(IQueryable<PlaceReviewWithNavigationProperties> query, string? filterText, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.PlaceReview.Title!.Contains(filterText!) || e.PlaceReview.Comment!.Contains(filterText!)).WhereIf(ratingMin.HasValue, e => e.PlaceReview.Rating >= ratingMin!.Value).WhereIf(ratingMax.HasValue, e => e.PlaceReview.Rating <= ratingMax!.Value).WhereIf(!string.IsNullOrWhiteSpace(title), e => e.PlaceReview.Title.Contains(title)).WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.PlaceReview.Comment.Contains(comment)).WhereIf(likeCountMin.HasValue, e => e.PlaceReview.LikeCount >= likeCountMin!.Value).WhereIf(likeCountMax.HasValue, e => e.PlaceReview.LikeCount <= likeCountMax!.Value).WhereIf(status.HasValue, e => e.PlaceReview.Status == status).WhereIf(userId.HasValue, e => e.PlaceReview.UserId == userId).WhereIf(placeId != null && placeId != Guid.Empty, e => e.Place != null && e.Place.Id == placeId);
    }

    public virtual async Task<List<PlaceReview>> GetListAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, ratingMin, ratingMax, title, comment, likeCountMin, likeCountMax, status, userId);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? PlaceReviewConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryForNavigationPropertiesAsync();
        query = ApplyFilter(query, filterText, ratingMin, ratingMax, title, comment, likeCountMin, likeCountMax, status, userId, placeId);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<PlaceReview> ApplyFilter(IQueryable<PlaceReview> query, string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Title!.Contains(filterText!) || e.Comment!.Contains(filterText!)).WhereIf(ratingMin.HasValue, e => e.Rating >= ratingMin!.Value).WhereIf(ratingMax.HasValue, e => e.Rating <= ratingMax!.Value).WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title)).WhereIf(!string.IsNullOrWhiteSpace(comment), e => e.Comment.Contains(comment)).WhereIf(likeCountMin.HasValue, e => e.LikeCount >= likeCountMin!.Value).WhereIf(likeCountMax.HasValue, e => e.LikeCount <= likeCountMax!.Value).WhereIf(status.HasValue, e => e.Status == status).WhereIf(userId.HasValue, e => e.UserId == userId);
    }
}