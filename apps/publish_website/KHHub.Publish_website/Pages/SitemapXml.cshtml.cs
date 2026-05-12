using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using KHHub.Publish_website.Seo;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace KHHub.Publish_website.Pages;

public class SitemapXmlModel : PageModel
{
    private readonly IConfiguration _configuration;
    private readonly IPublicContentCatalog _catalog;

    /// <summary>Default hub URLs (Vi + En list routes). Detail URLs are appended when <c>Seo:SitemapIncludeDetailUrls</c> is true.</summary>
    private static readonly string[] DefaultPaths =
    [
        "/",
        "/news",
        "/tin-tuc",
        "/jobs",
        "/viec-lam",
        "/places",
        "/dia-diem",
        "/gold-price",
        "/gia-vang"
    ];

    public SitemapXmlModel(IConfiguration configuration, IPublicContentCatalog catalog)
    {
        _configuration = configuration;
        _catalog = catalog;
    }

    public IActionResult OnGet()
    {
        var baseUrl = SeoUrls.ResolvePublicSiteBase(HttpContext.Request, _configuration).TrimEnd('/');
        var paths = _configuration.GetSection("Seo:SitemapPaths").Get<string[]>();
        if (paths is not { Length: > 0 })
        {
            paths = DefaultPaths;
        }

        var includeDetails = _configuration.GetValue("Seo:SitemapIncludeDetailUrls", true);

        var relativePaths = new List<string>();
        var seen = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

        foreach (var raw in paths)
        {
            var normalized = NormalizePath(raw);
            if (seen.Add(normalized))
            {
                relativePaths.Add(normalized);
            }
        }

        if (includeDetails)
        {
            try
            {
                foreach (var rel in EnumerateCatalogDetailRelativePaths(_catalog))
                {
                    if (seen.Add(rel))
                    {
                        relativePaths.Add(rel);
                    }
                }
            }
            catch
            {
                // Catalog may be unavailable; hub URLs from config still ship.
            }
        }

        XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        var urlElements = new List<XElement>();
        foreach (var path in relativePaths)
        {
            var loc = path == "/" ? baseUrl + "/" : $"{baseUrl}{path}";
            var depth = path.Trim('/').Split('/', StringSplitOptions.RemoveEmptyEntries).Length;
            var priority = path == "/" ? "1.0" : depth >= 2 ? "0.6" : "0.8";
            var changefreq = depth >= 2 ? "monthly" : "weekly";
            urlElements.Add(
                new XElement(
                    ns + "url",
                    new XElement(ns + "loc", loc),
                    new XElement(ns + "changefreq", changefreq),
                    new XElement(ns + "priority", priority)));
        }

        var doc = new XDocument(new XElement(ns + "urlset", urlElements));

        using var stream = new MemoryStream();
        var settings = new XmlWriterSettings
        {
            Indent = false,
            Encoding = new UTF8Encoding(false),
            OmitXmlDeclaration = false,
            Async = false
        };

        using (var writer = XmlWriter.Create(stream, settings))
        {
            doc.Save(writer);
        }

        return File(stream.ToArray(), "application/xml; charset=utf-8");
    }

    private static string NormalizePath(string? raw)
    {
        var path = (raw ?? "/").Trim();
        if (path.Length == 0)
        {
            path = "/";
        }

        if (!path.StartsWith('/'))
        {
            path = "/" + path;
        }

        return path;
    }

    /// <summary>Vietnamese canonical detail prefixes (default site locale).</summary>
    private static IEnumerable<string> EnumerateCatalogDetailRelativePaths(IPublicContentCatalog catalog)
    {
        foreach (var p in EnumerateSlugPaths(catalog, PublicContentKind.News, "/tin-tuc"))
        {
            yield return p;
        }

        foreach (var p in EnumerateSlugPaths(catalog, PublicContentKind.Location, "/dia-diem"))
        {
            yield return p;
        }

        foreach (var p in EnumerateSlugPaths(catalog, PublicContentKind.Job, "/viec-lam"))
        {
            yield return p;
        }
    }

    private static IEnumerable<string> EnumerateSlugPaths(IPublicContentCatalog catalog, PublicContentKind kind, string prefix)
    {
        const int batch = 24;
        var page = 1;
        while (true)
        {
            var result = catalog.GetCards(
                kind,
                new PublicContentQuery { Page = page, PageSize = batch, Sort = PublicContentSort.Newest });

            foreach (var card in result.Items)
            {
                if (string.IsNullOrWhiteSpace(card.Slug))
                {
                    continue;
                }

                yield return $"{prefix.TrimEnd('/')}/{card.Slug}";
            }

            if (result.Items.Count == 0 || result.Items.Count < result.PageSize)
            {
                break;
            }

            page++;
        }
    }
}
