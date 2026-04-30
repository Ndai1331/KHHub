using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.ArticleTagMappings;

namespace KHHub.MasterDataService.Data.ArticleTagMappings;

public partial interface IArticleTagMappingRepository : IRepository<ArticleTagMapping, Guid>
{
    Task DeleteAllAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null, CancellationToken cancellationToken = default);
    Task<ArticleTagMappingWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ArticleTagMappingWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<ArticleTagMapping>> GetListAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, bool? isPrimary = null, int? orderMin = null, int? orderMax = null, Guid? articleTagId = null, Guid? articleId = null, CancellationToken cancellationToken = default);
}