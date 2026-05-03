using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.PlaceCategories;

namespace KHHub.MasterDataService.Entities.PlaceCategories;

public abstract class PlaceCategoryManagerBase : DomainService
{
    protected IPlaceCategoryRepository _placeCategoryRepository;

    public PlaceCategoryManagerBase(IPlaceCategoryRepository placeCategoryRepository)
    {
        _placeCategoryRepository = placeCategoryRepository;
    }

    public virtual async Task<PlaceCategory> CreateAsync(string name, string slug, int displayOrder, bool isActive, string? description = null, string? icon = null, string? color = null, Guid? parentId = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), PlaceCategoryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceCategoryConsts.SlugMaxLength);
        Check.Length(description, nameof(description), PlaceCategoryConsts.DescriptionMaxLength);
        Check.Length(icon, nameof(icon), PlaceCategoryConsts.IconMaxLength);
        Check.Length(color, nameof(color), PlaceCategoryConsts.ColorMaxLength);
        var placeCategory = new PlaceCategory(GuidGenerator.Create(), name, slug, displayOrder, isActive, description, icon, color, parentId);
        return await _placeCategoryRepository.InsertAsync(placeCategory);
    }

    public virtual async Task<PlaceCategory> UpdateAsync(Guid id, string name, string slug, int displayOrder, bool isActive, string? description = null, string? icon = null, string? color = null, Guid? parentId = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), PlaceCategoryConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceCategoryConsts.SlugMaxLength);
        Check.Length(description, nameof(description), PlaceCategoryConsts.DescriptionMaxLength);
        Check.Length(icon, nameof(icon), PlaceCategoryConsts.IconMaxLength);
        Check.Length(color, nameof(color), PlaceCategoryConsts.ColorMaxLength);
        var placeCategory = await _placeCategoryRepository.GetAsync(id);
        placeCategory.Name = name;
        placeCategory.Slug = slug;
        placeCategory.DisplayOrder = displayOrder;
        placeCategory.IsActive = isActive;
        placeCategory.Description = description;
        placeCategory.Icon = icon;
        placeCategory.Color = color;
        placeCategory.ParentId = parentId;
        placeCategory.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeCategoryRepository.UpdateAsync(placeCategory);
    }
}