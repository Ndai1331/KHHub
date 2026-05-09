using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.JobFavorites;

namespace KHHub.MasterDataService.Services.JobFavorites;

public partial interface IJobFavoritesAppService : IApplicationService
{
    Task<PagedResultDto<JobFavoriteWithNavigationPropertiesDto>> GetListAsync(GetJobFavoritesInput input);
    Task<JobFavoriteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<JobFavoriteDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetJobLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<JobFavoriteDto> CreateAsync(JobFavoriteCreateDto input);
    Task<JobFavoriteDto> UpdateAsync(Guid id, JobFavoriteUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobFavoriteExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobfavoriteIds);
    Task DeleteAllAsync(GetJobFavoritesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}