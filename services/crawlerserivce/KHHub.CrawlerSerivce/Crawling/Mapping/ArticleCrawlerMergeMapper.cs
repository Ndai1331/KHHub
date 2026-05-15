using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Services.Dtos.ArticleCrawler;

namespace KHHub.CrawlerSerivce.Crawling.Mapping;

public static class ArticleCrawlerMergeMapper
{
    private const int SummaryMaxLength = 500;

    public static ArticleCrawlerUpsertInputDto ToUpsertDto(
        CrawledArticleListItem listing,
        CrawledArticleDetailPatch detail,
        Guid articleCategoryId,
        string defaultAuthorName,
        ArticleStatus defaultStatus,
        IReadOnlyList<string>? extraTagNames)
    {
        var categoryLabel = FirstNonEmpty(detail.CategoryLabel, listing.CategoryLabel);
        var articleType = ArticleTypeNormalizer.FromCategoryLabel(categoryLabel);

        var bodyHtml = FirstNonEmpty(detail.ContentHtml, $"<p>{WebUtility.HtmlEncode(listing.Title)}</p>");
        var body = bodyHtml ?? $"<p>{WebUtility.HtmlEncode(listing.Title)}</p>";
        var summary = FirstNonEmpty(
                          detail.Summary,
                          TruncatePlain(PlainStripHtml(detail.ContentHtml), SummaryMaxLength),
                          TruncatePlain(listing.Title, SummaryMaxLength))
                      ?? listing.Title[..Math.Min(listing.Title.Length, SummaryMaxLength)];

        var author = FirstNonEmpty(detail.AuthorName, defaultAuthorName) ?? defaultAuthorName;
        var sourceName = FirstNonEmpty(detail.SourceLabel, new Uri(listing.SourceAbsoluteUrl).Host);
        var publishedAt = detail.PublishedAt ?? listing.PublishedAt;

        var tags = new List<string>();
        if (extraTagNames != null)
        {
            tags.AddRange(extraTagNames.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Trim()));
        }

        foreach (var topic in detail.TopicLabels)
        {
            if (!string.IsNullOrWhiteSpace(topic))
            {
                tags.Add(topic.Trim());
            }
        }

        tags = tags.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        var seoDescription = TruncatePlain(
            FirstNonEmpty(detail.SeoDescription, summary),
            500);

        var categoryCandidates = new List<string>();
        if (!string.IsNullOrWhiteSpace(categoryLabel))
        {
            categoryCandidates.Add(categoryLabel.Trim());
        }

        return new ArticleCrawlerUpsertInputDto
        {
            SourceUrl = listing.SourceAbsoluteUrl,
            Title = listing.Title,
            Summary = summary!,
            Content = body,
            Type = articleType,
            AuthorName = author,
            Source = sourceName,
            PublishedAt = publishedAt,
            Status = defaultStatus,
            IsFeatured = false,
            IsHot = listing.IsHot,
            IsTrending = listing.IsTrending,
            ReadingTime = Math.Max(1, EstimateReadingMinutes(body)),
            SeoTitle = listing.Title.Length > ArticleConsts.SeoTitleMaxLength
                ? listing.Title[..ArticleConsts.SeoTitleMaxLength]
                : listing.Title,
            SeoDescription = seoDescription,
            SeoKeywords = categoryLabel,
            ThumbnailUrl = FirstNonEmpty(detail.ThumbnailUrl, listing.ThumbnailUrl),
            CoverImageUrl = FirstNonEmpty(detail.CoverImageUrl, detail.ThumbnailUrl, listing.ThumbnailUrl),
            ArticleCategoryId = articleCategoryId,
            PrimaryArticleCategoryName = categoryLabel,
            ArticleCategoryNameCandidates = categoryCandidates,
            TagNames = tags
        };
    }

    private static int EstimateReadingMinutes(string htmlOrText)
    {
        var plain = PlainStripHtml(htmlOrText);
        if (string.IsNullOrWhiteSpace(plain))
        {
            return 1;
        }

        var words = plain.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries).Length;
        return Math.Max(1, (words + 199) / 200);
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        return values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
    }

    private static string? TruncatePlain(string? text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var plain = text.Trim();
        return plain.Length <= maxLength ? plain : plain[..maxLength];
    }

    private static string? PlainStripHtml(string? html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return null;
        }

        var decoded = WebUtility.HtmlDecode(html).Trim();
        var stripped = Regex.Replace(decoded, "<.*?>", " ", RegexOptions.Singleline);
        return Regex.Replace(stripped, @"\s+", " ", RegexOptions.None).Trim();
    }
}
