using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.JobCategories;
using KHHub.MasterDataService.Services.Dtos.JobCategories;

namespace KHHub.MasterDataService.Services.JobCategories;

public partial interface IJobCategoriesAppService : IApplicationService
{
    Task<PagedResultDto<JobCategoryDto>> GetListAsync(GetJobCategoriesInput input);
    Task<JobCategoryDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<JobCategoryDto> CreateAsync(JobCategoryCreateDto input);
    Task<JobCategoryDto> UpdateAsync(Guid id, JobCategoryUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(JobCategoryExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> jobcategoryIds);
    Task DeleteAllAsync(GetJobCategoriesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}