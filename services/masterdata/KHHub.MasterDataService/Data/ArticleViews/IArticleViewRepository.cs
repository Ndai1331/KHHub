using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Entities.ArticleViews;

namespace KHHub.MasterDataService.Data.ArticleViews;

public partial interface IArticleViewRepository : IRepository<ArticleView, Guid>
{
    Task DeleteAllAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null, CancellationToken cancellationToken = default);
    Task<ArticleViewWithNavigationProperties> GetWithNavigationPropertiesAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<ArticleViewWithNavigationProperties>> GetListWithNavigationPropertiesAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<List<ArticleView>> GetListAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, string? sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, CancellationToken cancellationToken = default);
    Task<long> GetCountAsync(string? filterText = null, string? ipAddress = null, string? device = null, string? source = null, DateTime? viewedAtMin = null, DateTime? viewedAtMax = null, int? durationMin = null, int? durationMax = null, Guid? userId = null, Guid? articleId = null, CancellationToken cancellationToken = default);
}