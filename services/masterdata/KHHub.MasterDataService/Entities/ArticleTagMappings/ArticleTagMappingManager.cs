using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.ArticleTagMappings;

namespace KHHub.MasterDataService.Entities.ArticleTagMappings;

public abstract class ArticleTagMappingManagerBase : DomainService
{
    protected IArticleTagMappingRepository _articleTagMappingRepository;

    public ArticleTagMappingManagerBase(IArticleTagMappingRepository articleTagMappingRepository)
    {
        _articleTagMappingRepository = articleTagMappingRepository;
    }

    public virtual async Task<ArticleTagMapping> CreateAsync(Guid articleTagId, Guid articleId, bool isPrimary, int order)
    {
        Check.NotNull(articleTagId, nameof(articleTagId));
        Check.NotNull(articleId, nameof(articleId));
        var articleTagMapping = new ArticleTagMapping(GuidGenerator.Create(), articleTagId, articleId, isPrimary, order);
        return await _articleTagMappingRepository.InsertAsync(articleTagMapping);
    }

    public virtual async Task<ArticleTagMapping> UpdateAsync(Guid id, Guid articleTagId, Guid articleId, bool isPrimary, int order, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(articleTagId, nameof(articleTagId));
        Check.NotNull(articleId, nameof(articleId));
        var articleTagMapping = await _articleTagMappingRepository.GetAsync(id);
        articleTagMapping.ArticleTagId = articleTagId;
        articleTagMapping.ArticleId = articleId;
        articleTagMapping.IsPrimary = isPrimary;
        articleTagMapping.Order = order;
        articleTagMapping.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _articleTagMappingRepository.UpdateAsync(articleTagMapping);
    }
}