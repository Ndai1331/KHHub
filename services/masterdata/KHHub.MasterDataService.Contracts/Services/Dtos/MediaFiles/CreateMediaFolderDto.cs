using System.ComponentModel.DataAnnotations;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public class CreateMediaFolderDto
{
    [Required]
    [StringLength(256, MinimumLength = 1)]
    public string Name { get; set; } = "";

    /// <summary>Logical folder path where the new folder is created.</summary>
    public string? ParentFolderPath { get; set; }
}
