using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

public abstract class ArticleTagMappingCreateDtoBase
{
    public bool IsPrimary { get; set; }

    public int Order { get; set; } = 1;
    public Guid ArticleTagId { get; set; }

    public Guid ArticleId { get; set; }
}