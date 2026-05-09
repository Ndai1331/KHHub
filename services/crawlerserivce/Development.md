# Crawler Service — job listing → detail → MasterData

Luồng crawl đa nguồn (ưu tiên Khánh Hòa / Nha Trang), chuẩn hóa và ghi vào **MasterData** qua Integration Service.

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
- `RemoteServices:MasterDataService:BaseUrl`

---

## Tuân thủ

- Điều khoản dịch vụ / robots.txt / tần suất crawl.
- `DelayBetweenDetailRequestsMs` và `MaxJobsToImport` để tránh tải quá tải nguồn.
