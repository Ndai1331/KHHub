using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KHHub.MasterDataService.Data.ArticleCategories;
using KHHub.MasterDataService.Data.ArticleTagMappings;
using KHHub.MasterDataService.Data.ArticleTags;
using KHHub.MasterDataService.Data.Articles;
using KHHub.MasterDataService.Entities.ArticleCategories;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Entities.ArticleTagMappings;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleCrawler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace KHHub.MasterDataService.IntegrationServices;

public class ArticleCrawlerIntegrationService : ApplicationService, IArticleCrawlerIntegrationService
{
    private readonly IArticleRepository _articleRepository;
    private readonly ArticleManager _articleManager;
    private readonly IArticleTagRepository _articleTagRepository;
    private readonly ArticleTagManager _articleTagManager;
    private readonly IArticleTagMappingRepository _articleTagMappingRepository;
    private readonly ArticleTagMappingManager _articleTagMappingManager;
    private readonly IArticleCategoryRepository _articleCategoryRepository;
    private readonly ArticleCategoryManager _articleCategoryManager;

    private static readonly Regex NonSlugChars = new(@"[^a-z0-9\-]+", RegexOptions.Compiled);

    public ArticleCrawlerIntegrationService(
        IArticleRepository articleRepository,
        ArticleManager articleManager,
        IArticleTagRepository articleTagRepository,
        ArticleTagManager articleTagManager,
        IArticleTagMappingRepository articleTagMappingRepository,
        ArticleTagMappingManager articleTagMappingManager,
        IArticleCategoryRepository articleCategoryRepository,
        ArticleCategoryManager articleCategoryManager)
    {
        _articleRepository = articleRepository;
        _articleManager = articleManager;
        _articleTagRepository = articleTagRepository;
        _articleTagManager = articleTagManager;
        _articleTagMappingRepository = articleTagMappingRepository;
        _articleTagMappingManager = articleTagMappingManager;
        _articleCategoryRepository = articleCategoryRepository;
        _articleCategoryManager = articleCategoryManager;
    }

    [AllowAnonymous]
    public virtual async Task<ArticleCrawlerUpsertResultDto> UpsertFromCrawlerAsync(ArticleCrawlerUpsertInputDto input)
    {
        var existing = await _articleRepository.FirstOrDefaultAsync(a => a.SourceUrl == input.SourceUrl);

        var categoryId = await ResolveArticleCategoryIdAsync(
            input.PrimaryArticleCategoryName,
            input.ArticleCategoryNameCandidates,
            input.ArticleCategoryId);

        var slug = BuildSlug(input.Title, input.SourceUrl);
        var seoTitle = input.SeoTitle.Length > ArticleConsts.SeoTitleMaxLength
            ? input.SeoTitle[..ArticleConsts.SeoTitleMaxLength]
            : input.SeoTitle;

        var publishedAt = input.PublishedAt ?? Clock.Now;

        Article article;
        var wasCreated = false;

        if (existing == null)
        {
            wasCreated = true;
            article = await _articleManager.CreateAsync(
                categoryId,
                input.Title,
                slug,
                input.Summary,
                input.Content,
                input.Type,
                input.AuthorName,
                input.Status,
                publishedAt,
                input.IsFeatured,
                input.IsHot,
                input.IsTrending,
                viewCount: 0,
                likeCount: 0,
                shareCount: 0,
                commentCount: 0,
                readingTime: input.ReadingTime,
                seoTitle,
                input.ThumbnailUrl,
                input.CoverImageUrl,
                input.Source,
                input.SourceUrl,
                input.SeoDescription ?? input.Summary,
                input.SeoKeywords);
        }
        else
        {
            article = await _articleManager.UpdateAsync(
                existing.Id,
                categoryId,
                input.Title,
                slug,
                input.Summary,
                input.Content,
                input.Type,
                input.AuthorName,
                input.Status,
                publishedAt,
                input.IsFeatured,
                input.IsHot,
                input.IsTrending,
                existing.ViewCount,
                existing.LikeCount,
                existing.ShareCount,
                existing.CommentCount,
                input.ReadingTime > 0 ? input.ReadingTime : existing.ReadingTime,
                seoTitle,
                input.ThumbnailUrl ?? existing.ThumbnailUrl,
                input.CoverImageUrl ?? existing.CoverImageUrl,
                input.Source ?? existing.Source,
                input.SourceUrl,
                input.SeoDescription ?? input.Summary ?? existing.SeoDescription,
                input.SeoKeywords ?? existing.SeoKeywords,
                existing.ConcurrencyStamp);
        }

        await ReplaceArticleTagsAsync(article.Id, input.TagNames);

        return new ArticleCrawlerUpsertResultDto { ArticleId = article.Id, WasCreated = wasCreated };
    }

    private async Task<Guid> ResolveArticleCategoryIdAsync(string? primaryName, List<string>? candidates, Guid fallbackId)
    {
        var ordered = new List<string>();
        if (!string.IsNullOrWhiteSpace(primaryName))
        {
            ordered.Add(primaryName.Trim());
        }

        if (candidates != null)
        {
            foreach (var c in candidates.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                ordered.Add(c.Trim());
            }
        }

        foreach (var name in ordered.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var id = await TryResolveOrCreateArticleCategoryIdAsync(name);
            if (id != null)
            {
                return id.Value;
            }
        }

        return fallbackId;
    }

    private async Task<Guid?> TryResolveOrCreateArticleCategoryIdAsync(string name)
    {
        var trimmed = name.Trim();
        if (trimmed.Length == 0)
        {
            return null;
        }

        var queryable = await _articleCategoryRepository.GetQueryableAsync();
        var categories = await AsyncExecuter.ToListAsync(queryable.Where(c => c.IsActive));
        var key = ComparableAsciiKey(trimmed);
        var existing = categories.FirstOrDefault(c => ComparableAsciiKey(c.Name) == key);
        if (existing != null)
        {
            return existing.Id;
        }

        var slug = BuildArticleCategorySlug(trimmed);
        var existingBySlug = categories.FirstOrDefault(c =>
            string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));
        if (existingBySlug != null)
        {
            return existingBySlug.Id;
        }

        var nextOrder = categories.Count == 0 ? 1 : categories.Max(c => c.DisplayOrder) + 1;
        var created = await _articleCategoryManager.CreateAsync(trimmed, slug, Guid.Empty, nextOrder, true);
        return created.Id;
    }

    private static string BuildArticleCategorySlug(string name)
    {
        var ascii = SlugifyAsciiSegment(name);
        if (string.IsNullOrEmpty(ascii))
        {
            ascii = "article-category";
        }

        return ascii.Length > ArticleCategoryConsts.SlugMaxLength
            ? ascii[..ArticleCategoryConsts.SlugMaxLength]
            : ascii;
    }

    private async Task ReplaceArticleTagsAsync(Guid articleId, List<string> tagNames)
    {
        var queryable = await _articleTagMappingRepository.GetQueryableAsync();
        var mappingIds = queryable.Where(m => m.ArticleId == articleId).Select(m => m.Id).ToList();
        await _articleTagMappingRepository.DeleteManyAsync(mappingIds);

        var order = 0;
        foreach (var rawName in tagNames.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            order++;
            var name = rawName.Trim();
            var tagSlug = BuildTagSlug(name);
            var tag = await _articleTagRepository.FirstOrDefaultAsync(t => t.Slug == tagSlug);
            if (tag == null)
            {
                tag = await _articleTagManager.CreateAsync(name, tagSlug, usageCount: 0);
            }

            await _articleTagMappingManager.CreateAsync(tag.Id, articleId, isPrimary: order == 1, order: order);
        }
    }

    private static string BuildSlug(string title, string sourceUrl)
    {
        var ascii = SlugifyAsciiSegment(title);
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(sourceUrl))).ToLowerInvariant();
        var combined = $"{ascii}-{hash[..16]}".Trim('-');
        if (combined.Length > ArticleConsts.SlugMaxLength)
        {
            combined = combined[..ArticleConsts.SlugMaxLength];
        }

        return combined;
    }

    private static string BuildTagSlug(string name)
    {
        var ascii = SlugifyAsciiSegment(name);
        if (string.IsNullOrEmpty(ascii))
        {
            ascii = "tag";
        }

        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(name))).ToLowerInvariant();
        var combined = $"{ascii}-{hash[..8]}";
        if (combined.Length > ArticleTagConsts.SlugMaxLength)
        {
            combined = combined[..ArticleTagConsts.SlugMaxLength];
        }

        return combined;
    }

    private static string ComparableAsciiKey(string text)
    {
        return SlugifyAsciiSegment(text).Replace("-", "", StringComparison.Ordinal);
    }

    private static string SlugifyAsciiSegment(string text)
    {
        var lowered = text.Trim().ToLowerInvariant();
        var sb = new StringBuilder(lowered.Length);
        foreach (var c in lowered)
        {
            sb.Append(c switch
            {
                'à' or 'á' or 'ạ' or 'ả' or 'ã' or 'â' or 'ầ' or 'ấ' or 'ậ' or 'ẩ' or 'ẫ' or 'ă' or 'ằ' or 'ắ' or 'ặ' or 'ẳ' or 'ẵ' => 'a',
                'è' or 'é' or 'ẹ' or 'ẻ' or 'ẽ' or 'ê' or 'ề' or 'ế' or 'ệ' or 'ể' or 'ễ' => 'e',
                'ì' or 'í' or 'ị' or 'ỉ' or 'ĩ' => 'i',
                'ò' or 'ó' or 'ọ' or 'ỏ' or 'õ' or 'ô' or 'ồ' or 'ố' or 'ộ' or 'ổ' or 'ỗ' or 'ơ' or 'ờ' or 'ớ' or 'ợ' or 'ở' or 'ỡ' => 'o',
                'ù' or 'ú' or 'ụ' or 'ủ' or 'ũ' or 'ư' or 'ừ' or 'ứ' or 'ự' or 'ử' or 'ữ' => 'u',
                'ỳ' or 'ý' or 'ỵ' or 'ỷ' or 'ỹ' => 'y',
                'đ' => 'd',
                ' ' or '_' => '-',
                _ when char.IsLetterOrDigit(c) => c,
                _ when c == '-' => '-',
                _ => ' '
            });
        }

        var collapsed = NonSlugChars.Replace(sb.ToString().Replace(" ", "-"), "-");
        while (collapsed.Contains("--", StringComparison.Ordinal))
        {
            collapsed = collapsed.Replace("--", "-", StringComparison.Ordinal);
        }

        return collapsed.Trim('-');
    }
}
