using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.EntityFiles;

namespace KHHub.MasterDataService.Entities.EntityFiles;

public abstract class EntityFileManagerBase : DomainService
{
    protected IEntityFileRepository _entityFileRepository;

    public EntityFileManagerBase(IEntityFileRepository entityFileRepository)
    {
        _entityFileRepository = entityFileRepository;
    }

    public virtual async Task<EntityFile> CreateAsync(Guid mediaFileId, string entityType, Guid entityId, int sortOrder, bool isPrimary, string? collection = null)
    {
        Check.NotNull(mediaFileId, nameof(mediaFileId));
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));
        Check.Length(entityType, nameof(entityType), EntityFileConsts.EntityTypeMaxLength);
        Check.Length(collection, nameof(collection), EntityFileConsts.CollectionMaxLength);
        var entityFile = new EntityFile(GuidGenerator.Create(), mediaFileId, entityType, entityId, sortOrder, isPrimary, collection);
        return await _entityFileRepository.InsertAsync(entityFile);
    }

    public virtual async Task<EntityFile> UpdateAsync(Guid id, Guid mediaFileId, string entityType, Guid entityId, int sortOrder, bool isPrimary, string? collection = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(mediaFileId, nameof(mediaFileId));
        Check.NotNullOrWhiteSpace(entityType, nameof(entityType));
        Check.Length(entityType, nameof(entityType), EntityFileConsts.EntityTypeMaxLength);
        Check.Length(collection, nameof(collection), EntityFileConsts.CollectionMaxLength);
        var entityFile = await _entityFileRepository.GetAsync(id);
        entityFile.MediaFileId = mediaFileId;
        entityFile.EntityType = entityType;
        entityFile.EntityId = entityId;
        entityFile.SortOrder = sortOrder;
        entityFile.IsPrimary = isPrimary;
        entityFile.Collection = collection;
        entityFile.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _entityFileRepository.UpdateAsync(entityFile);
    }
}