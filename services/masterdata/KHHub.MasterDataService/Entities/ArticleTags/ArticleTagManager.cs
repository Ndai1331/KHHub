using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.ArticleTags;

namespace KHHub.MasterDataService.Entities.ArticleTags;

public abstract class ArticleTagManagerBase : DomainService
{
    protected IArticleTagRepository _articleTagRepository;

    public ArticleTagManagerBase(IArticleTagRepository articleTagRepository)
    {
        _articleTagRepository = articleTagRepository;
    }

    public virtual async Task<ArticleTag> CreateAsync(string name, string slug, int usageCount, string? description = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ArticleTagConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleTagConsts.SlugMaxLength);
        var articleTag = new ArticleTag(GuidGenerator.Create(), name, slug, usageCount, description);
        return await _articleTagRepository.InsertAsync(articleTag);
    }

    public virtual async Task<ArticleTag> UpdateAsync(Guid id, string name, string slug, int usageCount, string? description = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ArticleTagConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleTagConsts.SlugMaxLength);
        var articleTag = await _articleTagRepository.GetAsync(id);
        articleTag.Name = name;
        articleTag.Slug = slug;
        articleTag.UsageCount = usageCount;
        articleTag.Description = description;
        articleTag.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _articleTagRepository.UpdateAsync(articleTag);
    }
}