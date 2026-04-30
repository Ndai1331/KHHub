using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Entities.Articles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.ArticleTagMappings;

public abstract class ArticleTagMappingBase : FullAuditedAggregateRoot<Guid>
{
    public virtual bool IsPrimary { get; set; }

    public virtual int Order { get; set; }

    public Guid ArticleTagId { get; set; }

    public Guid ArticleId { get; set; }

    protected ArticleTagMappingBase()
    {
    }

    public ArticleTagMappingBase(Guid id, Guid articleTagId, Guid articleId, bool isPrimary, int order)
    {
        Id = id;
        IsPrimary = isPrimary;
        Order = order;
        ArticleTagId = articleTagId;
        ArticleId = articleId;
    }
}