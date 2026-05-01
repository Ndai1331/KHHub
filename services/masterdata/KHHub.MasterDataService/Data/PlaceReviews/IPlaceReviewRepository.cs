using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.PlaceReviews;

namespace KHHub.MasterDataService.Data.PlaceReviews;

public partial interface IPlaceReviewRepository : IRepository<PlaceReview, Guid>
{
    Task DeleteAllAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default);
    Task<PlaceReviewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<PlaceReviewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<PlaceReview>> GetListAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, int? ratingMin = null, int? ratingMax = null, string? title = null, string? comment = null, int? likeCountMin = null, int? likeCountMax = null, PlaceReviewStatus? status = null, Guid? userId = null, Guid? placeId = null, CancellationToken cancellationToken = default);
}