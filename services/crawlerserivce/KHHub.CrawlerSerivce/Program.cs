using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using Volo.Abp;
using Volo.Abp.Studio.Configuration;

namespace KHHub.CrawlerSerivce;

public class Program
{
    private const string StartupLogPrefix = "[Startup]";

    public static async Task<int> Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
            .CreateBootstrapLogger();

        try
        {
            Log.Information("{Prefix} Starting {Application}", StartupLogPrefix, GetCurrentAssemblyName());

            AbpStudioEnvironmentVariableLoader.Load();
            Log.Debug("{Prefix} ABP Studio environment variables loaded", StartupLogPrefix);

            Log.Information("{Prefix} Creating WebApplication builder...", StartupLogPrefix);
            var builder = WebApplication.CreateBuilder(args);

            Log.Information("{Prefix} Adding Aspire service defaults...", StartupLogPrefix);
            builder.AddServiceDefaults();

            builder.Host
                .AddAppSettingsSecretsJson()
                .UseAutofac()
                .UseSerilog((context, services, loggerConfiguration) =>
                {
                    var applicationName = services.GetRequiredService<IApplicationInfoAccessor>().ApplicationName;

                    loggerConfiguration
                        .MinimumLevel.Debug()
                        .ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.WithProperty("Application", applicationName)
                        .WriteTo.Async(c => c.File("Logs/logs.txt"))
                        .WriteTo.Async(c => c.Console())
                        .If(context.Configuration.GetValue<bool>("ElasticSearch:IsLoggingEnabled"), c =>
                            c.WriteTo.Elasticsearch(
                                new ElasticsearchSinkOptions(new Uri(context.Configuration["ElasticSearch:Url"]!))
                                {
                                    AutoRegisterTemplate = true,
                                    AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv6,
                                    IndexFormat = "KHHub-log-{0:yyyy.MM}"
                                })
                        )
                        .WriteTo.Async(c => c.OpenTelemetry())
                        .WriteTo.Async(c => c.AbpStudio(services));
                });

            Log.Information("{Prefix} Registering ABP module {Module}...", StartupLogPrefix, nameof(KHHubCrawlerSerivceModule));
            await builder.AddApplicationAsync<KHHubCrawlerSerivceModule>();

            Log.Information("{Prefix} Building application...", StartupLogPrefix);
            var app = builder.Build();

            LogStartupEndpointHints(app.Configuration);

            Log.Information("{Prefix} Initializing application (DB migration, middleware, endpoints)...", StartupLogPrefix);
            await app.InitializeApplicationAsync();

            Log.Information(
                "{Prefix} Application initialized. Listening URLs: {Urls}. Try Swagger: /swagger, Health: {HealthPath}, Health UI: /health-ui",
                StartupLogPrefix,
                app.Configuration["ASPNETCORE_URLS"] ?? "(from launchSettings / Kestrel)",
                app.Configuration["App:HealthCheckUrl"] ?? "/health-status");

            await app.RunAsync();

            Log.Information("{Prefix} Stopped {Application}", StartupLogPrefix, GetCurrentAssemblyName());

            return 0;
        }
        catch (HostAbortedException ex)
        {
            // EF Core design-time tools abort the host intentionally.
            Log.Warning(ex, "{Prefix} Host aborted (often EF tools): {Message}", StartupLogPrefix, ex.Message);
            return 2;
        }
        catch (Exception ex)
        {
            LogStartupFailure(ex);
            return 1;
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }

    private static void LogStartupEndpointHints(IConfiguration configuration)
    {
        var swaggerEnabled = configuration.GetValue("Swagger:IsEnabled", true);
        var healthPath = configuration["App:HealthCheckUrl"] ?? "/health-status";
        var authAuthority = configuration["AuthServer:Authority"];
        var redis = configuration["Redis:Configuration"];
        var crawlerDb = configuration.GetConnectionString("CrawlerSerivce");

        Log.Information(
            "{Prefix} Config snapshot — Swagger: {SwaggerEnabled}, Health: {HealthPath}, AuthServer: {AuthAuthority}, Redis: {Redis}, CrawlerDb: {CrawlerDb}",
            StartupLogPrefix,
            swaggerEnabled,
            healthPath,
            string.IsNullOrWhiteSpace(authAuthority) ? "(empty — Swagger OAuth may fail)" : authAuthority,
            redis ?? "(missing)",
            MaskConnectionString(crawlerDb));
    }

    private static void LogStartupFailure(Exception ex)
    {
        Console.WriteLine($"{GetCurrentAssemblyName()} terminated unexpectedly!");
        Console.WriteLine(ex.ToString());

        Log.Fatal(ex, "{Prefix} {Application} terminated unexpectedly", StartupLogPrefix, GetCurrentAssemblyName());

        for (var inner = ex.InnerException; inner != null; inner = inner.InnerException)
        {
            Log.Fatal(inner, "{Prefix} Inner exception: {Type}: {Message}", StartupLogPrefix, inner.GetType().Name, inner.Message);
        }

        if (ex is IOException { Message: var msg } && msg.Contains("address already in use", StringComparison.OrdinalIgnoreCase))
        {
            Log.Fatal(
                "{Prefix} Port is already in use. Stop the other process or change applicationUrl in Properties/launchSettings.json (current default: http://localhost:44300).",
                StartupLogPrefix);
        }
    }

    private static string MaskConnectionString(string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            return "(missing)";
        }

        const string passwordKey = "Password=";
        var idx = connectionString.IndexOf(passwordKey, StringComparison.OrdinalIgnoreCase);
        if (idx < 0)
        {
            return connectionString.Length > 80 ? connectionString[..80] + "..." : connectionString;
        }

        var end = connectionString.IndexOf(';', idx);
        return end < 0
            ? connectionString[..idx] + passwordKey + "***"
            : connectionString[..idx] + passwordKey + "***" + connectionString[end..];
    }

    private static string GetCurrentAssemblyName()
    {
        return typeof(Program).Assembly.GetName().Name!;
    }
}