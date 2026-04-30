using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.ArticleCategories;

namespace KHHub.MasterDataService.Data.ArticleCategories;

public partial interface IArticleCategoryRepository : IRepository<ArticleCategory, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, string? thumbnailUrl = null, CancellationToken cancellationToken = default);
    Task<List<ArticleCategory>> GetListAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, string? thumbnailUrl = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? name = null, string? slug = null, string? description = null, string? icon = null, Guid? parentId = null, int? displayOrderMin = null, int? displayOrderMax = null, bool? isActive = null, string? thumbnailUrl = null, CancellationToken cancellationToken = default);
}