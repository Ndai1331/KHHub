using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Data.ArticleTagMappings;
using KHHub.MasterDataService.Entities.ArticleTagMappings;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Linq;

namespace KHHub.MasterDataService.Services.ArticleTagMappings;

public class ArticleTagMappingsAppService : ArticleTagMappingsAppServiceBase, IArticleTagMappingsAppService
{
    public ArticleTagMappingsAppService(
        IArticleTagMappingRepository articleTagMappingRepository,
        ArticleTagMappingManager articleTagMappingManager,
        IDistributedCache<ArticleTagMappingDownloadTokenCacheItem, string> downloadTokenCache,
        IRepository<ArticleTag, Guid> articleTagRepository,
        IRepository<Article, Guid> articleRepository)
        : base(articleTagMappingRepository, articleTagMappingManager, downloadTokenCache, articleTagRepository, articleRepository)
    {
    }

    /// <summary>
    /// Match Place tag lookup: show tag name (not slug), filter by name or slug,
    /// and collapse duplicate slugs in the suggestion list when the user is searching.
    /// </summary>
    public override async Task<PagedResultDto<LookupDto<Guid>>> GetArticleTagLookupAsync(LookupRequestDto input)
    {
        var query = await _articleTagRepository.GetQueryableAsync();
        var nf = string.IsNullOrWhiteSpace(input.Filter)
            ? null
            : input.Filter.Trim().ToLowerInvariant();

        var filtered = query.WhereIf(
            nf != null,
            x =>
                (x.Name != null && x.Name.ToLower().Contains(nf!))
                || (x.Slug != null && x.Slug.ToLower().Contains(nf!)));

        var ordered = filtered.OrderBy(x => x.Name).ThenBy(x => x.Slug);

        List<ArticleTag> pageRows;
        int totalCount;

        if (nf == null)
        {
            totalCount = await ordered.CountAsync();
            pageRows = await ordered.Skip(input.SkipCount).Take(input.MaxResultCount).ToListAsync();
        }
        else
        {
            const int cap = 400;
            var batch = await ordered.Take(cap).ToListAsync();
            var distinctBySlug = batch
                .GroupBy(t => (t.Slug ?? string.Empty).Trim().ToLowerInvariant())
                .Select(g => g.OrderBy(t => t.Name).First())
                .OrderBy(x => x.Name)
                .ToList();
            totalCount = distinctBySlug.Count;
            pageRows = distinctBySlug.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
        }

        var items = pageRows
            .Select(x => new LookupDto<Guid>
            {
                Id = x.Id,
                DisplayName = string.IsNullOrWhiteSpace(x.Name) ? x.Slug : x.Name,
            })
            .ToList();

        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = items,
        };
    }
}
