using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.ArticleViews;

namespace KHHub.MasterDataService.Entities.ArticleViews;

public abstract class ArticleViewManagerBase : DomainService
{
    protected IArticleViewRepository _articleViewRepository;

    public ArticleViewManagerBase(IArticleViewRepository articleViewRepository)
    {
        _articleViewRepository = articleViewRepository;
    }

    public virtual async Task<ArticleView> CreateAsync(Guid articleId, DateTime viewedAt, int duration, Guid userId, string? ipAddress = null, string? device = null, string? source = null)
    {
        Check.NotNull(articleId, nameof(articleId));
        Check.NotNull(viewedAt, nameof(viewedAt));
        Check.Length(ipAddress, nameof(ipAddress), ArticleViewConsts.IpAddressMaxLength);
        Check.Length(device, nameof(device), ArticleViewConsts.DeviceMaxLength);
        Check.Length(source, nameof(source), ArticleViewConsts.SourceMaxLength);
        var articleView = new ArticleView(GuidGenerator.Create(), articleId, viewedAt, duration, userId, ipAddress, device, source);
        return await _articleViewRepository.InsertAsync(articleView);
    }

    public virtual async Task<ArticleView> UpdateAsync(Guid id, Guid articleId, DateTime viewedAt, int duration, Guid userId, string? ipAddress = null, string? device = null, string? source = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(articleId, nameof(articleId));
        Check.NotNull(viewedAt, nameof(viewedAt));
        Check.Length(ipAddress, nameof(ipAddress), ArticleViewConsts.IpAddressMaxLength);
        Check.Length(device, nameof(device), ArticleViewConsts.DeviceMaxLength);
        Check.Length(source, nameof(source), ArticleViewConsts.SourceMaxLength);
        var articleView = await _articleViewRepository.GetAsync(id);
        articleView.ArticleId = articleId;
        articleView.ViewedAt = viewedAt;
        articleView.Duration = duration;
        articleView.UserId = userId;
        articleView.IpAddress = ipAddress;
        articleView.Device = device;
        articleView.Source = source;
        articleView.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _articleViewRepository.UpdateAsync(articleView);
    }
}