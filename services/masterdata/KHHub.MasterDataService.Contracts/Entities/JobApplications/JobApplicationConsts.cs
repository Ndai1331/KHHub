namespace KHHub.MasterDataService.Entities.JobApplications;

public static class JobApplicationConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "JobApplication." : string.Empty);
    }

    public const int FullNameMaxLength = 255;
    public const int PhoneNumberMaxLength = 20;
    public const int CvUrlMaxLength = 1000;
    public const int PortfolioUrlMaxLength = 1000;
}