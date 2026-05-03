using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Content;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;

namespace KHHub.MasterDataService.Services.MediaFiles;

public partial interface IMediaFilesAppService
{
    Task<PagedResultDto<MediaFileDto>> GetExplorerListAsync(GetMediaFilesExplorerInput input);

    Task<MediaFileDto> CreateFolderAsync(CreateMediaFolderDto input);

    Task<MediaFileDto> UploadExplorerFileAsync(string? parentFolderPath, IRemoteStreamContent content);

    Task<MediaFileDto> RenameExplorerItemAsync(Guid id, RenameMediaFileExplorerDto input);

    Task DeleteExplorerEntryAsync(Guid id);
}
