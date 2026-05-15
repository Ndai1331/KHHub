# Crawler Service — Articles (news) → MasterData

Luồng crawl tin tức Khánh Hòa, Nha Trang, Ninh Thuận; chuẩn hóa và ghi vào **MasterData** qua **Integration Service** (machine-to-machine).

## Pipeline

```
Listing URL → Parse listing → item (detailUrl + meta ngắn)
     → GET trang detail → normalize (JSON-LD NewsArticle / Open Graph / DOM)
     → ArticleCrawlerUpsertInputDto → IArticleCrawlerIntegrationService.UpsertFromCrawlerAsync
```

- **Idempotent key**: `SourceUrl` = URL chi tiết bài trên nguồn (giống `Job.ApplicationUrl`).
- **Tags**: `TagNames` + `ExtraTagNames` (import) → `ArticleTag` + `ArticleTagMapping` (thay thế toàn bộ mapping theo bài khi upsert).

## Phạm vi MasterData

| Thành phần | Crawler / Integration |
|------------|------------------------|
| `Article` | Có — create/update qua `ArticleCrawlerIntegrationService` |
| `ArticleTag` / `ArticleTagMapping` | Có — merge `TagNames` + `ExtraTagNames`, replace mapping |
| `ArticleCategory` | Fallback từ `Crawler:ArticleImportDefaults` hoặc request; resolver từ `ArticleCategoryNameCandidates` |
| `ArticleViews`, `ArticleFavorites`, … | Không — hành vi user trên app |

## Mặc định moderation

- Khuyến nghị **`ArticleStatus.Pending`** cho nội dung từ báo bên thứ ba; duyệt trên admin trước khi `Published`.

## Nguồn (Tier)

### Tier 1 — Phase 1 (MVP handlers)

| Site | Host | URL mẫu listing | Handler `SiteKey` |
|------|------|-----------------|---------------------|
| Báo Khánh Hòa | `baokhanhhoa.vn` | `https://baokhanhhoa.vn/` | `BaoKhanhHoa` |
| Báo Ninh Thuận | `baoninhthuan.com.vn` | `https://www.baoninhthuan.com.vn/` | `BaoNinhThuan` |

### Tier 2 — Sau MVP

- VNExpress (tag địa phương), Tuổi Trẻ / Dân trí (chuyên mục miền Trung)
- Portal Du lịch Khánh Hòa (`dulichkhanhhoa.com.vn` nếu còn hoạt động)

### Tier 3 — Tổng hợp / lifestyle

- Blog du lịch, aggregator (cần lọc nhiễu, tuân thủ robots.txt)

## Tag taxonomy (seed + crawl)

**Vùng (slug):** `khanh-hoa`, `nha-trang`, `ninh-thuan`

**Loại tin (slug gợi ý):** `tin-nong`, `du-lich`, `an-uong`, `su-kien`, `kinh-te-xa-hoi`

**ExtraTagNames (nguồn):** `BaoKhanhHoa`, `BaoNinhThuan`, …

## Parse strategy (per handler)

1. `application/ld+json` — `NewsArticle` / `Article`
2. Open Graph — `og:title`, `og:image`, `article:published_time`
3. DOM — selector riêng từng site (lưu snapshot HTML cho test)

**Hot signals:** nhãn "Tin nóng", block đầu trang → `IsHot` / `IsTrending` (tuỳ site).

## API trong Crawler

| Service | Vai trò |
|---------|---------|
| `IArticleListingCrawlAppService.PreviewListingAsync` | Preview, không ghi MasterData |
| `IArticleNewsImportAppService.ImportFromListingAsync` | Listing + detail + upsert |

**Body mẫu (import):**

```json
{
  "listingUrl": "https://baokhanhhoa.vn/",
  "maxPages": 1,
  "maxArticlesToImport": 10,
  "delayBetweenDetailRequestsMs": 800,
  "extraTagNames": ["BaoKhanhHoa", "khanh-hoa"]
}
```

`articleCategoryId` có thể bỏ qua nếu đã cấu hình **`Crawler:ArticleImportDefaults:ArticleCategoryId`**.

## Cấu hình (`appsettings`)

- `Crawler:ArticleImportDefaults:ArticleCategoryId`
- `Crawler:ArticleImportDefaults:DefaultAuthorName`
- `Crawler:ArticleImportDefaults:DefaultStatus` — `Pending` | `Published` (string)
- `RemoteServices:MasterDataService:BaseUrl`

## Tuân thủ

- Đọc **robots.txt** / ToS trước khi bật production.
- Rate limit: `delayBetweenDetailRequestsMs` ≥ 800ms.
- Bản quyền: ưu tiên trích dẫn + `Source` + `SourceUrl`; có thể chỉ lưu `Summary` + link nếu policy yêu cầu.

## Roadmap implement (checklist file)

| Phase | Nội dung |
|-------|----------|
| 0 | `IArticleCrawlerIntegrationService`, DTO, `ArticleCrawlerIntegrationService`, proxy Crawler |
| 0 | `IArticleNewsSiteHandler`, resolver, models, merge mapper, import/preview app services |
| 1 | `BaoKhanhHoaArticleSiteHandler` |
| 1 | `BaoNinhThuanArticleSiteHandler` |
| 2 | RSS/Atom, background job, auth production |
