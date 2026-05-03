using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using KHHub.MasterDataService.Data;
using KHHub.MasterDataService.Entities.MediaFiles;

namespace KHHub.MasterDataService.Data.MediaFiles;

public class EfCoreMediaFileRepository : EfCoreMediaFileRepositoryBase, IMediaFileRepository
{
    public EfCoreMediaFileRepository(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public virtual async Task<long> GetExplorerChildrenCountAsync(
        string? normalizedParentFolder,
        string? filterTextNormalized,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyExplorerChildrenFilter(await GetQueryableAsync(), normalizedParentFolder, filterTextNormalized);
        return await query.LongCountAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<MediaFile>> GetExplorerChildrenPagedAsync(
        string? normalizedParentFolder,
        string? filterTextNormalized,
        string? sorting,
        int skipCount,
        int maxResultCount,
        CancellationToken cancellationToken = default)
    {
        var query = ApplyExplorerChildrenFilter(await GetQueryableAsync(), normalizedParentFolder, filterTextNormalized);
        query = ApplyExplorerSort(query, sorting);

        return await query
            .Skip(skipCount)
            .Take(maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<List<MediaFile>> GetSubtreeByPathPrefixDescendingAsync(
        string pathPrefix,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(pathPrefix);

        var query = await GetQueryableAsync();

        query = query.Where(e =>
            e.Path != null &&
            (e.Path == pathPrefix || e.Path.StartsWith(pathPrefix + "/")));

        return await query.OrderByDescending(e => e.Path!.Length).ToListAsync(GetCancellationToken(cancellationToken));
    }

    public virtual async Task<bool> ExistsExplorerDisplayNameAsync(
        string? normalizedParentFolder,
        string displayName,
        Guid? excludingId = null,
        CancellationToken cancellationToken = default)
    {
        var key = displayName.Trim().ToLowerInvariant();

        var query = ApplyExplorerChildrenFilter(await GetQueryableAsync(), normalizedParentFolder, null)
            .Where(e =>
                e.OriginalFileName != null && e.OriginalFileName.Trim().ToLower() == key);

        query = excludingId.HasValue ? query.Where(e => e.Id != excludingId.Value) : query;

        return await query.AnyAsync(GetCancellationToken(cancellationToken));
    }

    private static IQueryable<MediaFile> ApplyExplorerChildrenFilter(
        IQueryable<MediaFile> query,
        string? normalizedParentFolder,
        string? filterTextNormalized)
    {
        query = query.Where(x => x.Status != FileStatus.Deleted);

        query = normalizedParentFolder == null
            ? query.Where(x => x.Folder == null || x.Folder == "")
            : query.Where(x => x.Folder == normalizedParentFolder);

        query = MasterDataStringSearch.NormalizeForContains(filterTextNormalized) switch
        {
            null => query,
            var nf =>
                query.Where(x =>
                    (x.OriginalFileName != null && x.OriginalFileName.ToLower().Contains(nf))
                    || (x.FileName != null && x.FileName.ToLower().Contains(nf)))
        };

        return query;
    }

    private static IQueryable<MediaFile> ApplyExplorerSort(IQueryable<MediaFile> query, string? sorting)
    {
        if (!string.IsNullOrWhiteSpace(sorting) &&
            sorting!.Contains("originalFileName", StringComparison.OrdinalIgnoreCase))
        {
            return query
                .OrderBy(e => e.FileType == FileType.Folder ? 0 : 1)
                .ThenBy(e => e.OriginalFileName);
        }

        return query
            .OrderBy(e => e.FileType == FileType.Folder ? 0 : 1)
            .ThenByDescending(e => e.LastModificationTime);
    }
}
