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
using KHHub.MasterDataService.Entities.HomeBanners;

namespace KHHub.MasterDataService.Data.HomeBanners;

public abstract class EfCoreHomeBannerRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, HomeBanner, Guid>
{
    public EfCoreHomeBannerRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, title, subtitle, description, imageUrl, mobileImageUrl, buttonText, buttonUrl, targetType, targetId, sortOrderMin, sortOrderMax, isActive, startDateMin, startDateMax, endDateMin, endDateMax);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<HomeBanner>> GetListAsync(string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, title, subtitle, description, imageUrl, mobileImageUrl, buttonText, buttonUrl, targetType, targetId, sortOrderMin, sortOrderMax, isActive, startDateMin, startDateMax, endDateMin, endDateMax);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? HomeBannerConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, title, subtitle, description, imageUrl, mobileImageUrl, buttonText, buttonUrl, targetType, targetId, sortOrderMin, sortOrderMax, isActive, startDateMin, startDateMax, endDateMin, endDateMax);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<HomeBanner> ApplyFilter(IQueryable<HomeBanner> query, string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.Title!.Contains(filterText!) || e.Subtitle!.Contains(filterText!) || e.Description!.Contains(filterText!) || e.ImageUrl!.Contains(filterText!) || e.MobileImageUrl!.Contains(filterText!) || e.ButtonText!.Contains(filterText!) || e.ButtonUrl!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(title), e => e.Title.Contains(title)).WhereIf(!string.IsNullOrWhiteSpace(subtitle), e => e.Subtitle.Contains(subtitle)).WhereIf(!string.IsNullOrWhiteSpace(description), e => e.Description.Contains(description)).WhereIf(!string.IsNullOrWhiteSpace(imageUrl), e => e.ImageUrl.Contains(imageUrl)).WhereIf(!string.IsNullOrWhiteSpace(mobileImageUrl), e => e.MobileImageUrl.Contains(mobileImageUrl)).WhereIf(!string.IsNullOrWhiteSpace(buttonText), e => e.ButtonText.Contains(buttonText)).WhereIf(!string.IsNullOrWhiteSpace(buttonUrl), e => e.ButtonUrl.Contains(buttonUrl)).WhereIf(targetType.HasValue, e => e.TargetType == targetType).WhereIf(targetId.HasValue, e => e.TargetId == targetId).WhereIf(sortOrderMin.HasValue, e => e.SortOrder >= sortOrderMin!.Value).WhereIf(sortOrderMax.HasValue, e => e.SortOrder <= sortOrderMax!.Value).WhereIf(isActive.HasValue, e => e.IsActive == isActive).WhereIf(startDateMin.HasValue, e => e.StartDate >= startDateMin!.Value).WhereIf(startDateMax.HasValue, e => e.StartDate <= startDateMax!.Value).WhereIf(endDateMin.HasValue, e => e.EndDate >= endDateMin!.Value).WhereIf(endDateMax.HasValue, e => e.EndDate <= endDateMax!.Value);
    }
}