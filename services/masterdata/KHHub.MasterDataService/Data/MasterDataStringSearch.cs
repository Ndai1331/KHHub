namespace KHHub.MasterDataService.Data;

/// <summary>
/// Normalizes user-provided search text for case-insensitive, whitespace-tolerant matching in repositories.
/// </summary>
internal static class MasterDataStringSearch
{
    /// <summary>
    /// Returns null if the value is null, whitespace-only, or empty after trim;
    /// otherwise returns trim + invariant lower case for use with column.ToLower().Contains(...).
    /// </summary>
    public static string? NormalizeForContains(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var trimmed = value.Trim();
        return trimmed.Length == 0 ? null : trimmed.ToLowerInvariant();
    }
}
