using System;

namespace KHHub.MasterDataService.Services.Dtos.Shared;

public class LookupRequestDto : LookupRequestDtoBase
{
    public Guid? ProvinceId { get; set; }
}