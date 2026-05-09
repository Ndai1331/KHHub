using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using KHHub.Publish_website.Localization;
using KHHub.Publish_website.Services;
using KHHub.Publish_website.Services.GoldPrices;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Globalization;

namespace KHHub.Publish_website;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateBootstrapLogger();

        try
        {
            Log.Information($"Starting {GetCurrentAssemblyName()}");

            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            builder.AddServiceDefaults();

            builder.Host
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    var applicationName = GetCurrentAssemblyName();

                    loggerConfiguration
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.WithProperty("Application", applicationName)
                        .If(context.Configuration.GetValue<bool>("ElasticSearch:IsLoggingEnabled"), c =>
                            c.WriteTo.Elasticsearch(
                                new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Url"]!))
                                {
                                    AutoRegisterTemplate = true,
                                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                                    IndexFormat = "KHHub-log-{0:yyyy.MM}"
                                })
                        )
                        .WriteTo.Async(c => c.OpenTelemetry());
                });

            builder.Services.AddLocalization();
            builder.Services.AddSingleton<AppLocalizer>();
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton(TimeProvider.System);
            builder.Services.AddSingleton<IPublicContentCatalog, MockPublicContentCatalog>();
            builder.Services.AddHttpClient<IGoldPriceClient, GoldPriceClient>(client =>
            {
                client.BaseAddress = new Uri("https://www.khanhhoatrend.com/");
                client.Timeout = TimeSpan.FromSeconds(8);
            });
            builder.Services.AddHealthChecks();
            builder.Services
                .AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AddPageRoute("/Jobs/Index", "viec-lam");
                    options.Conventions.AddPageRoute("/Jobs/Details", "viec-lam/{slug}");
                    options.Conventions.AddPageRoute("/JobCategories/Index", "danh-muc-viec-lam");
                    options.Conventions.AddPageRoute("/JobCategories/Details", "danh-muc-viec-lam/{slug}");
                    options.Conventions.AddPageRoute("/JobTags/Index", "the-viec-lam");
                    options.Conventions.AddPageRoute("/JobTags/Details", "the-viec-lam/{slug}");

                    //Place routes
                    options.Conventions.AddPageRoute("/Places/Index", "dia-diem");
                    options.Conventions.AddPageRoute("/Places/Details", "dia-diem/{slug}");
                    options.Conventions.AddPageRoute("/PlaceCategories/Index", "danh-muc-dia-diem");
                    options.Conventions.AddPageRoute("/PlaceCategories/Details", "danh-muc-dia-diem/{slug}");
                    options.Conventions.AddPageRoute("/PlaceTags/Index", "tags-dia-diem");
                    options.Conventions.AddPageRoute("/PlaceTags/Details", "tags-dia-diem/{slug}");

                    //Article routes
                    options.Conventions.AddPageRoute("/Articles/Index", "tin-tuc");
                    options.Conventions.AddPageRoute("/Articles/Details", "tin-tuc/{slug}");
                    options.Conventions.AddPageRoute("/ArticleCategories/Index", "danh-muc-tin-tuc");
                    options.Conventions.AddPageRoute("/ArticleCategories/Details", "danh-muc-tin-tuc/{slug}");
                    options.Conventions.AddPageRoute("/ArticleTags/Index", "tags-tin-tuc");
                    options.Conventions.AddPageRoute("/ArticleTags/Details", "tags-tin-tuc/{slug}");
                })
                .AddViewLocalization()
                .AddDataAnnotationsLocalization();

            builder.Services.AddHttpClient<PublicMasterDataCatalogClient>((_, client) =>
            {
                var baseUrl = configuration["RemoteServices:Default:BaseUrl"]
                    ?? throw new InvalidOperationException("Configuration RemoteServices:Default:BaseUrl is required.");
                client.BaseAddress = new Uri(baseUrl.TrimEnd('/') + "/");
                client.Timeout = TimeSpan.FromSeconds(30);
            });

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
                {
                    options.Authority = configuration["AuthServer:Authority"];
                    options.RequireHttpsMetadata = configuration.GetValue<bool>("AuthServer:RequireHttpsMetadata");
                    options.ClientId = configuration["AuthServer:ClientId"];
                    options.ClientSecret = configuration["AuthServer:ClientSecret"];
                    options.ResponseType = OpenIdConnectResponseType.Code;
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                });

            var app = builder.Build();

            var supportedCultures = new[] { new CultureInfo("vi"), new CultureInfo("en") };
            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            // Drop the Accept-Language header provider so a browser configured for English
            // does not override the site default. The active language is decided by:
            //   1. ?culture=xx query string (one-shot, used by the SetLanguage handler)
            //   2. The .AspNetCore.Culture cookie set when the user picks a language
            //   3. The default culture (vi)
            localizationOptions.RequestCultureProviders.RemoveAll(p => p is AcceptLanguageHeaderRequestCultureProvider);

            app.UseRequestLocalization(localizationOptions);

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapHealthChecks("/health-status", new HealthCheckOptions());
            app.MapRazorPages();
            await app.RunAsync();

            Log.Information($"Stopped {GetCurrentAssemblyName()}");

            return 0;
        }
        catch (HostAbortedException)
        {
            /* Ignoring this exception because: https://github.com/dotnet/efcore/issues/29809#issuecomment-1345132260 */
            return 2;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{GetCurrentAssemblyName()} terminated unexpectedly!");
            Console.WriteLine(ex.ToString());
            Console.WriteLine(ex.StackTrace ?? "");
            
            Log.Fatal(ex, $"{GetCurrentAssemblyName()} terminated unexpectedly!");
            Log.Fatal(ex.Message);
            Log.Fatal(ex.StackTrace ?? "");
            
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static string GetCurrentAssemblyName()
    {
        return typeof(Program).Assembly.GetName().Name!;
    }
}