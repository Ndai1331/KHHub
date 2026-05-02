using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService;
using Volo.Abp.Identity;
using Volo.Abp.Account.Admin.Web;
using Volo.Abp.Identity.Web;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using KHHub.Web.Navigation;
using Polly;
using Prometheus;
using StackExchange.Redis;
using Volo.Abp;
using Volo.Abp.Studio;
using Volo.Abp.Account;
using Volo.Abp.Account.LinkUsers;
using Volo.Abp.Account.Pro.Public.Web.Shared;
using Volo.Abp.Account.Public.Web.Impersonation;
using Volo.Abp.AspNetCore.Authentication.OpenIdConnect;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Mvc.Libs;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.Shared.Toolbars;
using Volo.Abp.Mapperly;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonX.Bundling;
using Volo.Abp.LeptonX.Shared;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.Web;
using Volo.Abp.Http.Client.IdentityModel.Web;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Studio.Client.AspNetCore;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Security.Claims;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Pro.Web;
using Volo.Abp.LanguageManagement;
using KHHub.LanguageService;
using KHHub.LanguageService.Localization;
using Volo.Abp.TextTemplateManagement;
using Volo.Abp.TextTemplateManagement.Web;
using Volo.Abp.AuditLogging;
using Volo.Abp.AuditLogging.Web;
using KHHub.AuditLoggingService;
using Volo.Abp.Gdpr;
using Volo.Abp.Gdpr.Web;
using KHHub.GdprService;
using Volo.AIManagement;
using Volo.AIManagement.Client;
using Volo.AIManagement.Web;
using KHHub.AIManagementService;
using KHHub.IdentityService;
using KHHub.AdministrationService;
using KHHub.AdministrationService.Permissions;
using KHHub.Web.HealthChecks;

namespace KHHub.Web;

[DependsOn(typeof(KHHubMasterDataServiceContractsModule), typeof(AbpAccountAdminHttpApiClientModule), typeof(AbpAccountAdminWebModule), typeof(KHHubIdentityServiceContractsModule), typeof(KHHubAdministrationServiceContractsModule), typeof(AbpOpenIddictProWebModule), typeof(AbpOpenIddictProHttpApiClientModule), typeof(LanguageManagementWebModule), typeof(LanguageManagementHttpApiClientModule), typeof(KHHubLanguageServiceContractsModule), typeof(TextTemplateManagementWebModule), typeof(TextTemplateManagementHttpApiClientModule), typeof(AbpAuditLoggingWebModule), typeof(AbpAuditLoggingHttpApiClientModule), typeof(KHHubAuditLoggingServiceContractsModule), typeof(AbpGdprWebModule), typeof(AbpGdprHttpApiClientModule), typeof(KHHubGdprServiceContractsModule), typeof(KHHubAIManagementServiceContractsModule), typeof(AIManagementWebModule), typeof(AIManagementClientWebModule), typeof(AIManagementHttpApiClientModule), typeof(AIManagementClientHttpApiClientModule), typeof(AbpAccountPublicWebImpersonationModule), typeof(AbpAccountPublicWebSharedModule), typeof(AbpAccountPublicHttpApiClientModule), typeof(AbpIdentityHttpApiClientModule), typeof(AbpIdentityWebModule), typeof(AbpFeatureManagementWebModule), typeof(AbpCachingStackExchangeRedisModule), typeof(AbpEventBusRabbitMqModule), typeof(AbpAspNetCoreMvcClientModule), typeof(AbpAspNetCoreAuthenticationOpenIdConnectModule), typeof(AbpHttpClientWebModule), typeof(AbpHttpClientIdentityModelWebModule), typeof(AbpAutofacModule), typeof(AbpAspNetCoreSerilogModule), typeof(AbpAspNetCoreMvcUiLeptonXThemeModule), typeof(AbpSettingManagementHttpApiClientModule), typeof(AbpPermissionManagementHttpApiClientModule), typeof(AbpFeatureManagementHttpApiClientModule), typeof(AbpStudioClientAspNetCoreModule))]
public class KHHubWebModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigureDataAnnotations(context);
        PreConfigureHttpClient();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(typeof(KHHubMasterDataServiceContractsModule).Assembly, "MasterDataService");
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        var redis = CreateRedisConnection(configuration);
        ConfigureStudio(hostingEnvironment);
        ConfigurePII(configuration);
        ConfigureLocalization(hostingEnvironment);
        ConfigureBundling();
        context.Services.Configure<AbpMvcLibsOptions>(options =>
        {
            options.CheckLibs = false;
        });
        ConfigureDistributedCache(configuration);
        ConfigureUrls(configuration);
        ConfigureAuthentication(context, configuration);
        ConfigureDataProtection(context, configuration, redis);
        ConfigureNavigation(configuration);
        ConfigureToolbar();
        ConfigureLeptonXTheme();
        ConfigureImpersonation();
        ConfigureDynamicClaims(context);
        ConfigureHealthChecks(context);
        ConfigurePages();
        if (hostingEnvironment.IsDevelopment())
        {
            context.Services.AddRazorPages().AddRazorRuntimeCompilation();
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseForwardedHeaders(new ForwardedHeadersOptions { ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto });

        if (!env.IsDevelopment())
        {
            app.Use((ctx, next) => {
                if (!IsHealthOrMetricsProbePath(ctx.Request.Path))
                {
                    ctx.Request.Scheme = "https";
                }
                return next();
            });
        }

        app.UseWhen(
            ctx => !IsHealthOrMetricsProbePath(ctx.Request.Path),
            branch => branch.UseAbpRequestLocalization());

        if (!env.IsDevelopment())
        {
            app.UseErrorPage();
        }

        if (env.IsDevelopment())
        {
            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.None, Secure = CookieSecurePolicy.Always });
        }

        app.UseCorrelationId();
        app.UseRouting();
        app.MapAbpStaticAssets();
        app.UseAbpStudioLink();
        app.UseAbpSecurityHeaders();
        app.UseHttpMetrics();
        app.UseAuthentication();
        app.UseAbpSerilogEnrichers();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseConfiguredEndpoints(endpoints => {
            endpoints.MapMetrics();
        });
    }

    private static void PreConfigureDataAnnotations(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options => {
            options.AddAssemblyResource(typeof(LanguageServiceResource), typeof(KHHubWebModule).Assembly);
        });
    }

    private void PreConfigureHttpClient()
    {
        PreConfigure<AbpHttpClientBuilderOptions>(options => {
            options.ProxyClientBuildActions.Add((remoteServiceName, clientBuilder) => {
                clientBuilder.AddTransientHttpErrorPolicy(policyBuilder => policyBuilder.WaitAndRetryAsync(4, i => TimeSpan.FromSeconds(Math.Pow(2, i))));
            });
        });
    }

    private ConnectionMultiplexer CreateRedisConnection(IConfiguration configuration)
    {
        return ConnectionMultiplexer.Connect(configuration["Redis:Configuration"]);
    }

    private void ConfigureHealthChecks(ServiceConfigurationContext context)
    {
        context.Services.AddWebHealthChecks();
    }

    private void ConfigurePages()
    {
        Configure<RazorPagesOptions>(options => {
            options.Conventions.AuthorizePage("/HostDashboard", AdministrationServicePermissions.Dashboard.Host);
            options.Conventions.AuthorizePage("/Provinces/Index", MasterDataServicePermissions.Provinces.Default);
            options.Conventions.AuthorizePage("/Wards/Index", MasterDataServicePermissions.Wards.Default);
            options.Conventions.AuthorizePage("/ArticleCategories/Index", MasterDataServicePermissions.ArticleCategories.Default);
            options.Conventions.AuthorizePage("/ArticleTags/Index", MasterDataServicePermissions.ArticleTags.Default);
            options.Conventions.AuthorizePage("/Articles/Index", MasterDataServicePermissions.Articles.Default);
            options.Conventions.AuthorizePage("/ArticleTagMappings/Index", MasterDataServicePermissions.ArticleTagMappings.Default);
            options.Conventions.AuthorizePage("/ArticleViews/Index", MasterDataServicePermissions.ArticleViews.Default);
            options.Conventions.AuthorizePage("/MediaFiles/Index", MasterDataServicePermissions.MediaFiles.Default);
            options.Conventions.AuthorizePage("/PlaceCategories/Index", MasterDataServicePermissions.PlaceCategories.Default);
            options.Conventions.AuthorizePage("/PlaceTags/Index", MasterDataServicePermissions.PlaceTags.Default);
            options.Conventions.AuthorizePage("/Places/Index", MasterDataServicePermissions.Places.Default);
            options.Conventions.AuthorizePage("/PlaceTagMappings/Index", MasterDataServicePermissions.PlaceTagMappings.Default);
            options.Conventions.AuthorizePage("/EntityFiles/Index", MasterDataServicePermissions.EntityFiles.Default);
            options.Conventions.AuthorizePage("/PlaceReviews/Index", MasterDataServicePermissions.PlaceReviews.Default);
            options.Conventions.AuthorizePage("/PlaceFavorites/Index", MasterDataServicePermissions.PlaceFavorites.Default);
            options.Conventions.AuthorizePage("/PlaceViews/Index", MasterDataServicePermissions.PlaceViews.Default);
        });
    }

    private void ConfigureStudio(IHostEnvironment hostingEnvironment)
    {
        if (hostingEnvironment.IsProduction())
        {
            Configure<AbpStudioClientOptions>(options => {
                options.IsLinkEnabled = false;
            });
        }
    }

    private void ConfigurePII(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(configuration["App:EnablePII"]))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }
    }

    private void ConfigureLocalization(IWebHostEnvironment hostingEnvironment)
    {
        Configure<AbpVirtualFileSystemOptions>(options => {
            options.FileSets.AddEmbedded<KHHubWebModule>();
        });
        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options => {
                options.FileSets.ReplaceEmbeddedByPhysical<KHHubWebModule>(hostingEnvironment.ContentRootPath);
                options.FileSets.ReplaceEmbeddedByPhysical<KHHubLanguageServiceContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}..{0}services{0}language{0}KHHub.LanguageService.Contracts", Path.DirectorySeparatorChar)));
            });
        }
    }

    private void ConfigureBundling()
    {
        Configure<AbpBundlingOptions>(options => {
            options.StyleBundles.Configure(LeptonXThemeBundles.Styles.Global, bundle => {
                bundle.AddFiles("/global-styles.css");
            });
            options.ScriptBundles.Configure(LeptonXThemeBundles.Scripts.Global, bundle => {
                bundle.AddFiles("/global-scripts.js");
            });
        });
    }

    private void ConfigureDistributedCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options => {
            options.KeyPrefix = configuration["AbpDistributedCache:KeyPrefix"]!;
        });
    }

    private void ConfigureUrls(IConfiguration configuration)
    {
        Configure<AppUrlOptions>(options => {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
        });
        Configure<AbpAccountLinkUserOptions>(options => {
            options.LoginUrl = configuration["AuthServer:Authority"];
        });
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(options => {
            options.DefaultScheme = "Cookies";
            options.DefaultChallengeScheme = "oidc";
        }).AddCookie("Cookies", options => {
            options.ExpireTimeSpan = TimeSpan.FromDays(365);
        }).AddAbpOpenIdConnect("oidc", options => {
            options.Authority = configuration["AuthServer:Authority"];
            options.RequireHttpsMetadata = configuration.GetValue<bool>(configuration["AuthServer:RequireHttpsMetadata"]);
            options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
            options.ClientId = configuration["AuthServer:ClientId"];
            options.ClientSecret = configuration["AuthServer:ClientSecret"];
            options.SaveTokens = true;
            options.GetClaimsFromUserInfoEndpoint = true;
            options.TokenValidationParameters.ValidIssuers = new[] {
                configuration["AuthServer:Authority"].EnsureEndsWith('/'),
                configuration["AuthServer:MetaAddress"].EnsureEndsWith('/')
            };
            options.Scope.Add("roles");
            options.Scope.Add("email");
            options.Scope.Add("phone");
            options.Scope.Add("AuthServer");
            options.Scope.Add("IdentityService");
            options.Scope.Add("AdministrationService");
            options.Scope.Add("MasterDataService");
            options.Scope.Add("AuditLoggingService");
            options.Scope.Add("GdprService");
            options.Scope.Add("AIManagementService");
            options.Scope.Add("LanguageService");
        });
        if (Convert.ToBoolean(configuration["AuthServer:IsOnK8s"]))
        {
            context.Services.Configure<OpenIdConnectOptions>("oidc", options => {
                options.MetadataAddress = configuration["AuthServer:MetaAddress"].EnsureEndsWith('/') + ".well-known/openid-configuration";
                var previousOnRedirectToIdentityProvider = options.Events.OnRedirectToIdentityProvider;
                options.Events.OnRedirectToIdentityProvider = async ctx => {
                    // Intercept the redirection so the browser navigates to the right URL in your host
                    ctx.ProtocolMessage.IssuerAddress = configuration["AuthServer:Authority"].EnsureEndsWith('/') + "connect/authorize";
                    if (previousOnRedirectToIdentityProvider != null)
                    {
                        await previousOnRedirectToIdentityProvider(ctx);
                    }
                };
                var previousOnRedirectToIdentityProviderForSignOut = options.Events.OnRedirectToIdentityProviderForSignOut;
                options.Events.OnRedirectToIdentityProviderForSignOut = async ctx => {
                    // Intercept the redirection for signout so the browser navigates to the right URL in your host
                    ctx.ProtocolMessage.IssuerAddress = configuration["AuthServer:Authority"].EnsureEndsWith('/') + "connect/endsession";
                    if (previousOnRedirectToIdentityProviderForSignOut != null)
                    {
                        await previousOnRedirectToIdentityProviderForSignOut(ctx);
                    }
                };
            });
        }
    }

    private static void ConfigureDataProtection(ServiceConfigurationContext context, IConfiguration configuration, ConnectionMultiplexer redis)
    {
        context.Services.AddDataProtection().SetApplicationName(configuration["DataProtection:ApplicationName"]!).PersistKeysToStackExchangeRedis(redis, configuration["DataProtection:Keys"]);
    }

    private void ConfigureNavigation(IConfiguration configuration)
    {
        Configure<AbpNavigationOptions>(options => {
            options.MenuContributors.Add(new KHHubMenuContributor(configuration));
        });
    }

    private void ConfigureToolbar()
    {
        Configure<AbpToolbarOptions>(options => {
            options.Contributors.Add(new KHHubToolbarContributor());
        });
    }

    private void ConfigureLeptonXTheme()
    {
        Configure<LeptonXThemeOptions>(options => {
            options.DefaultStyle = LeptonXStyleNames.System;
        });
        Configure<LeptonXThemeMvcOptions>(options => {
            options.ApplicationLayout = LeptonXMvcLayouts.SideMenu;
        });
    }

    private void ConfigureImpersonation()
    {
        Configure<AbpIdentityWebOptions>(options => {
            options.EnableUserImpersonation = true;
        });
    }

    private void ConfigureDynamicClaims(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options => {
            options.IsDynamicClaimsEnabled = true;
        });
    }

    private static bool IsHealthOrMetricsProbePath(PathString path)
    {
        return path.StartsWithSegments("/health-status")
            || path.StartsWithSegments("/health-ui")
            || path.StartsWithSegments("/health-api")
            || path.StartsWithSegments("/metrics");
    }
}