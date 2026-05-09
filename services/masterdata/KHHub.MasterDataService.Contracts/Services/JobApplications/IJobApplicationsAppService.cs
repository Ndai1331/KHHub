using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.JobApplications;
using KHHub.MasterDataService.Services.Dtos.JobApplications;

namespace KHHub.MasterDataService.Services.JobApplications;

public partial interface IJobApplicationsAppService : IApplicationService
{
    Task<PagedResultDto<JobApplicationWithNavigationPropertiesDto>> GetListAsync(GetJobApplicationsInput input);
    Task<JobApplicationWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<JobApplicationDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetJobLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<JobApplicationDto> CreateAsync(JobApplicationCreateDto input);
    Task<JobApplicationDto> UpdateAsync(Guid id, JobApplicationUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobApplicationExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobapplicationIds);
    Task DeleteAllAsync(GetJobApplicationsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}