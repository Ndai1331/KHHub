using Localization.Resources.AbpUi;
using KHHub.LanguageService.Localization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Commercial.SuiteTemplates;
using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Domain;
using Volo.Abp.LanguageManagement;
using Volo.Abp.LanguageManagement.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Modularity;
using Volo.Abp.UI;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace KHHub.LanguageService;
    
[DependsOn(
    typeof(AbpValidationModule),
    typeof(AbpUiModule),
    typeof(VoloAbpCommercialSuiteTemplatesModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(LanguageManagementApplicationContractsModule)
)]

public class KHHubLanguageServiceContractsModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<KHHubLanguageServiceContractsModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<LanguageServiceResource>("vi")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/LanguageService");
            
            options.Languages.Add(new LanguageInfo("vi", "vi", "VN")); 
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "Chinese (Simplified)")); 
            options.Languages.Add(new LanguageInfo("en", "en", "English")); 
            options.Languages.Add(new LanguageInfo("ru", "ru", "Russian")); 
            options.Languages.Add(new LanguageInfo("ko", "ko", "Korean")); 

        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("LanguageManagementService", typeof(LanguageManagementResource));
        });
    }
}
