using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

public abstract class PlaceTagMappingCreateDtoBase
{
    public bool IsPrimary { get; set; } = false;
    public int SortOrder { get; set; }

    public Guid PlaceTagId { get; set; }

    public Guid PlaceId { get; set; }
}