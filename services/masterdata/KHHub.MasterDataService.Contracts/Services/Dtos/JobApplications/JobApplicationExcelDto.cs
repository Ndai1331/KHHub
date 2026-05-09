using KHHub.MasterDataService.Entities.JobApplications;
using System;

namespace KHHub.MasterDataService.Services.Dtos.JobApplications;

public abstract class JobApplicationExcelDtoBase
{
    public Guid? UserId { get; set; }

    public string FullName { get; set; } = null!;
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CvUrl { get; set; }

    public string? PortfolioUrl { get; set; }

    public DateTime AppliedAt { get; set; }

    public ApplicationStatus Status { get; set; }
}