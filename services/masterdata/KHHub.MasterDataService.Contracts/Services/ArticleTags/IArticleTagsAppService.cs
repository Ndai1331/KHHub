using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;

namespace KHHub.MasterDataService.Services.ArticleTags;

public partial interface IArticleTagsAppService : IApplicationService
{
    Task<PagedResultDto<ArticleTagDto>> GetListAsync(GetArticleTagsInput input);
    Task<ArticleTagDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<ArticleTagDto> CreateAsync(ArticleTagCreateDto input);
    Task<ArticleTagDto> UpdateAsync(Guid id, ArticleTagUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleTagExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> articletagIds);
    Task DeleteAllAsync(GetArticleTagsInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}