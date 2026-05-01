using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;

namespace KHHub.MasterDataService.Services.PlaceTags;

public partial interface IPlaceTagsAppService : IApplicationService
{
    Task<PagedResultDto<PlaceTagDto>> GetListAsync(GetPlaceTagsInput input);
    Task<PlaceTagDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<PlaceTagDto> CreateAsync(PlaceTagCreateDto input);
    Task<PlaceTagDto> UpdateAsync(Guid id, PlaceTagUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceTagExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> placetagIds);
    Task DeleteAllAsync(GetPlaceTagsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}