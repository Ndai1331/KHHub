using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.HomeBanners;

namespace KHHub.MasterDataService.Data.HomeBanners;

public partial interface IHomeBannerRepository : IRepository<HomeBanner, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null, CancellationToken cancellationToken = default);
    Task<List<HomeBanner>> GetListAsync(string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? title = null, string? subtitle = null, string? description = null, string? imageUrl = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, int? sortOrderMin = null, int? sortOrderMax = null, bool? isActive = null, DateTime? startDateMin = null, DateTime? startDateMax = null, DateTime? endDateMin = null, DateTime? endDateMax = null, CancellationToken cancellationToken = default);
}