using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.ArticleTags;

namespace KHHub.MasterDataService.Data.ArticleTags;

public partial interface IArticleTagRepository : IRepository<ArticleTag, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, CancellationToken cancellationToken = default);
    Task<List<ArticleTag>> GetListAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, int? usageCountMin = null, int? usageCountMax = null, CancellationToken cancellationToken = default);
}