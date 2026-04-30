using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCategories;

public abstract class GetArticleCategoriesInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public string? Icon { get; set; }

    public Guid? ParentId { get; set; }

    public int? DisplayOrderMin { get; set; }

    public int? DisplayOrderMax { get; set; }

    public bool? IsActive { get; set; }

    public string? ThumbnailUrl { get; set; }

    public GetArticleCategoriesInputBase()
    {
    }
}