using System;

namespace KHHub.Web.Media;

/// <summary>
/// Resolves stored media paths (e.g. /bucket/host/key) to a browser-loadable URL for &lt;img src&gt;.
/// Stored value stays a short path; preview uses storage origin + path (see MediaFilesStorage:PublicBaseUrl).
/// </summary>
public static class MediaPreviewUrl
{
    public static string ResolveReadUrl(string? storedValue, string? publicBaseUrl)
    {
        var s = (storedValue ?? string.Empty).Trim();
        if (string.IsNullOrEmpty(s))
        {
            return string.Empty;
        }

        if (s.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            s.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            return s;
        }

        // Protocol-relative URLs work as img src
        if (s.StartsWith("//", StringComparison.Ordinal))
        {
            return s;
        }

        var baseTrim = (publicBaseUrl ?? string.Empty).Trim().TrimEnd('/');
        if (string.IsNullOrEmpty(baseTrim) || !s.StartsWith('/'))
        {
            return s;
        }

        try
        {
            var u = new Uri(baseTrim);
            return u.GetLeftPart(UriPartial.Authority) + s;
        }
        catch (UriFormatException)
        {
            return s;
        }
    }
}
