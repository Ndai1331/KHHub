using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;

namespace KHHub.MasterDataService.Services.EntityFiles;

public partial interface IEntityFilesAppService : IApplicationService
{
    Task<PagedResultDto<EntityFileWithNavigationPropertiesDto>> GetListAsync(GetEntityFilesInput input);
    Task<EntityFileWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<EntityFileDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetMediaFileLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<EntityFileDto> CreateAsync(EntityFileCreateDto input);
    Task<EntityFileDto> UpdateAsync(Guid id, EntityFileUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(EntityFileExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> entityfileIds);
    Task DeleteAllAsync(GetEntityFilesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}