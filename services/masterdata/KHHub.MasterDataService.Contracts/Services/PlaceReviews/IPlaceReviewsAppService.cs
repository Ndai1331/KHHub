using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;

namespace KHHub.MasterDataService.Services.PlaceReviews;

public partial interface IPlaceReviewsAppService : IApplicationService
{
    Task<PagedResultDto<PlaceReviewWithNavigationPropertiesDto>> GetListAsync(GetPlaceReviewsInput input);
    Task<PlaceReviewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<PlaceReviewDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetPlaceLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<PlaceReviewDto> CreateAsync(PlaceReviewCreateDto input);
    Task<PlaceReviewDto> UpdateAsync(Guid id, PlaceReviewUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceReviewExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placereviewIds);
    Task DeleteAllAsync(GetPlaceReviewsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}