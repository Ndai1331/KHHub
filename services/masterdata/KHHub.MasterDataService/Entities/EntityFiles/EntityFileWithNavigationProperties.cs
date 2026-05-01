using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.EntityFiles;

namespace KHHub.MasterDataService.Entities.EntityFiles;

public abstract class EntityFileWithNavigationPropertiesBase
{
    public EntityFile EntityFile { get; set; } = null!;
    public MediaFile MediaFile { get; set; } = null!;
}