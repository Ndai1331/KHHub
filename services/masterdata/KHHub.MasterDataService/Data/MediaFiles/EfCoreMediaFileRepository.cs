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
        var nf = MasterDataStringSearch.NormalizeForContains(filterText);
        var nFileName = MasterDataStringSearch.NormalizeForContains(fileName);
        var nOriginalFileName = MasterDataStringSearch.NormalizeForContains(originalFileName);
        var nExtension = MasterDataStringSearch.NormalizeForContains(extension);
        var nContentType = MasterDataStringSearch.NormalizeForContains(contentType);
        var nStorageProvider = MasterDataStringSearch.NormalizeForContains(storageProvider);
        var nBucket = MasterDataStringSearch.NormalizeForContains(bucket);
        var nFolder = MasterDataStringSearch.NormalizeForContains(folder);
        var nPath = MasterDataStringSearch.NormalizeForContains(path);
        var nUrl = MasterDataStringSearch.NormalizeForContains(url);
        var nChecksum = MasterDataStringSearch.NormalizeForContains(checksum);
        return query.WhereIf(nf != null, e => (e.FileName != null && e.FileName.ToLower().Contains(nf!)) || (e.OriginalFileName != null && e.OriginalFileName.ToLower().Contains(nf!)) || (e.Extension != null && e.Extension.ToLower().Contains(nf!)) || (e.ContentType != null && e.ContentType.ToLower().Contains(nf!)) || (e.StorageProvider != null && e.StorageProvider.ToLower().Contains(nf!)) || (e.Bucket != null && e.Bucket.ToLower().Contains(nf!)) || (e.Folder != null && e.Folder.ToLower().Contains(nf!)) || (e.Path != null && e.Path.ToLower().Contains(nf!)) || (e.Url != null && e.Url.ToLower().Contains(nf!)) || (e.Checksum != null && e.Checksum.ToLower().Contains(nf!))).WhereIf(nFileName != null, e => e.FileName != null && e.FileName.ToLower().Contains(nFileName!)).WhereIf(nOriginalFileName != null, e => e.OriginalFileName != null && e.OriginalFileName.ToLower().Contains(nOriginalFileName!)).WhereIf(nExtension != null, e => e.Extension != null && e.Extension.ToLower().Contains(nExtension!)).WhereIf(nContentType != null, e => e.ContentType != null && e.ContentType.ToLower().Contains(nContentType!)).WhereIf(nStorageProvider != null, e => e.StorageProvider != null && e.StorageProvider.ToLower().Contains(nStorageProvider!)).WhereIf(nBucket != null, e => e.Bucket != null && e.Bucket.ToLower().Contains(nBucket!)).WhereIf(nFolder != null, e => e.Folder != null && e.Folder.ToLower().Contains(nFolder!)).WhereIf(nPath != null, e => e.Path != null && e.Path.ToLower().Contains(nPath!)).WhereIf(nUrl != null, e => e.Url != null && e.Url.ToLower().Contains(nUrl!)).WhereIf(nChecksum != null, e => e.Checksum != null && e.Checksum.ToLower().Contains(nChecksum!)).WhereIf(widthMin.HasValue, e => e.Width >= widthMin!.Value).WhereIf(widthMax.HasValue, e => e.Width <= widthMax!.Value).WhereIf(heightMin.HasValue, e => e.Height >= heightMin!.Value).WhereIf(heightMax.HasValue, e => e.Height <= heightMax!.Value).WhereIf(fileType.HasValue, e => e.FileType == fileType).WhereIf(status.HasValue, e => e.Status == status);
    }
}