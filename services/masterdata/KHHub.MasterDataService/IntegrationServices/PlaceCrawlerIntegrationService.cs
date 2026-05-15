using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KHHub.MasterDataService.Data.PlaceCategories;
using KHHub.MasterDataService.Data.PlaceTagMappings;
using KHHub.MasterDataService.Data.PlaceTags;
using KHHub.MasterDataService.Data.Places;
using KHHub.MasterDataService.Data.Wards;
using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Entities.PlaceTagMappings;
using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Services.Dtos.PlaceCrawler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace KHHub.MasterDataService.IntegrationServices;

public class PlaceCrawlerIntegrationService : ApplicationService, IPlaceCrawlerIntegrationService
{
    private readonly IPlaceRepository _placeRepository;
    private readonly PlaceManager _placeManager;
    private readonly IPlaceTagRepository _placeTagRepository;
    private readonly PlaceTagManager _placeTagManager;
    private readonly IPlaceTagMappingRepository _placeTagMappingRepository;
    private readonly PlaceTagMappingManager _placeTagMappingManager;
    private readonly IWardRepository _wardRepository;
    private readonly IPlaceCategoryRepository _placeCategoryRepository;
    private readonly PlaceCategoryManager _placeCategoryManager;

    private static readonly Regex NonSlugChars = new(@"[^a-z0-9\-]+", RegexOptions.Compiled);

    public PlaceCrawlerIntegrationService(
        IPlaceRepository placeRepository,
        PlaceManager placeManager,
        IPlaceTagRepository placeTagRepository,
        PlaceTagManager placeTagManager,
        IPlaceTagMappingRepository placeTagMappingRepository,
        PlaceTagMappingManager placeTagMappingManager,
        IWardRepository wardRepository,
        IPlaceCategoryRepository placeCategoryRepository,
        PlaceCategoryManager placeCategoryManager)
    {
        _placeRepository = placeRepository;
        _placeManager = placeManager;
        _placeTagRepository = placeTagRepository;
        _placeTagManager = placeTagManager;
        _placeTagMappingRepository = placeTagMappingRepository;
        _placeTagMappingManager = placeTagMappingManager;
        _wardRepository = wardRepository;
        _placeCategoryRepository = placeCategoryRepository;
        _placeCategoryManager = placeCategoryManager;
    }

    [AllowAnonymous]
    public virtual async Task<PlaceCrawlerUpsertResultDto> UpsertFromCrawlerAsync(PlaceCrawlerUpsertInputDto input)
    {
        var existing = await _placeRepository.FirstOrDefaultAsync(p => p.SourceUrl == input.SourceUrl);

        var wardId = await ResolveWardIdAsync(input.ProvinceId, input.WardNameCandidates, input.WardId);
        var placeCategoryId = await ResolvePlaceCategoryIdAsync(
            input.PrimaryPlaceCategoryName,
            input.PlaceCategoryNameCandidates,
            input.PlaceCategoryId);

        var slug = BuildSlug(input.Name, input.SourceUrl);
        var seoTitle = input.SeoTitle.Length > PlaceConsts.SeoTitleMaxLength
            ? input.SeoTitle[..PlaceConsts.SeoTitleMaxLength]
            : input.SeoTitle;

        var lat = input.Latitude ?? 0m;
        var lng = input.Longituded ?? 0m;

        var sourceLabel = input.CrawlerSourceLabel;

        Place place;
        var wasCreated = false;

        if (existing == null)
        {
            wasCreated = true;
            place = await _placeManager.CreateAsync(
                placeCategoryId,
                input.ProvinceId,
                wardId,
                input.Name,
                slug,
                lat,
                lng,
                input.PriceRange,
                input.Status,
                viewCount: 0,
                favoriteCount: 0,
                reviewCount: input.ReviewCount ?? 0,
                ratingAveraged: input.RatingAveraged ?? 0m,
                ratingTotal: input.RatingTotal ?? 0,
                input.IsFeatured,
                input.IsHot,
                input.IsVerified,
                seoTitle,
                input.ShortDescription,
                input.Description,
                input.ThumbnailUrl,
                input.CoverImageUrl,
                input.Address,
                input.PhoneNumber,
                input.Email,
                input.Website,
                input.OpeningHours,
                input.GoogleMapUrl,
                input.SeoDescription ?? input.ShortDescription,
                input.SeoKeywords,
                sourceLabel,
                input.SourceUrl);
        }
        else
        {
            place = await _placeManager.UpdateAsync(
                existing.Id,
                placeCategoryId,
                input.ProvinceId,
                wardId,
                input.Name,
                slug,
                lat,
                lng,
                input.PriceRange,
                input.Status,
                existing.ViewCount,
                existing.FavoriteCount,
                input.ReviewCount ?? existing.ReviewCount,
                input.RatingAveraged ?? existing.RatingAveraged,
                input.RatingTotal ?? existing.RatingTotal,
                input.IsFeatured,
                input.IsHot,
                input.IsVerified,
                seoTitle,
                input.ShortDescription ?? existing.ShortDescription,
                input.Description ?? existing.Description,
                input.ThumbnailUrl ?? existing.ThumbnailUrl,
                input.CoverImageUrl ?? existing.CoverImageUrl,
                input.Address ?? existing.Address,
                input.PhoneNumber ?? existing.PhoneNumber,
                input.Email ?? existing.Email,
                input.Website ?? existing.Website,
                input.OpeningHours ?? existing.OpeningHours,
                input.GoogleMapUrl ?? existing.GoogleMapUrl,
                input.SeoDescription ?? existing.SeoDescription,
                input.SeoKeywords ?? existing.SeoKeywords,
                sourceLabel ?? existing.Source,
                input.SourceUrl,
                existing.ConcurrencyStamp);
        }

        await ReplacePlaceTagsAsync(place.Id, input.TagNames);

        return new PlaceCrawlerUpsertResultDto { PlaceId = place.Id, WasCreated = wasCreated };
    }

    private async Task<Guid> ResolveWardIdAsync(Guid provinceId, List<string>? candidates, Guid fallbackId)
    {
        if (candidates == null || candidates.Count == 0)
        {
            return fallbackId;
        }

        var queryable = await _wardRepository.GetQueryableAsync();
        var wards = await AsyncExecuter.ToListAsync(queryable.Where(w => w.ProvinceId == provinceId));
        if (wards.Count == 0)
        {
            return fallbackId;
        }

        var ordered = candidates
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Select(c => c.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(c => ComparableAsciiKey(c).Length)
            .ToList();

        foreach (var c in ordered)
        {
            var ck = ComparableAsciiKey(c);
            if (ck.Length == 0)
            {
                continue;
            }

            var hit = wards.FirstOrDefault(w => ComparableAsciiKey(w.Name) == ck);
            if (hit != null)
            {
                return hit.Id;
            }
        }

        foreach (var c in ordered)
        {
            var ck = ComparableAsciiKey(c);
            if (ck.Length < 6)
            {
                continue;
            }

            var hit = wards.FirstOrDefault(w => ComparableAsciiKey(w.Name).Contains(ck, StringComparison.Ordinal));
            if (hit != null)
            {
                return hit.Id;
            }
        }

        return fallbackId;
    }

    private async Task<Guid> ResolvePlaceCategoryIdAsync(string? primaryName, List<string>? candidates, Guid fallbackId)
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
            var id = await TryResolveOrCreatePlaceCategoryIdAsync(name);
            if (id != null)
            {
                return id.Value;
            }
        }

        return fallbackId;
    }

    private async Task<Guid?> TryResolveOrCreatePlaceCategoryIdAsync(string name)
    {
        var trimmed = name.Trim();
        if (trimmed.Length == 0)
        {
            return null;
        }

        var queryable = await _placeCategoryRepository.GetQueryableAsync();
        var categories = await AsyncExecuter.ToListAsync(queryable.Where(c => c.IsActive));
        var key = ComparableAsciiKey(trimmed);
        var existing = categories.FirstOrDefault(c => ComparableAsciiKey(c.Name) == key);
        if (existing != null)
        {
            return existing.Id;
        }

        var slug = BuildPlaceCategorySlug(trimmed);
        var existingBySlug = categories.FirstOrDefault(c =>
            string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));
        if (existingBySlug != null)
        {
            return existingBySlug.Id;
        }

        var nextOrder = categories.Count == 0 ? 1 : categories.Max(c => c.DisplayOrder) + 1;
        var created = await _placeCategoryManager.CreateAsync(trimmed, slug, nextOrder, true);
        return created.Id;
    }

    private static string BuildPlaceCategorySlug(string name)
    {
        var ascii = SlugifyAsciiSegment(name);
        if (string.IsNullOrEmpty(ascii))
        {
            ascii = "place-category";
        }

        return ascii.Length > PlaceCategoryConsts.SlugMaxLength
            ? ascii[..PlaceCategoryConsts.SlugMaxLength]
            : ascii;
    }

    private async Task ReplacePlaceTagsAsync(Guid placeId, List<string> tagNames)
    {
        var queryable = await _placeTagMappingRepository.GetQueryableAsync();
        var mappingIds = queryable.Where(m => m.PlaceId == placeId).Select(m => m.Id).ToList();
        await _placeTagMappingRepository.DeleteManyAsync(mappingIds);

        var order = 0;
        foreach (var rawName in tagNames.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            order++;
            var name = rawName.Trim();
            var tagSlug = BuildTagSlug(name);
            var tag = await _placeTagRepository.FirstOrDefaultAsync(t => t.Slug == tagSlug);
            if (tag == null)
            {
                tag = await _placeTagManager.CreateAsync(name, tagSlug, usageCount: 0);
            }

            await _placeTagMappingManager.CreateAsync(tag.Id, placeId, isPrimary: order == 1, sortOrder: order);
        }
    }

    private static string BuildSlug(string name, string sourceUrl)
    {
        var ascii = SlugifyAsciiSegment(name);
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(sourceUrl))).ToLowerInvariant();
        var combined = $"{ascii}-{hash[..16]}".Trim('-');
        if (combined.Length > PlaceConsts.SlugMaxLength)
        {
            combined = combined[..PlaceConsts.SlugMaxLength];
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
        if (combined.Length > PlaceTagConsts.SlugMaxLength)
        {
            combined = combined[..PlaceTagConsts.SlugMaxLength];
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
