# Crawler Service — Places (địa điểm) → MasterData

Luồng crawl địa điểm du lịch, ăn uống, khám phá (Khánh Hòa, Nha Trang, Ninh Thuận); ghi vào **MasterData** qua **Integration Service**.

## Pipeline

```
Listing URL → Parse listing → item (detailUrl + meta ngắn)
     → GET trang detail → normalize (JSON-LD LocalBusiness / Open Graph / DOM)
     → PlaceCrawlerUpsertInputDto → IPlaceCrawlerIntegrationService.UpsertFromCrawlerAsync
```

- **Idempotent key**: `SourceUrl` (URL chi tiết địa điểm trên nguồn) — **field mới** trên entity `Place` + unique index (nullable).
- **Tags**: `TagNames` + `ExtraTagNames` → `PlaceTag` + `PlaceTagMapping` (replace mapping khi upsert).

## Phạm vi MasterData

| Thành phần | Crawler / Integration |
|------------|------------------------|
| `Place` | Có — create/update; **cần migration thêm `SourceUrl`** |
| `PlaceTag` / `PlaceTagMapping` | Có |
| `Province`, `Ward`, `PlaceCategory` | Fallback `Crawler:PlaceImportDefaults` + `WardNameCandidates` resolver |
| Rating từ nguồn | Có thể map `RatingAveraged`, `ReviewCount` từ metadata site (không thay review user trong app) |
| `PlaceReviews` (user) | Không crawl text review UGC đầy đủ nếu vi phạm ToS |

## Mặc định moderation

- **`PlaceStatus.Draft`** làm mặc định cho import tự động (chờ duyệt).

## Nguồn (Tier)

### Tier 1 — Phase 1 (MVP handler)

| Site | Handler `SiteKey` | URL mẫu |
|------|-------------------|---------|
| Pasgo (Nha Trang / KH) | `PasgoNhaTrang` | `https://www.pasgo.vn/nha-trang/...` |

### Tier 2

- Portal Du lịch Khánh Hòa, Ninh Thuận tourism
- Foody / Lozi (độ khó cao anti-bot)

### Tier 3

- Google Maps: không scrape — dùng **Places API** nếu cần

## Tag taxonomy

**Vùng:** `khanh-hoa`, `nha-trang`, `ninh-thuan`, `cam-ranh`, …

**Loại hình:** `am-thuc`, `bien-dao`, `homestay-khach-san`, `di-tich`, `giai-tri`

**Nguồn:** `Pasgo`, …

## Geocoding (Phase 2+)

- Nếu thiếu lat/lng: import với `WardId` mặc định; queue geocode (Nominatim / Google Geocoding API) sau.

## API trong Crawler

| Service | Vai trò |
|---------|---------|
| `IPlaceListingCrawlAppService.PreviewListingAsync` | Preview |
| `IPlaceDirectoryImportAppService.ImportFromListingAsync` | Full import |

**Body mẫu:**

```json
{
  "listingUrl": "https://www.pasgo.vn/nha-trang/an-uong",
  "maxPages": 2,
  "maxPlacesToImport": 20,
  "delayBetweenDetailRequestsMs": 1000,
  "extraTagNames": ["Pasgo", "nha-trang", "am-thuc"]
}
```

## Cấu hình (`appsettings`)

- `Crawler:PlaceImportDefaults:ProvinceId`
- `Crawler:PlaceImportDefaults:WardId`
- `Crawler:PlaceImportDefaults:PlaceCategoryId`

**Lấy GUID thật** từ DB MasterData (Province Khánh Hòa, Ward mặc định, PlaceCategory mặc định) — không hardcode production.

## Tuân thủ

- robots.txt / rate limit / User-Agent có contact.
- Pasgo có thể chặn bot — retry + backoff; nếu không ổn định, chuyển Phase 2 partnership/API.

## Roadmap implement

| Phase | Nội dung |
|-------|----------|
| 0 | Migration `Place.SourceUrl`, `IPlaceCrawlerIntegrationService`, DTO, implementation |
| 0 | `IPlaceDirectorySiteHandler`, resolver, models, merge mapper, import/preview |
| 1 | `PasgoNhaTrangPlaceSiteHandler` |
| 2 | Geocoding queue, thêm nguồn chính quyền du lịch |
| 3 | Background job, auth production |
