using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.Web.Pages.Wards;
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

[Mapper]
public partial class WardDtoToWardUpdateViewModelMapper : MapperBase<WardDto, WardUpdateViewModel>
{
    public override partial WardUpdateViewModel Map(WardDto source);
    public override partial void Map(WardDto source, WardUpdateViewModel destination);
}

[Mapper]
public partial class WardUpdateViewModelToWardUpdateDto : MapperBase<WardUpdateViewModel, WardUpdateDto>
{
    public override partial WardUpdateDto Map(WardUpdateViewModel source);
    public override partial void Map(WardUpdateViewModel source, WardUpdateDto destination);
}

[Mapper]
public partial class WardCreateViewModelToWardCreateDto : MapperBase<WardCreateViewModel, WardCreateDto>
{
    public override partial WardCreateDto Map(WardCreateViewModel source);
    public override partial void Map(WardCreateViewModel source, WardCreateDto destination);
}