using KHHub.MasterDataService.Entities.JobApplications;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.JobApplications;

public abstract class JobApplicationBase : FullAuditedAggregateRoot<Guid>
{
    public virtual Guid? UserId { get; set; }

    [NotNull]
    public virtual string FullName { get; set; }

    [CanBeNull]
    public virtual string? Email { get; set; }

    [CanBeNull]
    public virtual string? PhoneNumber { get; set; }

    [CanBeNull]
    public virtual string? CvUrl { get; set; }

    [CanBeNull]
    public virtual string? PortfolioUrl { get; set; }

    public virtual DateTime AppliedAt { get; set; }

    public virtual ApplicationStatus Status { get; set; }

    public Guid JobId { get; set; }

    protected JobApplicationBase()
    {
    }

    public JobApplicationBase(Guid id, Guid jobId, string fullName, DateTime appliedAt, ApplicationStatus status, Guid? userId = null, string? email = null, string? phoneNumber = null, string? cvUrl = null, string? portfolioUrl = null)
    {
        Id = id;
        Check.NotNull(fullName, nameof(fullName));
        Check.Length(fullName, nameof(fullName), JobApplicationConsts.FullNameMaxLength, 0);
        Check.Length(phoneNumber, nameof(phoneNumber), JobApplicationConsts.PhoneNumberMaxLength, 0);
        Check.Length(cvUrl, nameof(cvUrl), JobApplicationConsts.CvUrlMaxLength, 0);
        Check.Length(portfolioUrl, nameof(portfolioUrl), JobApplicationConsts.PortfolioUrlMaxLength, 0);
        FullName = fullName;
        AppliedAt = appliedAt;
        Status = status;
        UserId = userId;
        Email = email;
        PhoneNumber = phoneNumber;
        CvUrl = cvUrl;
        PortfolioUrl = portfolioUrl;
        JobId = jobId;
    }
}