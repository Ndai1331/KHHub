using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.Wards;

namespace KHHub.MasterDataService.Entities.Wards;

public abstract class WardManagerBase : DomainService
{
    protected IWardRepository _wardRepository;

    public WardManagerBase(IWardRepository wardRepository)
    {
        _wardRepository = wardRepository;
    }

    public virtual async Task<Ward> CreateAsync(Guid provinceId, string code, string name)
    {
        Check.NotNull(provinceId, nameof(provinceId));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.Length(code, nameof(code), WardConsts.CodeMaxLength);
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), WardConsts.NameMaxLength);
        var ward = new Ward(GuidGenerator.Create(), provinceId, code, name);
        return await _wardRepository.InsertAsync(ward);
    }

    public virtual async Task<Ward> UpdateAsync(Guid id, Guid provinceId, string code, string name, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(provinceId, nameof(provinceId));
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.Length(code, nameof(code), WardConsts.CodeMaxLength);
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), WardConsts.NameMaxLength);
        var ward = await _wardRepository.GetAsync(id);
        ward.ProvinceId = provinceId;
        ward.Code = code;
        ward.Name = name;
        ward.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _wardRepository.UpdateAsync(ward);
    }
}