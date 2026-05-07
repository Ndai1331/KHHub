using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using KHHub.Publish_website.Localization;
using KHHub.Publish_website.Services;
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
            builder.Services.AddHealthChecks();
            builder.Services
                .AddRazorPages()
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

            var supportedCultures = new[] { new CultureInfo("en"), new CultureInfo("vi") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("vi"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

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