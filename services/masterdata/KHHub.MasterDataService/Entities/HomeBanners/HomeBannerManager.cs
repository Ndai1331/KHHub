using KHHub.MasterDataService.Entities.HomeBanners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.HomeBanners;

namespace KHHub.MasterDataService.Entities.HomeBanners;

public abstract class HomeBannerManagerBase : DomainService
{
    protected IHomeBannerRepository _homeBannerRepository;

    public HomeBannerManagerBase(IHomeBannerRepository homeBannerRepository)
    {
        _homeBannerRepository = homeBannerRepository;
    }

    public virtual async Task<HomeBanner> CreateAsync(string title, string imageUrl, int sortOrder, bool isActive, string? subtitle = null, string? description = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, DateTime? startDate = null, DateTime? endDate = null)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), HomeBannerConsts.TitleMaxLength);
        Check.NotNullOrWhiteSpace(imageUrl, nameof(imageUrl));
        Check.Length(imageUrl, nameof(imageUrl), HomeBannerConsts.ImageUrlMaxLength);
        Check.Length(subtitle, nameof(subtitle), HomeBannerConsts.SubtitleMaxLength);
        Check.Length(mobileImageUrl, nameof(mobileImageUrl), HomeBannerConsts.MobileImageUrlMaxLength);
        Check.Length(buttonText, nameof(buttonText), HomeBannerConsts.ButtonTextMaxLength);
        Check.Length(buttonUrl, nameof(buttonUrl), HomeBannerConsts.ButtonUrlMaxLength);
        var homeBanner = new HomeBanner(GuidGenerator.Create(), title, imageUrl, sortOrder, isActive, subtitle, description, mobileImageUrl, buttonText, buttonUrl, targetType, targetId, startDate, endDate);
        return await _homeBannerRepository.InsertAsync(homeBanner);
    }

    public virtual async Task<HomeBanner> UpdateAsync(Guid id, string title, string imageUrl, int sortOrder, bool isActive, string? subtitle = null, string? description = null, string? mobileImageUrl = null, string? buttonText = null, string? buttonUrl = null, TargetType? targetType = null, Guid? targetId = null, DateTime? startDate = null, DateTime? endDate = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), HomeBannerConsts.TitleMaxLength);
        Check.NotNullOrWhiteSpace(imageUrl, nameof(imageUrl));
        Check.Length(imageUrl, nameof(imageUrl), HomeBannerConsts.ImageUrlMaxLength);
        Check.Length(subtitle, nameof(subtitle), HomeBannerConsts.SubtitleMaxLength);
        Check.Length(mobileImageUrl, nameof(mobileImageUrl), HomeBannerConsts.MobileImageUrlMaxLength);
        Check.Length(buttonText, nameof(buttonText), HomeBannerConsts.ButtonTextMaxLength);
        Check.Length(buttonUrl, nameof(buttonUrl), HomeBannerConsts.ButtonUrlMaxLength);
        var homeBanner = await _homeBannerRepository.GetAsync(id);
        homeBanner.Title = title;
        homeBanner.ImageUrl = imageUrl;
        homeBanner.SortOrder = sortOrder;
        homeBanner.IsActive = isActive;
        homeBanner.Subtitle = subtitle;
        homeBanner.Description = description;
        homeBanner.MobileImageUrl = mobileImageUrl;
        homeBanner.ButtonText = buttonText;
        homeBanner.ButtonUrl = buttonUrl;
        homeBanner.TargetType = targetType;
        homeBanner.TargetId = targetId;
        homeBanner.StartDate = startDate;
        homeBanner.EndDate = endDate;
        homeBanner.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _homeBannerRepository.UpdateAsync(homeBanner);
    }
}