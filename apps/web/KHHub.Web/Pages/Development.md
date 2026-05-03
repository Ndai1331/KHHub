# Chung Articles & Places

## Add/Edit Article & Place – Tag Management
**Status: DONE**

Enhance the **Add/Edit Article** and **Add/Edit Place** screens with tag management functionality.

### Requirements
- Allow users to:
  - Create new tags
  - Select existing tags
- Support attaching tags to:
  - Articles (`ArticleTags`)
  - Places (`PlacesTags`)
- Tag input should support:
  - Search existing tags
  - Multi-select
  - Inline tag creation (similar to modern CMS systems)

---

## UI Structure Update
**Status: DONE**

Replace the current section layout with tab-based navigation.

### Tabs
1. **General Information**
2. **Reviews**

### Reviews Tab
Implement full CRUD functionality for reviews:
- Create review
- Edit review
- Delete review
- View review list

> **Note:** `PlaceReview` exists in MasterData. **Articles** use an informational tab (no `ArticleReview` entity in backend yet).

---

# Places Only

## Media Gallery Enhancement
**Status: DONE**

Add a new **Medias** section below the cover image.

### Requirements
- Allow uploading/selecting multiple images (`EntityFiles`)  and show progessing uploading 
- Display images similar to WordPress gallery behavior:
  - Show around 5 thumbnail images initially
  - If more than 5 images exist:
    - Display `...` or a `View More` button
- Clicking `View More` should:
  - Open a modal
  - Display all mapped images in a carousel/slider view

### Suggested Features
- Responsive gallery layout
- Image preview support
- Smooth carousel navigation
- Lazy loading for large image sets

> **Note:** Gallery uses **Media Library** pick → `EntityFiles` with `EntityType = Place`, `Collection = Gallery`. New uploads go through Media Explorer then pick (busy state during add).

---

## Favorite Users Tab
**Status: DONE**

Add a new tab: **Favorites**

### Requirements
Display users who marked the place as favorite:
- User list
- Pagination support
- Basic user information:
  - Avatar
  - Name
  - Email (optional depending on permission)

### Features
- Server-side pagination
- Search/filter support (optional)
- Sort by latest favorite date (optional)

> **Note:** User display uses `UserId` plus placeholder avatar UI; email column appears only when `AbpIdentity.Users` is granted.
