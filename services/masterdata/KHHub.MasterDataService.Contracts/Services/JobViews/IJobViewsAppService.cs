using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.JobViews;
using KHHub.MasterDataService.Services.Dtos.JobViews;

namespace KHHub.MasterDataService.Services.JobViews;

public partial interface IJobViewsAppService : IApplicationService
{
    Task<PagedResultDto<JobViewWithNavigationPropertiesDto>> GetListAsync(GetJobViewsInput input);
    Task<JobViewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<JobViewDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetJobLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<JobViewDto> CreateAsync(JobViewCreateDto input);
    Task<JobViewDto> UpdateAsync(Guid id, JobViewUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobViewExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobviewIds);
    Task DeleteAllAsync(GetJobViewsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}