using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace KHHub.CrawlerSerivce.Crawling.Sites.Articles.BaoKhanhHoa;

/// <summary>
/// Derives display category names from baokhanhhoa.vn article URLs (segments before YYYYMM folder).
/// </summary>
public static class BaoKhanhHoaUriCategoryExtractor
{
    private static readonly Regex YyyyMmSegment = new(
        @"^(?:19|20)\d{4}$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant);

    private static readonly Dictionary<string, string> SlugToDisplay =
        new(StringComparer.OrdinalIgnoreCase)
        {
            ["xa-hoi"] = "Xã hội",
            ["kinh-te"] = "Kinh tế",
            ["chinh-tri"] = "Chính trị",
            ["doi-ngoai"] = "Đối ngoại",
            ["van-hoa"] = "Văn hóa",
            ["the-thao"] = "Thể thao",
            ["doi-song"] = "Đời sống",
            ["du-lich"] = "Du lịch",
            ["phap-luat"] = "Pháp luật",
            ["khoa-hoc-cong-nghe"] = "Khoa học - Công nghệ",
            ["the-gioi"] = "Thế giới",
            ["tin-dia-phuong"] = "Tin địa phương",
            ["ban-doc"] = "Bạn đọc",
            ["giao-duc"] = "Giáo dục",
            ["multimedia"] = "Multimedia",
            ["video"] = "Video",
            ["podcast"] = "Podcast",
            ["phong-su"] = "Phóng sự",
            ["tin-noi-bat-trang-chu"] = "Tin nổi bật trang chủ",
            ["chung-tay-cai-cach-hanh-chinh"] = "Chung tay cải cách hành chính",
            ["chuyen-doi-so"] = "Chuyển đổi số",
            ["thong-tin-quang-cao"] = "Thông tin - Quảng cáo",
            ["goc-review"] = "Góc Review",
            ["hoat-dong-lanh-dao-tinh"] = "Hoạt động lãnh đạo tỉnh"
        };

    /// <summary>
    /// Returns the most specific category display name and parent category names (immediate parent first).
    /// </summary>
    public static bool TryExtractCategoryPath(Uri articleUri, out string? primaryDisplayName, out List<string> parentCategoryDisplayNamesOrdered)
    {
        primaryDisplayName = null;
        parentCategoryDisplayNamesOrdered = new List<string>();

        var path = articleUri.AbsolutePath.TrimEnd('/');
        if (string.IsNullOrEmpty(path))
        {
            return false;
        }

        var segments = path.Split('/', StringSplitOptions.RemoveEmptyEntries);
        if (segments.Length == 0)
        {
            return false;
        }

        var dateIndex = Array.FindIndex(segments, s => YyyyMmSegment.IsMatch(s));
        if (dateIndex <= 0)
        {
            return false;
        }

        primaryDisplayName = SlugToDisplayName(segments[dateIndex - 1]);
        for (var i = dateIndex - 2; i >= 0; i--)
        {
            parentCategoryDisplayNamesOrdered.Add(SlugToDisplayName(segments[i]));
        }

        return true;
    }

    public static string SlugToDisplayName(string slug)
    {
        if (string.IsNullOrWhiteSpace(slug))
        {
            return string.Empty;
        }

        var key = slug.Trim();
        if (SlugToDisplay.TryGetValue(key, out var mapped))
        {
            return mapped;
        }

        var parts = key.Split('-', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length == 0)
        {
            return key;
        }

        return string.Join(' ', parts.Select(static p =>
            p.Length == 0
                ? p
                : char.ToUpperInvariant(p[0]) + (p.Length > 1 ? p[1..].ToLowerInvariant() : string.Empty)));
    }
}
