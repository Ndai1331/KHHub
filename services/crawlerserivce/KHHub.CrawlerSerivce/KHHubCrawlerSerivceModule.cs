using Medallion.Threading;
using Medallion.Threading.Redis;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.AspNetCore.Serilog;
using Microsoft.OpenApi;
using KHHub.CrawlerSerivce.Data;
using Prometheus;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.Mapperly;
using Volo.Abp.BackgroundJobs.RabbitMQ;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Caching;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp.Uow;
using Volo.Abp.DistributedLocking;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.AuditLogging;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Http.Client;
using Volo.Abp.LanguageManagement;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Studio.Client.AspNetCore;
using Volo.Abp.Swashbuckle;
using Volo.Abp.Security.Claims;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.LanguageManagement.EntityFrameworkCore;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.PostgreSql;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.DistributedEvents;
using Volo.Abp.BlobStoring.Database.EntityFrameworkCore;
using KHHub.CrawlerSerivce.BackgroundWorkers;
using KHHub.CrawlerSerivce.Configuration;
using KHHub.CrawlerSerivce.Crawling.Http;
using KHHub.CrawlerSerivce.HealthChecks;
using KHHub.MasterDataService;

namespace KHHub.CrawlerSerivce;

[DependsOn(
    typeof(BlobStoringDatabaseEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCorePostgreSqlModule),
    typeof(LanguageManagementEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(KHHubCrawlerSerivceContractsModule),
    typeof(KHHubMasterDataServiceContractsModule),
    typeof(AbpAutofacModule),
    typeof(AbpAspNetCoreSerilogModule),
    typeof(AbpSwashbuckleModule),
    typeof(AbpAspNetCoreMvcModule),
    typeof(AbpEventBusRabbitMqModule),
    typeof(AbpBackgroundJobsRabbitMqModule),
    typeof(AbpBackgroundWorkersModule),
    typeof(AbpCachingStackExchangeRedisModule),
    typeof(AbpDistributedLockingModule),
    typeof(AbpStudioClientAspNetCoreModule),
    typeof(AbpHttpClientModule)
    )]
public class KHHubCrawlerSerivceModule : AbpModule
{
    private const string StartupLogPrefix = "[Startup]";

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();
        var env = context.Services.GetHostingEnvironment();

        Log.Information("{Prefix} ConfigureServices started (Environment: {Environment})", StartupLogPrefix, env.EnvironmentName);

        var redis = CreateRedisConnection(configuration);

        ConfigurePII(configuration);
        ConfigureJwtBearer(context, configuration);
        ConfigureCors(context, configuration);
        ConfigureSwagger(context, configuration);
        ConfigureDatabase(context);
        ConfigureDistributedCache(configuration);
        ConfigureDataProtection(context, configuration, redis);
        ConfigureDistributedLock(context, redis);
        ConfigureDistributedEventBus();
        ConfigureIntegrationServices();
        ConfigureAntiForgery(env);
        ConfigureVirtualFileSystem();
        ConfigureObjectMapper(context);
        ConfigureAutoControllers();
        ConfigureDynamicClaims(context);
        ConfigureHealthChecks(context);
        ConfigureCrawlerHttp(context, configuration);
        ConfigureCrawlerImportDefaults(configuration);
        ConfigureCrawlerArticleImportDefaults(configuration);
        ConfigureCrawlerPlaceImportDefaults(configuration);
        ConfigureCrawlerScheduledImports(configuration);
        ConfigureMasterDataHttpClient(context);

        context.Services.TransformAbpClaims();

        Log.Information("{Prefix} ConfigureServices completed", StartupLogPrefix);
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();
        var configuration = context.GetConfiguration();

        Log.Information("{Prefix} Configuring HTTP pipeline...", StartupLogPrefix);

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCorrelationId();
        app.UseAbpRequestLocalization();
        app.MapAbpStaticAssets();
        app.UseAbpStudioLink();
        app.UseCors();
        app.UseRouting();
        app.UseHttpMetrics();
        app.UseAuthentication();
        app.UseAuthorization();

        var swaggerEnabled = IsSwaggerEnabled(configuration);
        if (swaggerEnabled)
        {
            Log.Information("{Prefix} Swagger enabled — UI: /swagger, JSON: /swagger/v1/swagger.json", StartupLogPrefix);
            app.UseSwagger();
            app.UseAbpSwaggerUI(options => { ConfigureSwaggerUI(options, configuration); });
        }
        else
        {
            Log.Warning("{Prefix} Swagger is disabled (Swagger:IsEnabled=false)", StartupLogPrefix);
        }

        app.UseAbpSerilogEnrichers();
        app.UseAuditing();
        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseConfiguredEndpoints(endpoints =>
        {
            // Attribute-routed controllers (e.g. DemoController, conventional API) need MapControllers().
            // Without this, no MVC endpoints are registered and Swagger UI / OpenAPI return 404.
            endpoints.MapControllers();
            endpoints.MapMetrics();
            Log.Information("{Prefix} Endpoints mapped: MapControllers, MapMetrics", StartupLogPrefix);
        });

        var healthPath = configuration["App:HealthCheckUrl"] ?? "/health-status";
        Log.Information(
            "{Prefix} Health endpoints — status: {HealthPath}, UI: /health-ui, API: /health-api",
            StartupLogPrefix,
            healthPath);
    }

    public override async Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var logger = context.ServiceProvider.GetRequiredService<ILogger<KHHubCrawlerSerivceModule>>();
        logger.LogInformation("{Prefix} Applying database migrations for CrawlerSerivce...", StartupLogPrefix);

        try
        {
            using var scope = context.ServiceProvider.CreateScope();
            await scope.ServiceProvider
                .GetRequiredService<CrawlerSerivceRuntimeDatabaseMigrator>()
                .CheckAndApplyDatabaseMigrationsAsync();
            logger.LogInformation("{Prefix} Database migrations completed", StartupLogPrefix);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Prefix} Database migration failed: {Message}", StartupLogPrefix, ex.Message);
            throw;
        }
    }

    public override async Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        // Must call base so OnApplicationInitialization runs (HTTP pipeline, Swagger, health endpoints).
        // Overriding this method without base skips UseConfiguredEndpoints and /health-status returns 404.
        await base.OnApplicationInitializationAsync(context);

        Log.Information("{Prefix} Registering background worker {Worker}...", StartupLogPrefix, nameof(CrawlerScheduledImportTriggerWorker));
        await context.AddBackgroundWorkerAsync<CrawlerScheduledImportTriggerWorker>();
        Log.Information("{Prefix} Background worker registered", StartupLogPrefix);
    }

    private ConnectionMultiplexer CreateRedisConnection(IConfiguration configuration)
    {
        var redisConfiguration = configuration["Redis:Configuration"] ?? "localhost:6379";
        Log.Information("{Prefix} Connecting to Redis: {RedisConfiguration}", StartupLogPrefix, redisConfiguration);

        try
        {
            var redis = ConnectionMultiplexer.Connect(redisConfiguration);
            Log.Information("{Prefix} Redis connected", StartupLogPrefix);
            return redis;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "{Prefix} Redis connection failed. Is Redis running at {RedisConfiguration}?", StartupLogPrefix, redisConfiguration);
            throw;
        }
    }

    private void ConfigureHealthChecks(ServiceConfigurationContext context)
    {
        context.Services.AddCrawlerSerivceHealthChecks();
    }

    private void ConfigureCrawlerHttp(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddHttpClient(CrawlerHtmlFetcher.HttpClientName, client =>
        {
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                configuration["Crawler:UserAgent"] ?? "KHHub-Crawler/1.0");
            client.Timeout = TimeSpan.FromSeconds(60);
        });
    }

    private void ConfigureCrawlerImportDefaults(IConfiguration configuration)
    {
        Configure<CrawlerImportDefaultsOptions>(configuration.GetSection(CrawlerImportDefaultsOptions.SectionKey));
    }

    private void ConfigureCrawlerArticleImportDefaults(IConfiguration configuration)
    {
        Configure<CrawlerArticleImportDefaultsOptions>(configuration.GetSection(CrawlerArticleImportDefaultsOptions.SectionKey));
    }

    private void ConfigureCrawlerPlaceImportDefaults(IConfiguration configuration)
    {
        Configure<CrawlerPlaceImportDefaultsOptions>(configuration.GetSection(CrawlerPlaceImportDefaultsOptions.SectionKey));
    }

    private void ConfigureCrawlerScheduledImports(IConfiguration configuration)
    {
        Configure<CrawlerScheduledImportsOptions>(configuration.GetSection(CrawlerScheduledImportsOptions.SectionKey));
    }

    private void ConfigureMasterDataHttpClient(ServiceConfigurationContext context)
    {
        context.Services.AddStaticHttpClientProxies(typeof(KHHubMasterDataServiceContractsModule).Assembly, "MasterDataService");
    }
    
    private void ConfigurePII(IConfiguration configuration)
    {
        if (configuration.GetValue<bool>(configuration["App:EnablePII"] ?? "false"))
        {
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.ShowPII = true;
            Microsoft.IdentityModel.Logging.IdentityModelEventSource.LogCompleteSecurityArtifact = true;
        }
    }

    
    private void ConfigureJwtBearer(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddAbpJwtBearer(options =>
            {
                options.Authority = configuration["AuthServer:Authority"];
                options.MetadataAddress = configuration["AuthServer:MetaAddress"]!.EnsureEndsWith('/') + ".well-known/openid-configuration";
                options.RequireHttpsMetadata = configuration.GetValue<bool>(configuration["AuthServer:RequireHttpsMetadata"]);
                options.Audience = configuration["AuthServer:Audience"];
            });
    }

    private void ConfigureVirtualFileSystem()
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<KHHubCrawlerSerivceModule>();
        });
    }

    private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
    {
        var corsOrigins = configuration["App:CorsOrigins"];
        context.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                if (corsOrigins != null)
                {
                    builder
                        .WithOrigins(
                            corsOrigins
                                .Split(",", StringSplitOptions.RemoveEmptyEntries)
                                .Select(o => o.RemovePostFix("/"))
                                .ToArray()
                        )
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                }
            });
        });
    }

    private void ConfigureSwagger(ServiceConfigurationContext context, IConfiguration configuration)
    {
        if (IsSwaggerEnabled(configuration))
        {
            var authority = configuration["AuthServer:Authority"];
            if (string.IsNullOrWhiteSpace(authority))
            {
                Log.Warning(
                    "{Prefix} AuthServer:Authority is empty — Swagger OAuth and JWT metadata may fail at runtime",
                    StartupLogPrefix);
            }

            context.Services.AddAbpSwaggerGenWithOAuth(
                authority: configuration["AuthServer:Authority"],
                scopes: new Dictionary<string, string>
                {
                    {"CrawlerSerivce", "CrawlerSerivce Service API"}
                },
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "CrawlerSerivce API", Version = "v1"});
                    options.DocInclusionPredicate((_, _) => true);
                    options.CustomSchemaIds(type => type.FullName);
                });
        }
    }

    private void ConfigureDatabase(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.Databases.Configure("AdministrationService", database =>
            {
                database.MappedConnections.Add(AbpPermissionManagementDbProperties.ConnectionStringName);
                database.MappedConnections.Add(AbpFeatureManagementDbProperties.ConnectionStringName);
                database.MappedConnections.Add(AbpSettingManagementDbProperties.ConnectionStringName);
            });
            
            options.Databases.Configure("AuditLoggingService", database =>
            {
                database.MappedConnections.Add(AbpAuditLoggingDbProperties.ConnectionStringName);
            });
            
            options.Databases.Configure("LanguageService", database =>
            {
                database.MappedConnections.Add(LanguageManagementDbProperties.ConnectionStringName);
            });
        });

        context.Services.AddAbpDbContext<CrawlerSerivceDbContext>(options =>
        {
            options.AddDefaultRepositories();
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.Configure(opts =>
            {
                /* Sets default DBMS for this service */
                opts.UseNpgsql();
            });
            
            options.Configure<CrawlerSerivceDbContext>(c =>
            {
                c.UseNpgsql(b =>
                {
                    b.MigrationsHistoryTable("__CrawlerSerivce_Migrations");
                });
            });
        });
    }
    
    private void ConfigureDistributedCache(IConfiguration configuration)
    {
        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = configuration["AbpDistributedCache:KeyPrefix"] ?? "";
        });
    }
    
    private void ConfigureDataProtection(ServiceConfigurationContext context, IConfiguration configuration, IConnectionMultiplexer redis)
    {
        context.Services
            .AddDataProtection()
            .SetApplicationName(configuration["DataProtection:ApplicationName"]!)
            .PersistKeysToStackExchangeRedis(redis, configuration["DataProtection:Keys"]);
    }

    private void ConfigureDistributedLock(ServiceConfigurationContext context, IConnectionMultiplexer redis)
    {
        context.Services.AddSingleton<IDistributedLockProvider>(
            _ => new RedisDistributedSynchronizationProvider(redis.GetDatabase())
        );
    }

    private void ConfigureDistributedEventBus()
    {
        Configure<AbpDistributedEventBusOptions>(options =>
        {
            options.Inboxes.Configure(config =>
            {
                config.UseDbContext<CrawlerSerivceDbContext>();
            });

            options.Outboxes.Configure(config =>
            {
                config.UseDbContext<CrawlerSerivceDbContext>();
            });
        });
    }
    
    private void ConfigureIntegrationServices()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options.ExposeIntegrationServices = true;
        });
    }
    
    private void ConfigureAntiForgery(IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            Configure<AbpAntiForgeryOptions>(options =>
            {
                /* Disabling ABP's auto anti forgery validation feature, because
                 * when we run the application in "localhost" domain, it will share
                 * the cookies between other applications (like the authentication server)
                 * and anti-forgery validation filter uses the other application's tokens
                 * which will fail the process unnecessarily.
                 */
                options.AutoValidate = false;
            });
        }
    }
    
    private void ConfigureObjectMapper(ServiceConfigurationContext context)
    {
        context.Services.AddMapperlyObjectMapper<KHHubCrawlerSerivceModule>();
    }
    
    private void ConfigureAutoControllers()
    {
        Configure<AbpAspNetCoreMvcOptions>(options =>
        {
            options
                .ConventionalControllers
                .Create(typeof(KHHubCrawlerSerivceModule).Assembly, opts =>
                {
                    opts.RemoteServiceName = CrawlerSerivceRemoteServiceConsts.RemoteServiceName;
                    opts.RootPath = CrawlerSerivceRemoteServiceConsts.ModuleName;
                });
        });
    }
    
    private static void ConfigureSwaggerUI(SwaggerUIOptions options, IConfiguration configuration)
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CrawlerSerivce API");
        options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
        options.OAuthScopes("CrawlerSerivce");
    }
    
    private static bool IsSwaggerEnabled(IConfiguration configuration)
    {
        return bool.Parse(configuration["Swagger:IsEnabled"] ?? "true");
    }

    private void ConfigureDynamicClaims(ServiceConfigurationContext context)
    {
        context.Services.Configure<AbpClaimsPrincipalFactoryOptions>(options =>
        {
            options.IsDynamicClaimsEnabled = true;
        });
    }
}