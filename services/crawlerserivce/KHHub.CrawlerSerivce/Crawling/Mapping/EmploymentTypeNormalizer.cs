using System;
using KHHub.MasterDataService.Entities.Jobs;

namespace KHHub.CrawlerSerivce.Crawling.Mapping;

/// <summary>
/// Maps vendor-specific employment labels to MasterData <see cref="EmploymentType"/>.
/// </summary>
public static class EmploymentTypeNormalizer
{
    public static EmploymentType? TryMap(string? label)
    {
        if (string.IsNullOrWhiteSpace(label))
        {
            return null;
        }

        var x = label.Trim().ToLowerInvariant();

        if (x.Contains("full_time", StringComparison.Ordinal) ||
            x.Contains("full-time", StringComparison.Ordinal) ||
            x.Contains("toàn thời gian", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("toan thoi gian", StringComparison.OrdinalIgnoreCase))
        {
            return EmploymentType.FullTime;
        }

        if (x.Contains("part_time", StringComparison.Ordinal) ||
            x.Contains("part-time", StringComparison.Ordinal) ||
            x.Contains("bán thời gian", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("ban thoi gian", StringComparison.OrdinalIgnoreCase))
        {
            return EmploymentType.PartTime;
        }

        if (x.Contains("intern", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("thực tập", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("thuc tap", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("ctv", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("sinh viên", StringComparison.OrdinalIgnoreCase))
        {
            return EmploymentType.Internship;
        }

        if (x.Contains("freelance", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("tự do", StringComparison.OrdinalIgnoreCase))
        {
            return EmploymentType.Freelance;
        }

        if (x.Contains("contract", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("hợp đồng", StringComparison.OrdinalIgnoreCase) ||
            x.Contains("hop dong", StringComparison.OrdinalIgnoreCase))
        {
            return EmploymentType.Contract;
        }

        return null;
    }
}
