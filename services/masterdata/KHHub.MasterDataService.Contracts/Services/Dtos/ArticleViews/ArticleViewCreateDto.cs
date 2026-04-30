using KHHub.MasterDataService.Entities.ArticleViews;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.ArticleViews;

public abstract class ArticleViewCreateDtoBase
{
    [StringLength(ArticleViewConsts.IpAddressMaxLength)]
    public string? IpAddress { get; set; }

    [StringLength(ArticleViewConsts.DeviceMaxLength)]
    public string? Device { get; set; }

    [StringLength(ArticleViewConsts.SourceMaxLength)]
    public string? Source { get; set; }

    public DateTime ViewedAt { get; set; }

    public int Duration { get; set; }

    public Guid UserId { get; set; }

    public Guid ArticleId { get; set; }
}