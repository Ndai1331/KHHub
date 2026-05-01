using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.PlaceViews;

namespace KHHub.MasterDataService.Entities.PlaceViews;

public abstract class PlaceViewManagerBase : DomainService
{
    protected IPlaceViewRepository _placeViewRepository;

    public PlaceViewManagerBase(IPlaceViewRepository placeViewRepository)
    {
        _placeViewRepository = placeViewRepository;
    }

    public virtual async Task<PlaceView> CreateAsync(Guid placeId, Guid userId, DateTime viewedAt, int duration, string? ipAddress = null, string? device = null, string? source = null)
    {
        Check.NotNull(placeId, nameof(placeId));
        Check.NotNull(viewedAt, nameof(viewedAt));
        Check.Length(ipAddress, nameof(ipAddress), PlaceViewConsts.IpAddressMaxLength);
        Check.Length(device, nameof(device), PlaceViewConsts.DeviceMaxLength);
        Check.Length(source, nameof(source), PlaceViewConsts.SourceMaxLength);
        var placeView = new PlaceView(GuidGenerator.Create(), placeId, userId, viewedAt, duration, ipAddress, device, source);
        return await _placeViewRepository.InsertAsync(placeView);
    }

    public virtual async Task<PlaceView> UpdateAsync(Guid id, Guid placeId, Guid userId, DateTime viewedAt, int duration, string? ipAddress = null, string? device = null, string? source = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(placeId, nameof(placeId));
        Check.NotNull(viewedAt, nameof(viewedAt));
        Check.Length(ipAddress, nameof(ipAddress), PlaceViewConsts.IpAddressMaxLength);
        Check.Length(device, nameof(device), PlaceViewConsts.DeviceMaxLength);
        Check.Length(source, nameof(source), PlaceViewConsts.SourceMaxLength);
        var placeView = await _placeViewRepository.GetAsync(id);
        placeView.PlaceId = placeId;
        placeView.UserId = userId;
        placeView.ViewedAt = viewedAt;
        placeView.Duration = duration;
        placeView.IpAddress = ipAddress;
        placeView.Device = device;
        placeView.Source = source;
        placeView.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeViewRepository.UpdateAsync(placeView);
    }
}