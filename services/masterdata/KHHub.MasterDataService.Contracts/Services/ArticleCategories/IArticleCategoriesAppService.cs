using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;
using KHHub.MasterDataService.Entities.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;

namespace KHHub.MasterDataService.Services.ArticleCategories;

public partial interface IArticleCategoriesAppService : IApplicationService
{
    Task<PagedResultDto<ArticleCategoryDto>> GetListAsync(GetArticleCategoriesInput input);
    Task<ArticleCategoryDto> GetAsync(Guid id);
    Task DeleteAsync(Guid id);
    Task<ArticleCategoryDto> CreateAsync(ArticleCategoryCreateDto input);
    Task<ArticleCategoryDto> UpdateAsync(Guid id, ArticleCategoryUpdateDto input);
    Task<IRemoteStreamContent> GetListAsExcelFileAsync(ArticleCategoryExcelDownloadDto input);
    Task DeleteByIdsAsync(List<Guid> articlecategoryIds);
    Task DeleteAllAsync(GetArticleCategoriesInput input);
    Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync();
}