using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;

namespace KHHub.MasterDataService.Services.ArticleViews;

public partial interface IArticleViewsAppService : IApplicationService
{
    Task<PagedResultDto<ArticleViewWithNavigationPropertiesDto>> GetListAsync(GetArticleViewsInput input);
    Task<ArticleViewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<ArticleViewDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetArticleLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<ArticleViewDto> CreateAsync(ArticleViewCreateDto input);
    Task<ArticleViewDto> UpdateAsync(Guid id, ArticleViewUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleViewExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> articleviewIds);
    Task DeleteAllAsync(GetArticleViewsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}