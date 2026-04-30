using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.Web.Pages.Provinces;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace KHHub.Web;

/*
Write your mappings here...

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class KHHubWebMappers : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}
*/
[Mapper]
public partial class ProvinceDtoToProvinceUpdateViewModelMapper : MapperBase<ProvinceDto, ProvinceUpdateViewModel>
{
    public override partial ProvinceUpdateViewModel Map(ProvinceDto source);
    public override partial void Map(ProvinceDto source, ProvinceUpdateViewModel destination);
}

[Mapper]
public partial class ProvinceUpdateViewModelToProvinceUpdateDto : MapperBase<ProvinceUpdateViewModel, ProvinceUpdateDto>
{
    public override partial ProvinceUpdateDto Map(ProvinceUpdateViewModel source);
    public override partial void Map(ProvinceUpdateViewModel source, ProvinceUpdateDto destination);
}

[Mapper]
public partial class ProvinceCreateViewModelToProvinceCreateDto : MapperBase<ProvinceCreateViewModel, ProvinceCreateDto>
{
    public override partial ProvinceCreateDto Map(ProvinceCreateViewModel source);
    public override partial void Map(ProvinceCreateViewModel source, ProvinceCreateDto destination);
}