using KHHub.MasterDataService.Entities.JobApplications;
using KHHub.MasterDataService.Entities.JobApplications;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.JobApplications;

public abstract class JobApplicationCreateDtoBase
{
    public Guid? UserId { get; set; }

    [Required]
    [StringLength(JobApplicationConsts.FullNameMaxLength)]
    public string FullName { get; set; } = null!;
    [EmailAddress]
    public string? Email { get; set; }

    [StringLength(JobApplicationConsts.PhoneNumberMaxLength)]
    public string? PhoneNumber { get; set; }

    [StringLength(JobApplicationConsts.CvUrlMaxLength)]
    public string? CvUrl { get; set; }

    [StringLength(JobApplicationConsts.PortfolioUrlMaxLength)]
    public string? PortfolioUrl { get; set; }

    public DateTime AppliedAt { get; set; }

    public ApplicationStatus Status { get; set; } = ((ApplicationStatus[])Enum.GetValues(typeof(ApplicationStatus)))[0];
    public Guid JobId { get; set; }
}