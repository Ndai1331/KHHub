using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Services.Dtos.Wards;

namespace KHHub.MasterDataService.Services.Wards;

public partial interface IWardsAppService : IApplicationService
{
    Task<PagedResultDto<WardWithNavigationPropertiesDto>> GetListAsync(GetWardsInput input);
    Task<WardWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<WardDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<WardDto> CreateAsync(WardCreateDto input);
    Task<WardDto> UpdateAsync(Guid id, WardUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(WardExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> wardIds);
    Task DeleteAllAsync(GetWardsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}