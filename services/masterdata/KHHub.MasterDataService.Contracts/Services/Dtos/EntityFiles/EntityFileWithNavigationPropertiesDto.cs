using KHHub.MasterDataService.Services.Dtos.MediaFiles;
using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.EntityFiles;

public abstract class EntityFileWithNavigationPropertiesDtoBase
{
    public EntityFileDto EntityFile { get; set; } = null!;
    public MediaFileDto MediaFile { get; set; } = null!;
}