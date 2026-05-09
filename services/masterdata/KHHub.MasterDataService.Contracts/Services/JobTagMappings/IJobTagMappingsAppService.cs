using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;

namespace KHHub.MasterDataService.Services.JobTagMappings;

public partial interface IJobTagMappingsAppService : IApplicationService
{
    Task<PagedResultDto<JobTagMappingWithNavigationPropertiesDto>> GetListAsync(GetJobTagMappingsInput input);
    Task<JobTagMappingWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<JobTagMappingDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetJobTagLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetJobLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<JobTagMappingDto> CreateAsync(JobTagMappingCreateDto input);
    Task<JobTagMappingDto> UpdateAsync(Guid id, JobTagMappingUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobTagMappingExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobtagmappingIds);
    Task DeleteAllAsync(GetJobTagMappingsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}