using Volo.Abp.Application.Dtos;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public class GetMediaFilesExplorerInput : PagedAndSortedResultRequestDto
{
    /// <summary>
    /// Normalized logical parent folder path; null / empty means root folder.
    /// </summary>
    public string? ParentFolderPath { get; set; }

    public string? FilterText { get; set; }
}
