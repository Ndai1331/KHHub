using System;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Data.ArticleTags;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace KHHub.MasterDataService.Services.ArticleTags;

public class ArticleTagsAppService : ArticleTagsAppServiceBase, IArticleTagsAppService
{
    public ArticleTagsAppService(
        IArticleTagRepository articleTagRepository,
        ArticleTagManager articleTagManager,
        IDistributedCache<ArticleTagDownloadTokenCacheItem, string> downloadTokenCache)
        : base(articleTagRepository, articleTagManager, downloadTokenCache)
    {
    }

    public override async Task<ArticleTagDto> CreateAsync(ArticleTagCreateDto input)
    {
        await EnsureArticleTagSlugUniqueAsync(input.Slug, null);
        return await base.CreateAsync(input);
    }

    public override async Task<ArticleTagDto> UpdateAsync(Guid id, ArticleTagUpdateDto input)
    {
        await EnsureArticleTagSlugUniqueAsync(input.Slug, id);
        return await base.UpdateAsync(id, input);
    }

    private async Task EnsureArticleTagSlugUniqueAsync(string slug, Guid? excludeId)
    {
        var normalized = (slug ?? string.Empty).Trim().ToLowerInvariant();
        if (normalized.Length == 0)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Slug"]]);
        }

        var query = await _articleTagRepository.GetQueryableAsync();
        var exists = await query.AnyAsync(x =>
            x.Slug.ToLower() == normalized && (!excludeId.HasValue || x.Id != excludeId.Value));

        if (exists)
        {
            throw new UserFriendlyException(L["ArticleTagSlugAlreadyExists", slug]);
        }
    }
}
