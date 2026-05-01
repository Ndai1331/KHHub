using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.PlaceTags;

namespace KHHub.MasterDataService.Entities.PlaceTags;

public abstract class PlaceTagManagerBase : DomainService
{
    protected IPlaceTagRepository _placeTagRepository;

    public PlaceTagManagerBase(IPlaceTagRepository placeTagRepository)
    {
        _placeTagRepository = placeTagRepository;
    }

    public virtual async Task<PlaceTag> CreateAsync(string name, string slug, int usageCount, string? description = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), PlaceTagConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceTagConsts.SlugMaxLength);
        Check.Length(description, nameof(description), PlaceTagConsts.DescriptionMaxLength);
        var placeTag = new PlaceTag(GuidGenerator.Create(), name, slug, usageCount, description);
        return await _placeTagRepository.InsertAsync(placeTag);
    }

    public virtual async Task<PlaceTag> UpdateAsync(Guid id, string name, string slug, int usageCount, string? description = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), PlaceTagConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceTagConsts.SlugMaxLength);
        Check.Length(description, nameof(description), PlaceTagConsts.DescriptionMaxLength);
        var placeTag = await _placeTagRepository.GetAsync(id);
        placeTag.Name = name;
        placeTag.Slug = slug;
        placeTag.UsageCount = usageCount;
        placeTag.Description = description;
        placeTag.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeTagRepository.UpdateAsync(placeTag);
    }
}