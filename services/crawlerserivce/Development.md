# Crawler Service — job listing → detail → MasterData

Luồng crawl đa nguồn (ưu tiên Khánh Hòa / Nha Trang), chuẩn hóa và ghi vào **MasterData** qua Integration Service.

---

## Cách chạy Crawler (việc làm, tin tức, địa điểm)

### 1. Chuẩn bị

| Thành phần | Ghi chú |
|------------|---------|
| **PostgreSQL** | Crawler + MasterData có connection string trong `appsettings` |
| **Redis / RabbitMQ** | Bật theo template microservice (Crawler phụ thuộc) |
| **MasterDataService** | **Phải chạy trước** khi gọi **Import** (ghi DB). Preview chỉ cần Crawler. |
| **Migration MasterData** | Sau khi có migration mới: `dotnet ef database update` (từ project EF / DbMigrator) |

### 2. Cấu hình `KHHub.CrawlerSerivce/appsettings.json`

| Key | Mục đích |
|-----|----------|
| `RemoteServices:MasterDataService:BaseUrl` | URL API MasterData đang chạy (ví dụ `http://localhost:44381/` — đổi đúng port máy bạn) |
| `Crawler:ImportDefaults` | **Việc làm:** `ProvinceId`, `WardId`, `JobCategoryId` khi request import không gửi hoặc gửi GUID rỗng |
| `Crawler:ArticleImportDefaults` | **Tin tức:** bắt buộc có `ArticleCategoryId` **hợp lệ** trong DB; thêm `DefaultAuthorName`, `DefaultStatus` (`Pending` / `Published` …) |
| `Crawler:PlaceImportDefaults` | **Địa điểm:** `ProvinceId`, `WardId`, `PlaceCategoryId` hợp lệ; có thể trùng tỉnh/xã với `ImportDefaults`; `DefaultStatus` (mặc định `Draft`) |
| `Crawler:ScheduledImports` | **Lịch tự động:** bật/tắt tổng `Enabled`, múi giờ `TimeZoneId` (mặc định `Asia/Ho_Chi_Minh`), từng loại `Jobs` / `Articles` / `Places` có `Enabled`, `Cron` (5 trường chuẩn) và `Import` (cùng field như body API `ImportFromListingAsync`) |

Thay mọi GUID placeholder (`00000000-0000-...`) bằng ID thật lấy từ MasterData (category tin / category địa điểm).

**Cron mặc định trong mẫu config:** việc làm `0 7,17 * * *` (07:00 và 17:00), tin `0 * * * *` mỗi giờ đúng phút 0, địa điểm `0 12 * * *` (12:00). Mẫu `appsettings` để `Enabled: false` cho đến khi bạn chỉnh URL listing và bật từng nhánh.

### 3. Chạy dịch vụ (terminal)

```bash
# Terminal 1 — MasterData
cd services/masterdata/KHHub.MasterDataService
dotnet run

# Terminal 2 — Crawler
cd services/crawlerserivce/KHHub.CrawlerSerivce
dotnet run
```

*(Nếu dùng .NET Aspire: chạy `aspire/app-host` và bật các project tương ứng; chỉnh `BaseUrl` theo URL mà Aspire cấp.)*

Mở **Swagger** của Crawler (bật khi `Swagger:IsEnabled: true`) — gọi API theo bảng dưới.

### 4. Ba luồng: API và cấu hình tối thiểu

| Loại | Preview (không ghi MasterData) | Import (listing → detail → MasterData) | `appsettings` cần cho import |
|------|--------------------------------|----------------------------------------|------------------------------|
| **Việc làm** | `IJobListingCrawlAppService` → `PreviewListingAsync` | `IJobBoardImportAppService` → `ImportFromListingAsync` | `Crawler:ImportDefaults` (hoặc gửi `provinceId` / `wardId` / `jobCategoryId` trong body) |
| **Tin tức** | `IArticleListingCrawlAppService` → `PreviewListingAsync` | `IArticleNewsImportAppService` → `ImportFromListingAsync` | `Crawler:ArticleImportDefaults:ArticleCategoryId` (+ có thể chỉ `DefaultAuthorName`, `DefaultStatus`) |
| **Địa điểm** | `IPlaceListingCrawlAppService` → `PreviewListingAsync` | `IPlaceDirectoryImportAppService` → `ImportFromListingAsync` | `Crawler:PlaceImportDefaults` (`ProvinceId`, `WardId`, `PlaceCategoryId`) |

**Gợi ý URL listing (mẫu):**

- Việc làm: `https://nhatrangjob.vn/viec-lam`
- Tin: `https://www.baokhanhhoa.vn/` (handler `BaoKhanhHoa`) hoặc `https://www.baoninhthuan.com.vn/`
- Địa điểm: `https://www.pasgo.vn/nha-trang/an-uong` (handler Pasgo Nha Trang — URL listing phải khớp handler)

**Thứ tự an toàn:** luôn **Preview** trước → ổn định rồi **Import**; tăng `delayBetweenDetailRequestsMs` nếu nguồn chặn bot.

**Ví dụ body Import (Swagger):**

Việc làm — xem mục [API trong Crawler](#api-trong-crawler) bên dưới.

Tin tức:

```json
{
  "listingUrl": "https://www.baokhanhhoa.vn/",
  "maxPages": 1,
  "maxArticlesToImport": 5,
  "delayBetweenDetailRequestsMs": 800,
  "extraTagNames": ["BaoKhanhHoa", "khanh-hoa"]
}
```

Địa điểm:

```json
{
  "listingUrl": "https://www.pasgo.vn/nha-trang/an-uong",
  "maxPages": 1,
  "maxPlacesToImport": 5,
  "delayBetweenDetailRequestsMs": 1000,
  "extraTagNames": ["Pasgo", "nha-trang"]
}
```

Chi tiết thêm — Job: các mục dưới; Articles / Places: [Planning-Articles](./Planning-Articles.md), [Planning-Places](./Planning-Places.md).

---

## Lịch crawl tự động (ScheduledImports)

- **Cơ chế:** `AsyncPeriodicBackgroundWorker` của ABP tick **mỗi 1 phút**, đối chiếu **Cronos** với `TimeZoneId`. Khi khớp một “slot” phút, service **enqueue** job vào **RabbitMQ** (`Volo.Abp.BackgroundJobs.RabbitMQ`); worker chạy `AsyncBackgroundJob` và gọi lại đúng `ImportFromListingAsync` (không nhân đôi logic crawl).
- **Không enqueue trùng cùng slot:** `IAbpDistributedLock` (Redis/Medallion qua `AbpDistributedLocking`) với khóa theo slot, ví dụ `Crawler:Schedule:Jobs:2026-05-15T07:00` (thời điểm theo múi giờ cấu hình). Nhiều replica Crawler: chỉ một instance enqueue cho mỗi slot.
- **Hạ tầng phải chạy:** **Redis** (cache + distributed lock), **RabbitMQ** (hàng đợi job). Không có RabbitMQ thì job không được xử lý.
- **“Không trùng bản ghi đã cào”:** phía **MasterData** — upsert theo URL (`ApplicationUrl` / `SourceUrl`); import lại chỉ cập nhật bản ghi hiện có.

Bật lịch: `Crawler:ScheduledImports:Enabled` = `true` và bật `Jobs.Enabled` / `Articles.Enabled` / `Places.Enabled` tùy nhu cầu; điền `Import:ListingUrl` (và các field giới hạn/delay như khi gọi Swagger).

---

## Pipeline

```
Listing URL → Parse listing → sourceAbsoluteUrl + meta ngắn
     → GET trang detail → normalize (JSON-LD / HTML theo site)
     → JobCrawlerUpsertInputDto → MasterData IJobCrawlerIntegrationService.UpsertFromCrawlerAsync
```

- **Idempotent key**: `ApplicationUrl` = URL chi tiết nguồn (`sourceAbsoluteUrl`).
- **Tags**: `ExtraTagNames` (import) → `JobTag` + `JobTagMapping` (replace toàn bộ mapping theo job khi upsert).

### Phạm vi MasterData

| Thành phần | Crawler / Integration |
|------------|------------------------|
| `Job` | Có — create/update qua `JobCrawlerIntegrationService` |
| `JobTag` / `JobTagMapping` | Có — theo `TagNames` + `ExtraTagNames` |
| `JobCategory`, `Province`, `Ward` | Fallback từ `Crawler:ImportDefaults` trong `appsettings`; có thể **ghi đè** bằng body (`provinceId`, `wardId`, `jobCategoryId`). Resolver ward/category từ trang detail vẫn áp dụng như trước. |
| `JobViews` | Không — đếm lượt xem từ người dùng app |
| `JobFavorites` | Không — hành vi user |
| `JobApplications` | Không — hồ sơ ứng viên |

---

## API trong Crawler

### 1. Preview listing (không ghi DB MasterData)

`IJobListingCrawlAppService.PreviewListingAsync` — chỉ trả danh sách rút gọn.

### 2. Import đầy đủ (listing + detail + MasterData)

`IJobBoardImportAppService.ImportFromListingAsync`  
Input `ImportJobListingInput`:

- `ListingUrl`, `MaxPages` (1–1000), `MaxJobsToImport?`, `DelayBetweenDetailRequestsMs`
- `ProvinceId?`, `WardId?`, `JobCategoryId?` — **tuỳ chọn**; khi bỏ qua hoặc `null`/empty GUID thì dùng **`Crawler:ImportDefaults`** trong `appsettings.json`.
- `ExtraTagNames` (tuỳ chọn)

**Điều kiện chạy:**

1. **MasterData** đang chạy — mặc định `RemoteServices:MasterDataService:BaseUrl` = `http://localhost:44381/` trong `appsettings.json`.
2. Redis/RabbitMQ theo template microservice (đã có từ project Crawler).
3. Ít nhất một trong hai: đã cấu hình **`Crawler:ImportDefaults`** (ProvinceId, WardId, JobCategoryId) **hoặc** gửi ba GUID trong body.

Swagger body tối thiểu (dùng default trong appsettings):

```json
{
  "listingUrl": "https://nhatrangjob.vn/viec-lam",
  "maxPages": 1,
  "maxJobsToImport": 5,
  "delayBetweenDetailRequestsMs": 500,
  "extraTagNames": ["NhaTrangJob"]
}
```

Ví dụ **ghi đè** một phần (chỉ gửi field cần thay):

```json
{
  "listingUrl": "https://nhatrangjob.vn/viec-lam",
  "maxPages": 1,
  "maxJobsToImport": 5,
  "delayBetweenDetailRequestsMs": 500,
  "wardId": "11111111-1111-1111-1111-111111111111",
  "extraTagNames": ["NhaTrangJob"]
}
```

Quét **nhiều trang listing** và **không giới hạn** số tin (trong phạm vi các trang đó): đặt `maxJobsToImport` = `null`, `maxPages` = số trang tối đa (tối đa **1000**). Có thể bỏ `provinceId`/`wardId`/`jobCategoryId` nếu đã set trong appsettings:

```json
{
  "listingUrl": "https://nhatrangjob.vn/viec-lam",
  "maxPages": 500,
  "maxJobsToImport": null,
  "delayBetweenDetailRequestsMs": 500,
  "extraTagNames": ["NhaTrangJob"]
}
```

> Endpoint đang `[AllowAnonymous]` để debug — production nên khóa auth / client-credentials.

---

## MasterData — Integration

- `IJobCrawlerIntegrationService` + `JobCrawlerUpsertInputDto` / `JobCrawlerUpsertResultDto` (Contracts).
- `JobCrawlerIntegrationService`: upsert theo `ApplicationUrl`, đồng bộ tag mapping.

---

## Mở rộng site mới

| Thành phần | Vai trò |
|------------|---------|
| `IJobBoardSiteHandler` | Một site: `SupportsListingPage`, `SupportsJobDetailPage`, `BuildListingPageUri`, `ParseListingHtmlAsync`, `ParseJobDetailHtmlAsync`. |
| `JobBoardSiteHandlerResolver` | `ResolveForListing` / `ResolveForJobDetail`. |
| `CrawlerHtmlFetcher` | Client `CrawlerSerivce.Html`. |
| `EmploymentTypeNormalizer` | Map label tiếng Việt / schema.org → enum MasterData. |
| `JobCrawlerMergeMapper` | Gộp `CrawledJobListItem` + `CrawledJobDetailPatch` → `JobCrawlerUpsertInputDto`. |

**Site mẫu**: `NhaTrangJobBoardSiteHandler` — listing HTML + detail **JSON-LD `JobPosting`** khi có.

Checklist thêm site:

1. Thêm class trong `Crawling/Sites/<Ten>/` implement `IJobBoardSiteHandler` + `ITransientDependency`.
2. Phân biệt rõ URL listing vs URL detail (`SupportsListingPage` / `SupportsJobDetailPage`).
3. Tuỳ site: ưu tiên JSON-LD, Open Graph, hoặc DOM — snapshot HTML cho test.

---

## Cấu hình

- `Crawler:UserAgent`
- `Crawler:ImportDefaults:ProvinceId`, `WardId`, `JobCategoryId` — mặc định cho import khi request không gửi hoặc gửi empty GUID.
- `Crawler:ScheduledImports` — lịch tự động (xem mục **Lịch crawl tự động (ScheduledImports)** ở trên).
- `RemoteServices:MasterDataService:BaseUrl`

---

## Tuân thủ

- Điều khoản dịch vụ / robots.txt / tần suất crawl.
- `DelayBetweenDetailRequestsMs` và `MaxJobsToImport` để tránh tải quá tải nguồn.

---

## Related planning

- **Articles (tin tức):** [Planning-Articles.md](./Planning-Articles.md) — pipeline, nguồn Tier 1–3, tags, handler checklist, moderation.
- **Places (địa điểm):** [Planning-Places.md](./Planning-Places.md) — pipeline, `Place.SourceUrl`, Pasgo, geocoding roadmap.

### Articles → MasterData

- **Integration:** `IArticleCrawlerIntegrationService` + `ArticleCrawlerUpsertInputDto` / `ArticleCrawlerUpsertResultDto`
- **Preview (không ghi DB):** `IArticleListingCrawlAppService.PreviewListingAsync`
- **Import:** `IArticleNewsImportAppService.ImportFromListingAsync`
- **Cấu hình:** `Crawler:ArticleImportDefaults:ArticleCategoryId`, `DefaultAuthorName`, `DefaultStatus` — **ArticleCategoryId** phải là GUID hợp lệ trong MasterData (thay placeholder `00000000-...` trong `appsettings.json`)

### Places → MasterData

- **Integration:** `IPlaceCrawlerIntegrationService` + `PlaceCrawlerUpsertInputDto` / `PlaceCrawlerUpsertResultDto`
- **Preview:** `IPlaceListingCrawlAppService.PreviewListingAsync`
- **Import:** `IPlaceDirectoryImportAppService.ImportFromListingAsync`
- **Cấu hình:** `Crawler:PlaceImportDefaults` (ProvinceId, WardId, PlaceCategoryId) — có thể tái sử dụng tỉnh/xã từ `Crawler:ImportDefaults`; **PlaceCategoryId** phải là GUID hợp lệ (thay placeholder trong `appsettings.json`)

