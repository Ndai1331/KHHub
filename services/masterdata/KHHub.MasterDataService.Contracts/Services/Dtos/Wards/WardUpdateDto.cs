using KHHub.MasterDataService.Entities.Wards;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Wards;

public abstract class WardUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(WardConsts.CodeMaxLength)]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(WardConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    public Guid ProvinceId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}