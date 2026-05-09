using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.JobFavorites;

namespace KHHub.MasterDataService.Entities.JobFavorites;

public abstract class JobFavoriteManagerBase : DomainService
{
    protected IJobFavoriteRepository _jobFavoriteRepository;

    public JobFavoriteManagerBase(IJobFavoriteRepository jobFavoriteRepository)
    {
        _jobFavoriteRepository = jobFavoriteRepository;
    }

    public virtual async Task<JobFavorite> CreateAsync(Guid jobId, Guid userId)
    {
        Check.NotNull(jobId, nameof(jobId));
        var jobFavorite = new JobFavorite(GuidGenerator.Create(), jobId, userId);
        return await _jobFavoriteRepository.InsertAsync(jobFavorite);
    }

    public virtual async Task<JobFavorite> UpdateAsync(Guid id, Guid jobId, Guid userId, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(jobId, nameof(jobId));
        var jobFavorite = await _jobFavoriteRepository.GetAsync(id);
        jobFavorite.JobId = jobId;
        jobFavorite.UserId = userId;
        jobFavorite.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobFavoriteRepository.UpdateAsync(jobFavorite);
    }
}