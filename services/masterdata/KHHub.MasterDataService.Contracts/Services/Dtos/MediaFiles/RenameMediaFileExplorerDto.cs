using System.ComponentModel.DataAnnotations;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public class RenameMediaFileExplorerDto
{
    [Required]
    [StringLength(512, MinimumLength = 1)]
    public string DisplayName { get; set; } = "";
}
