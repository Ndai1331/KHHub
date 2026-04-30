using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.ArticleCategories;

namespace KHHub.MasterDataService.Entities.ArticleCategories;

public abstract class ArticleCategoryManagerBase : DomainService
{
    protected IArticleCategoryRepository _articleCategoryRepository;

    public ArticleCategoryManagerBase(IArticleCategoryRepository articleCategoryRepository)
    {
        _articleCategoryRepository = articleCategoryRepository;
    }

    public virtual async Task<ArticleCategory> CreateAsync(string name, string slug, Guid parentId, int displayOrder, bool isActive, string? description = null, string? icon = null, string? thumbnailUrl = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ArticleCategoryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleCategoryConsts.SlugMaxLength);
        Check.Length(icon, nameof(icon), ArticleCategoryConsts.IconMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), ArticleCategoryConsts.ThumbnailUrlMaxLength);
        var articleCategory = new ArticleCategory(GuidGenerator.Create(), name, slug, parentId, displayOrder, isActive, description, icon, thumbnailUrl);
        return await _articleCategoryRepository.InsertAsync(articleCategory);
    }

    public virtual async Task<ArticleCategory> UpdateAsync(Guid id, string name, string slug, Guid parentId, int displayOrder, bool isActive, string? description = null, string? icon = null, string? thumbnailUrl = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ArticleCategoryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleCategoryConsts.SlugMaxLength);
        Check.Length(icon, nameof(icon), ArticleCategoryConsts.IconMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), ArticleCategoryConsts.ThumbnailUrlMaxLength);
        var articleCategory = await _articleCategoryRepository.GetAsync(id);
        articleCategory.Name = name;
        articleCategory.Slug = slug;
        articleCategory.ParentId = parentId;
        articleCategory.DisplayOrder = displayOrder;
        articleCategory.IsActive = isActive;
        articleCategory.Description = description;
        articleCategory.Icon = icon;
        articleCategory.ThumbnailUrl = thumbnailUrl;
        articleCategory.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _articleCategoryRepository.UpdateAsync(articleCategory);
    }
}