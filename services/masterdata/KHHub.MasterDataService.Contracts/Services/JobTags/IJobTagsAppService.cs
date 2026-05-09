using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.JobTags;
using KHHub.MasterDataService.Services.Dtos.JobTags;

namespace KHHub.MasterDataService.Services.JobTags;

public partial interface IJobTagsAppService : IApplicationService
{
    Task<PagedResultDto<JobTagDto>> GetListAsync(GetJobTagsInput input);
    Task<JobTagDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<JobTagDto> CreateAsync(JobTagCreateDto input);
    Task<JobTagDto> UpdateAsync(Guid id, JobTagUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobTagExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobtagIds);
    Task DeleteAllAsync(GetJobTagsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}