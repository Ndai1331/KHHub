using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;

namespace KHHub.MasterDataService.Services.MediaFiles;

public partial interface IMediaFilesAppService : IApplicationService
{
    Task<PagedResultDto<MediaFileDto>> GetListAsync(GetMediaFilesInput input);
    Task<MediaFileDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<MediaFileDto> CreateAsync(MediaFileCreateDto input);
    Task<MediaFileDto> UpdateAsync(Guid id, MediaFileUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(MediaFileExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> mediafileIds);
    Task DeleteAllAsync(GetMediaFilesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}