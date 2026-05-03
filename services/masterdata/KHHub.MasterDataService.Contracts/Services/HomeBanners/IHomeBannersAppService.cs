using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;

namespace KHHub.MasterDataService.Services.HomeBanners;

public partial interface IHomeBannersAppService : IApplicationService
{
    Task<PagedResultDto<HomeBannerDto>> GetListAsync(GetHomeBannersInput input);
    Task<HomeBannerDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<HomeBannerDto> CreateAsync(HomeBannerCreateDto input);
    Task<HomeBannerDto> UpdateAsync(Guid id, HomeBannerUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(HomeBannerExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> homebannerIds);
    Task DeleteAllAsync(GetHomeBannersInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}