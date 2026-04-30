using KHHub.MasterDataService.Entities.Provinces;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Provinces;

public abstract class ProvinceUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(ProvinceConsts.CodeMaxLength)]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(ProvinceConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    public string ConcurrencyStamp { get; set; } = null!;
}