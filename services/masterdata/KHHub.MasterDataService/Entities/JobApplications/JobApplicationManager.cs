using KHHub.MasterDataService.Entities.JobApplications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.JobApplications;

namespace KHHub.MasterDataService.Entities.JobApplications;

public abstract class JobApplicationManagerBase : DomainService
{
    protected IJobApplicationRepository _jobApplicationRepository;

    public JobApplicationManagerBase(IJobApplicationRepository jobApplicationRepository)
    {
        _jobApplicationRepository = jobApplicationRepository;
    }

    public virtual async Task<JobApplication> CreateAsync(Guid jobId, string fullName, DateTime appliedAt, ApplicationStatus status, Guid? userId = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null)
    {
        Check.NotNull(jobId, nameof(jobId));
        Check.NotNullOrWhiteSpace(fullName, nameof(fullName));
        Check.Length(fullName, nameof(fullName), JobApplicationConsts.FullNameMaxLength);
        Check.NotNull(appliedAt, nameof(appliedAt));
        Check.NotNull(status, nameof(status));
        Check.Length(phoneNumber, nameof(phoneNumber), JobApplicationConsts.PhoneNumberMaxLength);
        Check.Length(cvUrl, nameof(cvUrl), JobApplicationConsts.CvUrlMaxLength);
        Check.Length(portfolioUrl, nameof(portfolioUrl), JobApplicationConsts.PortfolioUrlMaxLength);
        var jobApplication = new JobApplication(GuidGenerator.Create(), jobId, fullName, appliedAt, status, userId, email, phoneNumber, cvUrl, portfolioUrl);
        return await _jobApplicationRepository.InsertAsync(jobApplication);
    }

    public virtual async Task<JobApplication> UpdateAsync(Guid id, Guid jobId, string fullName, DateTime appliedAt, ApplicationStatus status, Guid? userId = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(jobId, nameof(jobId));
        Check.NotNullOrWhiteSpace(fullName, nameof(fullName));
        Check.Length(fullName, nameof(fullName), JobApplicationConsts.FullNameMaxLength);
        Check.NotNull(appliedAt, nameof(appliedAt));
        Check.NotNull(status, nameof(status));
        Check.Length(phoneNumber, nameof(phoneNumber), JobApplicationConsts.PhoneNumberMaxLength);
        Check.Length(cvUrl, nameof(cvUrl), JobApplicationConsts.CvUrlMaxLength);
        Check.Length(portfolioUrl, nameof(portfolioUrl), JobApplicationConsts.PortfolioUrlMaxLength);
        var jobApplication = await _jobApplicationRepository.GetAsync(id);
        jobApplication.JobId = jobId;
        jobApplication.FullName = fullName;
        jobApplication.AppliedAt = appliedAt;
        jobApplication.Status = status;
        jobApplication.UserId = userId;
        jobApplication.Email = email;
        jobApplication.PhoneNumber = phoneNumber;
        jobApplication.CvUrl = cvUrl;
        jobApplication.PortfolioUrl = portfolioUrl;
        jobApplication.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobApplicationRepository.UpdateAsync(jobApplication);
    }
}