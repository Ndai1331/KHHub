using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.Provinces;

namespace KHHub.MasterDataService.Entities.Provinces;

public abstract class ProvinceManagerBase : DomainService
{
    protected IProvinceRepository _provinceRepository;

    public ProvinceManagerBase(IProvinceRepository provinceRepository)
    {
        _provinceRepository = provinceRepository;
    }

    public virtual async Task<Province> CreateAsync(string code, string name)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.Length(code, nameof(code), ProvinceConsts.CodeMaxLength);
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ProvinceConsts.NameMaxLength);
        var province = new Province(GuidGenerator.Create(), code, name);
        return await _provinceRepository.InsertAsync(province);
    }

    public virtual async Task<Province> UpdateAsync(Guid id, string code, string name, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(code, nameof(code));
        Check.Length(code, nameof(code), ProvinceConsts.CodeMaxLength);
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), ProvinceConsts.NameMaxLength);
        var province = await _provinceRepository.GetAsync(id);
        province.Code = code;
        province.Name = name;
        province.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _provinceRepository.UpdateAsync(province);
    }
}