using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.ArticleTags;

public abstract class ArticleTagBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    [NotNull]
    public virtual string Slug { get; set; }

    [CanBeNull]
    public virtual string? Description { get; set; }

    public virtual int UsageCount { get; set; }

    protected ArticleTagBase()
    {
    }

    public ArticleTagBase(Guid id, string name, string slug, int usageCount, string? description = null)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), ArticleTagConsts.NameMaxLength, 0);
        Check.NotNull(slug, nameof(slug));
        Check.Length(slug, nameof(slug), ArticleTagConsts.SlugMaxLength, 0);
        Name = name;
        Slug = slug;
        UsageCount = usageCount;
        Description = description;
    }
}