using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleViews;

public abstract class GetArticleViewsInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? IpAddress { get; set; }

    public string? Device { get; set; }

    public string? Source { get; set; }

    public DateTime? ViewedAtMin { get; set; }

    public DateTime? ViewedAtMax { get; set; }

    public int? DurationMin { get; set; }

    public int? DurationMax { get; set; }

    public Guid? UserId { get; set; }

    public Guid? ArticleId { get; set; }

    public GetArticleViewsInputBase()
    {
    }
}