using KHHub.MasterDataService.Entities.JobApplications;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.JobApplications;

public abstract class JobApplicationExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public Guid? UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CvUrl { get; set; }

    public string? PortfolioUrl { get; set; }

    public DateTime? AppliedAtMin { get; set; }

    public DateTime? AppliedAtMax { get; set; }

    public ApplicationStatus? Status { get; set; }

    public Guid? JobId { get; set; }

    public JobApplicationExcelDownloadDtoBase()
    {
    }
}