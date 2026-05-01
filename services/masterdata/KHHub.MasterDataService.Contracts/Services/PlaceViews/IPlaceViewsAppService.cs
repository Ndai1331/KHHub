using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceViews;

namespace KHHub.MasterDataService.Services.PlaceViews;

public partial interface IPlaceViewsAppService : IApplicationService
{
    Task<PagedResultDto<PlaceViewWithNavigationPropertiesDto>> GetListAsync(GetPlaceViewsInput input);
    Task<PlaceViewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<PlaceViewDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetPlaceLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<PlaceViewDto> CreateAsync(PlaceViewCreateDto input);
    Task<PlaceViewDto> UpdateAsync(Guid id, PlaceViewUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceViewExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placeviewIds);
    Task DeleteAllAsync(GetPlaceViewsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}