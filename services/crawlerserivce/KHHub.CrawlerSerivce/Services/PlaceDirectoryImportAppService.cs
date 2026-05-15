using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Configuration;
using KHHub.CrawlerSerivce.Crawling.Abstractions;
using KHHub.CrawlerSerivce.Crawling.Http;
using KHHub.CrawlerSerivce.Crawling.Mapping;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.IntegrationServices;
using KHHub.MasterDataService.Services.Dtos.PlaceCrawler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

[AllowAnonymous]
public class PlaceDirectoryImportAppService : CrawlerSerivceAppService, IPlaceDirectoryImportAppService
{
    private const int MasterDataUpsertMaxAttempts = 3;

    private readonly IPlaceDirectorySiteHandlerResolver _resolver;
    private readonly ICrawlerHtmlFetcher _htmlFetcher;
    private readonly IPlaceCrawlerIntegrationService _placeCrawler;
    private readonly CrawlerImportDefaultsOptions _jobImportDefaults;
    private readonly CrawlerPlaceImportDefaultsOptions _placeDefaults;

    public PlaceDirectoryImportAppService(
        IPlaceDirectorySiteHandlerResolver resolver,
        ICrawlerHtmlFetcher htmlFetcher,
        IPlaceCrawlerIntegrationService placeCrawler,
        IOptions<CrawlerImportDefaultsOptions> jobImportDefaults,
        IOptions<CrawlerPlaceImportDefaultsOptions> placeDefaults)
    {
        _resolver = resolver;
        _htmlFetcher = htmlFetcher;
        _placeCrawler = placeCrawler;
        _jobImportDefaults = jobImportDefaults.Value;
        _placeDefaults = placeDefaults.Value;
    }

    public async Task<ImportPlaceListingResultDto> ImportFromListingAsync(ImportPlaceListingInput input)
    {
        var provinceId = ResolveGuidOrDefault(input.ProvinceId, _placeDefaults.ProvinceId != Guid.Empty ? _placeDefaults.ProvinceId : _jobImportDefaults.ProvinceId);
        var wardId = ResolveGuidOrDefault(input.WardId, _placeDefaults.WardId != Guid.Empty ? _placeDefaults.WardId : _jobImportDefaults.WardId);
        var placeCategoryId = ResolveGuidOrDefault(input.PlaceCategoryId, _placeDefaults.PlaceCategoryId);

        if (provinceId == Guid.Empty || wardId == Guid.Empty || placeCategoryId == Guid.Empty)
        {
            throw new Volo.Abp.UserFriendlyException(
                "ProvinceId, WardId and PlaceCategoryId are required: set Crawler:PlaceImportDefaults (or Crawler:ImportDefaults for province/ward) in appsettings or pass them in the request body.");
        }

        var status = ParsePlaceStatus(_placeDefaults.DefaultStatus);

        Uri seed;
        try
        {
            seed = new Uri(input.ListingUrl, UriKind.Absolute);
        }
        catch (UriFormatException ex)
        {
            throw new Volo.Abp.UserFriendlyException($"ListingUrl is invalid: {ex.Message}");
        }

        var listingHandler = _resolver.ResolveForListing(seed);
        var result = new ImportPlaceListingResultDto { SiteKey = listingHandler.SiteKey };

        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        var rowsQueued = new List<CrawledPlaceListItem>();

        for (var page = 1; page <= input.MaxPages; page++)
        {
            if (input.MaxPlacesToImport.HasValue && rowsQueued.Count >= input.MaxPlacesToImport.Value)
            {
                break;
            }

            var pageUri = listingHandler.BuildListingPageUri(seed, page);
            var fetchResult = await CrawlerHtmlFetchRetry.GetStringWithRetriesAsync(_htmlFetcher, pageUri);
            if (!fetchResult.Success)
            {
                result.Warnings.Add(
                    $"Listing page {page} ({pageUri}): failed after {fetchResult.AttemptsMade} attempt(s) — {fetchResult.ErrorMessage}");
                break;
            }

            var rows = await listingHandler.ParseListingHtmlAsync(fetchResult.Html!, pageUri);
            foreach (var row in rows)
            {
                if (!seen.Add(row.SourceAbsoluteUrl))
                {
                    continue;
                }

                rowsQueued.Add(row);
                result.ListingRowsSeen++;

                if (input.MaxPlacesToImport.HasValue && rowsQueued.Count >= input.MaxPlacesToImport.Value)
                {
                    break;
                }
            }
        }

        foreach (var row in rowsQueued)
        {
            var detailUri = new Uri(row.SourceAbsoluteUrl);
            CrawledPlaceDetailPatch detail;
            try
            {
                var detailHandler = _resolver.ResolveForPlaceDetail(detailUri);
                var detailHtml = await _htmlFetcher.GetStringAsync(detailUri);
                detail = await detailHandler.ParsePlaceDetailHtmlAsync(detailHtml, detailUri);
            }
            catch (Exception ex)
            {
                result.Errors.Add($"{row.SourceAbsoluteUrl}: detail fetch/parse failed — {ex.Message}");
                continue;
            }

            var upsert = PlaceCrawlerMergeMapper.ToUpsertDto(
                row,
                detail,
                provinceId,
                wardId,
                placeCategoryId,
                status,
                input.ExtraTagNames);

            var upsertResult = await UpsertPlaceWithRetriesAsync(upsert, row.SourceAbsoluteUrl, result);
            if (upsertResult == null)
            {
                result.Warnings.Add(
                    "Place import stopped: MasterData upsert failed after retries; remaining queued places were not processed.");
                break;
            }

            result.PlacesProcessed++;
            if (upsertResult.WasCreated)
            {
                result.PlacesCreated++;
            }
            else
            {
                result.PlacesUpdated++;
            }

            if (input.DelayBetweenDetailRequestsMs > 0)
            {
                await Task.Delay(input.DelayBetweenDetailRequestsMs);
            }
        }

        return result;
    }

    private async Task<PlaceCrawlerUpsertResultDto?> UpsertPlaceWithRetriesAsync(
        PlaceCrawlerUpsertInputDto upsert,
        string sourceAbsoluteUrl,
        ImportPlaceListingResultDto result)
    {
        Exception? lastEx = null;
        for (var attempt = 1; attempt <= MasterDataUpsertMaxAttempts; attempt++)
        {
            try
            {
                return await _placeCrawler.UpsertFromCrawlerAsync(upsert);
            }
            catch (Exception ex)
            {
                lastEx = ex;
                if (attempt < MasterDataUpsertMaxAttempts)
                {
                    await Task.Delay(TimeSpan.FromMilliseconds(300 * attempt));
                }
            }
        }

        result.Errors.Add(
            $"{sourceAbsoluteUrl}: MasterData upsert failed after {MasterDataUpsertMaxAttempts} attempt(s) — {lastEx!.Message}");
        return null;
    }

    private static Guid ResolveGuidOrDefault(Guid? requestValue, Guid configuredDefault)
    {
        if (requestValue is { } v && v != Guid.Empty)
        {
            return v;
        }

        return configuredDefault;
    }

    private static PlaceStatus ParsePlaceStatus(string? s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return PlaceStatus.Draft;
        }

        return Enum.TryParse<PlaceStatus>(s, true, out var st) ? st : PlaceStatus.Draft;
    }
}
