using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

namespace KHHub.MasterDataService.Services.PlaceFavorites;

public partial interface IPlaceFavoritesAppService : IApplicationService
{
    Task<PagedResultDto<PlaceFavoriteWithNavigationPropertiesDto>> GetListAsync(GetPlaceFavoritesInput input);
    Task<PlaceFavoriteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<PlaceFavoriteDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetPlaceLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<PlaceFavoriteDto> CreateAsync(PlaceFavoriteCreateDto input);
    Task<PlaceFavoriteDto> UpdateAsync(Guid id, PlaceFavoriteUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceFavoriteExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placefavoriteIds);
    Task DeleteAllAsync(GetPlaceFavoritesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}