using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

namespace KHHub.MasterDataService.Services.ArticleTagMappings;

public partial interface IArticleTagMappingsAppService : IApplicationService
{
    Task<PagedResultDto<ArticleTagMappingWithNavigationPropertiesDto>> GetListAsync(GetArticleTagMappingsInput input);
    Task<ArticleTagMappingWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);
    Task<ArticleTagMappingDto> GetAsync(Guid id);
    Task<PagedResultDto<LookupDto<Guid>>> GetArticleTagLookupAsync(LookupRequestDto input);
    Task<PagedResultDto<LookupDto<Guid>>> GetArticleLookupAsync(LookupRequestDto input);
    Task DeleteAsync(Guid id);
    Task<ArticleTagMappingDto> CreateAsync(ArticleTagMappingCreateDto input);
    Task<ArticleTagMappingDto> UpdateAsync(Guid id, ArticleTagMappingUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleTagMappingExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> articletagmappingIds);
    Task DeleteAllAsync(GetArticleTagMappingsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}