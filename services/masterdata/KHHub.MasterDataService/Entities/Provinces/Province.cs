using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.Provinces;

public abstract class ProvinceBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Code { get; set; }

    [NotNull]
    public virtual string Name { get; set; }

    protected ProvinceBase()
    {
    }

    public ProvinceBase(Guid id, string code, string name)
    {
        Id = id;
        Check.NotNull(code, nameof(code));
        Check.Length(code, nameof(code), ProvinceConsts.CodeMaxLength, 0);
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), ProvinceConsts.NameMaxLength, 0);
        Code = code;
        Name = name;
    }
}