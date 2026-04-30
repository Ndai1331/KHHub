using KHHub.MasterDataService.Entities.Provinces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.Wards;

public abstract class WardBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Code { get; set; }

    [NotNull]
    public virtual string Name { get; set; }

    public Guid ProvinceId { get; set; }

    protected WardBase()
    {
    }

    public WardBase(Guid id, Guid provinceId, string code, string name)
    {
        Id = id;
        Check.NotNull(code, nameof(code));
        Check.Length(code, nameof(code), WardConsts.CodeMaxLength, 0);
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), WardConsts.NameMaxLength, 0);
        Code = code;
        Name = name;
        ProvinceId = provinceId;
    }
}