using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;

namespace KHHub.MasterDataService.Services.Articles;

public partial interface IArticlesAppService : IApplicationService
{
    Task<PagedResultDto<ArticleWithNavigationPropertiesDto>> GetListAsync(GetArticlesInput input);
    Task<ArticleWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<ArticleDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetArticleCategoryLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<ArticleDto> CreateAsync(ArticleCreateDto input);
    Task<ArticleDto> UpdateAsync(Guid id, ArticleUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> articleIds);
    Task DeleteAllAsync(GetArticlesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}