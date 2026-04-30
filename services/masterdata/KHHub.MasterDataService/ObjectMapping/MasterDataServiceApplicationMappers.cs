using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.MasterDataService.Entities.Wards;
using System;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.MasterDataService.Entities.Provinces;
using System.Linq;
using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace KHHub.MasterDataService.ObjectMapping;

/*
Write your mappings here...

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MasterDataServiceApplicationMappers : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}
*/
[Mapper]
public partial class ProvinceToProvinceDtoMappers : MapperBase<Province, ProvinceDto>
{
    public override partial ProvinceDto Map(Province source);
    public override partial void Map(Province source, ProvinceDto destination);
}

[Mapper]
public partial class ProvinceToProvinceExcelDtoMappers : MapperBase<Province, ProvinceExcelDto>
{
    public override partial ProvinceExcelDto Map(Province source);
    public override partial void Map(Province source, ProvinceExcelDto destination);
}

[Mapper]
public partial class WardToWardDtoMappers : MapperBase<Ward, WardDto>
{
    public override partial WardDto Map(Ward source);
    public override partial void Map(Ward source, WardDto destination);
}

[Mapper]
public partial class WardToWardExcelDtoMappers : MapperBase<Ward, WardExcelDto>
{
    public override partial WardExcelDto Map(Ward source);
    public override partial void Map(Ward source, WardExcelDto destination);
}

[Mapper]
public partial class WardWithNavigationPropertiesToWardWithNavigationPropertiesDtoMapper : MapperBase<WardWithNavigationProperties, WardWithNavigationPropertiesDto>
{
    public override partial WardWithNavigationPropertiesDto Map(WardWithNavigationProperties source);
    public override partial void Map(WardWithNavigationProperties source, WardWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class ProvinceToLookupDtoGuidMapper : MapperBase<Province, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(Province source);
    public override partial void Map(Province source, LookupDto<Guid> destination);

    public override void AfterMap(Province source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}