using System.Security.Cryptography;
using System.Text;
using System.Linq;

namespace KHHub.Publish_website.Services;

/// <summary>
/// Ward badge fill color: rule "#" + wardCode + "0", then first 6 hex digits for #RRGGBB (text uses white on top).
/// Falls back to SHA-256 of the seed when there are not enough hex digits.
/// </summary>
public static class WardBadgeFormat
{
    public static string ForegroundHex(string? wardCode, string? wardNameFallback = null)
    {
        var raw = !string.IsNullOrWhiteSpace(wardCode)
            ? wardCode.Trim()
            : (wardNameFallback ?? string.Empty).Trim();

        if (raw.Length == 0)
        {
            return "#334155";
        }

        var combined = "#" + raw + "0";
        var hexChars = string.Concat(combined.Where(Uri.IsHexDigit).Select(char.ToLowerInvariant));
        if (hexChars.Length >= 6)
        {
            return "#" + hexChars[..6];
        }

        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(raw)))[..6].ToLowerInvariant();
        return "#" + hash;
    }
}
