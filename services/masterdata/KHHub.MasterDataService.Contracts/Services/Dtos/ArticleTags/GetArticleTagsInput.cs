using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTags;

public abstract class GetArticleTagsInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? Description { get; set; }

    public int? UsageCountMin { get; set; }

    public int? UsageCountMax { get; set; }

    public GetArticleTagsInputBase()
    {
    }
}