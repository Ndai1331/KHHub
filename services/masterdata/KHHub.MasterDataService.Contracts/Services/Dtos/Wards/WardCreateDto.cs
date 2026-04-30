using KHHub.MasterDataService.Entities.Wards;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.Wards;

public abstract class WardCreateDtoBase
{
    [Required]
    [StringLength(WardConsts.CodeMaxLength)]
    public string Code { get; set; } = null!;
    [Required]
    [StringLength(WardConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    public Guid ProvinceId { get; set; }
}