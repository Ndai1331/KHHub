using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Services.Dtos.Provinces;

namespace KHHub.MasterDataService.Services.Provinces;

public partial interface IProvincesAppService : IApplicationService
{
    Task<PagedResultDto<ProvinceDto>> GetListAsync(GetProvincesInput input);
    Task<ProvinceDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<ProvinceDto> CreateAsync(ProvinceCreateDto input);
    Task<ProvinceDto> UpdateAsync(Guid id, ProvinceUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ProvinceExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> provinceIds);
    Task DeleteAllAsync(GetProvincesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}