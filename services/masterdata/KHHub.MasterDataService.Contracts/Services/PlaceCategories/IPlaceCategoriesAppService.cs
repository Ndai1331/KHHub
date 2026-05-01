using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;

namespace KHHub.MasterDataService.Services.PlaceCategories;

public partial interface IPlaceCategoriesAppService : IApplicationService
{
    Task<PagedResultDto<PlaceCategoryDto>> GetListAsync(GetPlaceCategoriesInput input);
    Task<PlaceCategoryDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<PlaceCategoryDto> CreateAsync(PlaceCategoryCreateDto input);
    Task<PlaceCategoryDto> UpdateAsync(Guid id, PlaceCategoryUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceCategoryExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placecategoryIds);
    Task DeleteAllAsync(GetPlaceCategoriesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}