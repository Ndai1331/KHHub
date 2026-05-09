using KHHub.MasterDataService.Entities.JobApplications;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.JobApplications;

public abstract class JobApplicationDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public Guid? UserId { get; set; }

    public string FullName { get; set; } = null!;
    public string? Email { get; set; }

    public string? PhoneNumber { get; set; }

    public string? CvUrl { get; set; }

    public string? PortfolioUrl { get; set; }

    public DateTime AppliedAt { get; set; }

    public ApplicationStatus Status { get; set; }

    public Guid JobId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}