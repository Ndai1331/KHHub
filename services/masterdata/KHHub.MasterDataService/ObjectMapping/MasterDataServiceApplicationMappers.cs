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