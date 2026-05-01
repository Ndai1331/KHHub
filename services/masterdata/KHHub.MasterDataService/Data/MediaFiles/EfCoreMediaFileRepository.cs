using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using KHHub.MasterDataService.Data;
using KHHub.MasterDataService.Entities.MediaFiles;

namespace KHHub.MasterDataService.Data.MediaFiles;

public abstract class EfCoreMediaFileRepositoryBase : EfCoreRepository<MasterDataServiceDbContext, MediaFile, Guid>
{
    public EfCoreMediaFileRepositoryBase(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task DeleteAllAsync(string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = await GetQueryableAsync();
        query = ApplyFilter(query, filterText, fileName, originalFileName, extension, contentType, storageProvider, bucket, folder, path, url, checksum, widthMin, widthMax, heightMin, heightMax, fileType, status);
        var ids = query.Select(x => x.Id);
        await DeleteManyAsync(ids, cancellationToken: GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<MediaFile>> GetListAsync(string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetQueryableAsync()), filterText, fileName, originalFileName, extension, contentType, storageProvider, bucket, folder, path, url, checksum, widthMin, widthMax, heightMin, heightMax, fileType, status);
        query = query.OrderBy(string.IsNullOrWhiteSpace(sorting) ? MediaFileConsts.GetDefaultSorting(false) : sorting);
        return await query.PageBy(skipCount, maxResultCount).ToListAsync(cancellationToken);
    }

    public virtual async Task<long> GetCountAsync(string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null, CancellationToken cancellationToken = default)
    {
        var query = ApplyFilter((await GetDbSetAsync()), filterText, fileName, originalFileName, extension, contentType, storageProvider, bucket, folder, path, url, checksum, widthMin, widthMax, heightMin, heightMax, fileType, status);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    protected virtual IQueryable<MediaFile> ApplyFilter(IQueryable<MediaFile> query, string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null)
    {
        return query.WhereIf(!string.IsNullOrWhiteSpace(filterText), e => e.FileName!.Contains(filterText!) || e.OriginalFileName!.Contains(filterText!) || e.Extension!.Contains(filterText!) || e.ContentType!.Contains(filterText!) || e.StorageProvider!.Contains(filterText!) || e.Bucket!.Contains(filterText!) || e.Folder!.Contains(filterText!) || e.Path!.Contains(filterText!) || e.Url!.Contains(filterText!) || e.Checksum!.Contains(filterText!)).WhereIf(!string.IsNullOrWhiteSpace(fileName), e => e.FileName.Contains(fileName)).WhereIf(!string.IsNullOrWhiteSpace(originalFileName), e => e.OriginalFileName.Contains(originalFileName)).WhereIf(!string.IsNullOrWhiteSpace(extension), e => e.Extension.Contains(extension)).WhereIf(!string.IsNullOrWhiteSpace(contentType), e => e.ContentType.Contains(contentType)).WhereIf(!string.IsNullOrWhiteSpace(storageProvider), e => e.StorageProvider.Contains(storageProvider)).WhereIf(!string.IsNullOrWhiteSpace(bucket), e => e.Bucket.Contains(bucket)).WhereIf(!string.IsNullOrWhiteSpace(folder), e => e.Folder.Contains(folder)).WhereIf(!string.IsNullOrWhiteSpace(path), e => e.Path.Contains(path)).WhereIf(!string.IsNullOrWhiteSpace(url), e => e.Url.Contains(url)).WhereIf(!string.IsNullOrWhiteSpace(checksum), e => e.Checksum.Contains(checksum)).WhereIf(widthMin.HasValue, e => e.Width >= widthMin!.Value).WhereIf(widthMax.HasValue, e => e.Width <= widthMax!.Value).WhereIf(heightMin.HasValue, e => e.Height >= heightMin!.Value).WhereIf(heightMax.HasValue, e => e.Height <= heightMax!.Value).WhereIf(fileType.HasValue, e => e.FileType == fileType).WhereIf(status.HasValue, e => e.Status == status);
    }
}