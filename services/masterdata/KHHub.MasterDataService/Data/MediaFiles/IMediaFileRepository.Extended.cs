using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.MediaFiles;

namespace KHHub.MasterDataService.Data.MediaFiles;

public partial interface IMediaFileRepository
{
    Task<long> GetExplorerChildrenCountAsync(
        string? normalizedParentFolder,
        string? filterTextNormalized,
        CancellationToken cancellationToken = default);

    Task<List<MediaFile>> GetExplorerChildrenPagedAsync(
        string? normalizedParentFolder,
        string? filterTextNormalized,
        string? sorting,
        int skipCount,
        int maxResultCount,
        CancellationToken cancellationToken = default);

    Task<List<MediaFile>> GetSubtreeByPathPrefixDescendingAsync(
        string pathPrefix,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsExplorerDisplayNameAsync(
        string? normalizedParentFolder,
        string displayName,
        Guid? excludingId = null,
        CancellationToken cancellationToken = default);
}
