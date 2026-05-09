using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.JobViews;

namespace KHHub.MasterDataService.Entities.JobViews;

public abstract class JobViewManagerBase : DomainService
{
    protected IJobViewRepository _jobViewRepository;

    public JobViewManagerBase(IJobViewRepository jobViewRepository)
    {
        _jobViewRepository = jobViewRepository;
    }

    public virtual async Task<JobView> CreateAsync(Guid jobId, DateTime viewedAt, int duration, Guid? userId = null, string? ipAddress = null, string? device = null, string? source = null)
    {
        Check.NotNull(jobId, nameof(jobId));
        Check.NotNull(viewedAt, nameof(viewedAt));
        Check.Length(ipAddress, nameof(ipAddress), JobViewConsts.IpAddressMaxLength);
        Check.Length(device, nameof(device), JobViewConsts.DeviceMaxLength);
        Check.Length(source, nameof(source), JobViewConsts.SourceMaxLength);
        var jobView = new JobView(GuidGenerator.Create(), jobId, viewedAt, duration, userId, ipAddress, device, source);
        return await _jobViewRepository.InsertAsync(jobView);
    }

    public virtual async Task<JobView> UpdateAsync(Guid id, Guid jobId, DateTime viewedAt, int duration, Guid? userId = null, string? ipAddress = null, string? device = null, string? source = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(jobId, nameof(jobId));
        Check.NotNull(viewedAt, nameof(viewedAt));
        Check.Length(ipAddress, nameof(ipAddress), JobViewConsts.IpAddressMaxLength);
        Check.Length(device, nameof(device), JobViewConsts.DeviceMaxLength);
        Check.Length(source, nameof(source), JobViewConsts.SourceMaxLength);
        var jobView = await _jobViewRepository.GetAsync(id);
        jobView.JobId = jobId;
        jobView.ViewedAt = viewedAt;
        jobView.Duration = duration;
        jobView.UserId = userId;
        jobView.IpAddress = ipAddress;
        jobView.Device = device;
        jobView.Source = source;
        jobView.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobViewRepository.UpdateAsync(jobView);
    }
}