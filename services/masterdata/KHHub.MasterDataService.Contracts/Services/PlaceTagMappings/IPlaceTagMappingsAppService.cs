using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

namespace KHHub.MasterDataService.Services.PlaceTagMappings;

public partial interface IPlaceTagMappingsAppService : IApplicationService
{
    Task<PagedResultDto<PlaceTagMappingWithNavigationPropertiesDto>> GetListAsync(GetPlaceTagMappingsInput input);
    Task<PlaceTagMappingWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<PlaceTagMappingDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetPlaceTagLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetPlaceLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<PlaceTagMappingDto> CreateAsync(PlaceTagMappingCreateDto input);
    Task<PlaceTagMappingDto> UpdateAsync(Guid id, PlaceTagMappingUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceTagMappingExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placetagmappingIds);
    Task DeleteAllAsync(GetPlaceTagMappingsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}