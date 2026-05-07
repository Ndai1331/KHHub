namespace KHHub.Publish_website.Services.PublicContent;

public interface IPublicContentCatalog
{
    PagedContentResult<PublicContentCardViewModel> GetCards(PublicContentKind kind, PublicContentQuery query);

    FilterSidebarViewModel GetFilters(PublicContentKind kind, PublicContentQuery query, string actionPath, string title);

    DetailPageViewModel? GetDetail(PublicContentKind kind, string slug);

    IReadOnlyList<PublicContentCardViewModel> GetRelated(PublicContentKind kind, string slug, int maxCount);
}
