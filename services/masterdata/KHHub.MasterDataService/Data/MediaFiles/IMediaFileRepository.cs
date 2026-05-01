using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.MediaFiles;

namespace KHHub.MasterDataService.Data.MediaFiles;

public partial interface IMediaFileRepository : IRepository<MediaFile, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null, CancellationToken cancellationToken = default);
    Task<List<MediaFile>> GetListAsync(string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, int? widthMin = null, int? widthMax = null, int? heightMin = null, int? heightMax = null, FileType? fileType = null, FileStatus? status = null, CancellationToken cancellationToken = default);
}