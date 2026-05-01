using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Services.Dtos.Places;

namespace KHHub.MasterDataService.Services.Places;

public partial interface IPlacesAppService : IApplicationService
{
    Task<PagedResultDto<PlaceWithNavigationPropertiesDto>> GetListAsync(GetPlacesInput input);
    Task<PlaceWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<PlaceDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetPlaceCategoryLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetWardLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<PlaceDto> CreateAsync(PlaceCreateDto input);
    Task<PlaceDto> UpdateAsync(Guid id, PlaceUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placeIds);
    Task DeleteAllAsync(GetPlacesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}