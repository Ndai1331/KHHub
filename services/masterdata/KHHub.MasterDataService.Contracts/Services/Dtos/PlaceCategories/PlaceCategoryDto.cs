using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.PlaceCategories;

public abstract class PlaceCategoryDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Description { get; set; }

    public string? Icon { get; set; }

    public string? Color { get; set; }

    public Guid? ParentId { get; set; }

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}