using System.Globalization;
using System.Text.Json;

namespace KHHub.Publish_website.Localization;

public class AppLocalizer
{
    private readonly Dictionary<string, Dictionary<string, string>> _textsByCulture;

    public AppLocalizer(IWebHostEnvironment environment)
    {
        _textsByCulture = LoadTexts(environment.ContentRootPath);
    }

    public string this[string key]
    {
        get
        {
            var culture = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            if (_textsByCulture.TryGetValue(culture, out var localized) &&
                localized.TryGetValue(key, out var localizedValue))
            {
                return localizedValue;
            }

            if (_textsByCulture.TryGetValue("en", out var fallback) &&
                fallback.TryGetValue(key, out var fallbackValue))
            {
                return fallbackValue;
            }

            return key;
        }
    }

    private static Dictionary<string, Dictionary<string, string>> LoadTexts(string contentRootPath)
    {
        var result = new Dictionary<string, Dictionary<string, string>>(StringComparer.OrdinalIgnoreCase);
        var localizationDirectory = Path.Combine(contentRootPath, "Localization", "Publish_website");
        if (!Directory.Exists(localizationDirectory))
        {
            return result;
        }

        foreach (var filePath in Directory.GetFiles(localizationDirectory, "*.json"))
        {
            using var stream = File.OpenRead(filePath);
            using var json = JsonDocument.Parse(stream);
            if (!json.RootElement.TryGetProperty("culture", out var cultureProperty) ||
                !json.RootElement.TryGetProperty("texts", out var textsProperty))
            {
                continue;
            }

            var culture = cultureProperty.GetString();
            if (string.IsNullOrWhiteSpace(culture))
            {
                continue;
            }

            var texts = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (var property in textsProperty.EnumerateObject())
            {
                texts[property.Name] = property.Value.GetString() ?? string.Empty;
            }

            result[culture] = texts;
        }

        return result;
    }
}
