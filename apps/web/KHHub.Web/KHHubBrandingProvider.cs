using Microsoft.Extensions.Localization;
using KHHub.LanguageService.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace KHHub.Web;

[Dependency(ReplaceServices = true)]
public class KHHubBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<LanguageServiceResource> _localizer;

    public KHHubBrandingProvider(IStringLocalizer<LanguageServiceResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["KHHub"];
}
