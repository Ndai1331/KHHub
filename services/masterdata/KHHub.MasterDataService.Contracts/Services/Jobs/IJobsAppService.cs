using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Services.Dtos.Jobs;

namespace KHHub.MasterDataService.Services.Jobs;

public partial interface IJobsAppService : IApplicationService
{
    Task<PagedResultDto<JobWithNavigationPropertiesDto>> GetListAsync(GetJobsInput input);
    Task<JobWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<JobDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetWardLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetJobCategoryLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<JobDto> CreateAsync(JobCreateDto input);
    Task<JobDto> UpdateAsync(Guid id, JobUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobIds);
    Task DeleteAllAsync(GetJobsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}