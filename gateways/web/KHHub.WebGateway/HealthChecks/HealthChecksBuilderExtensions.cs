using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;

namespace KHHub.WebGateway.HealthChecks;

public static class HealthChecksBuilderExtensions
{
    public static void AddWebGatewayHealthChecks(this IServiceCollection services)
    {
        // Add your health checks here
        var healthChecksBuilder = services.AddHealthChecks();

        var configuration = services.GetConfiguration();
        var healthCheckUrl = configuration["App:HealthCheckUrl"];

        if (string.IsNullOrEmpty(healthCheckUrl))
        {
            healthCheckUrl = "/health-status";
        }

        services.ConfigureHealthCheckEndpoint(healthCheckUrl);

        var healthUiEndpointUri = ResolveHealthUiCheckEndpointUri(configuration, healthCheckUrl);

        var healthChecksUiBuilder = services.AddHealthChecksUI(settings =>
        {
            // Must be an absolute URI: default HttpClient has Aspire service discovery; a relative path
            // becomes http://[::]/... outside Aspire and breaks the background collector.
            settings.AddHealthCheckEndpoint("WebGateway Health Status", healthUiEndpointUri);
        });

        // Set your HealthCheck UI Storage here
        healthChecksUiBuilder.AddInMemoryStorage();

        services.MapHealthChecksUiEndpoints(options =>
        {
            options.UIPath = "/health-ui";
            options.ApiPath = "/health-api";
        });
    }

    /// <summary>
    /// Builds an absolute URL for HealthChecks UI's HTTP collector.
    /// Uses App:HealthUiCheckUrl when it is already absolute; otherwise derives loopback URL from ASPNETCORE_URLS.
    /// </summary>
    private static string ResolveHealthUiCheckEndpointUri(IConfiguration configuration, string healthCheckPath)
    {
        var explicitUri = configuration["App:HealthUiCheckUrl"];
        if (!string.IsNullOrWhiteSpace(explicitUri) &&
            explicitUri.StartsWith("http", StringComparison.OrdinalIgnoreCase))
        {
            return explicitUri;
        }

        var path = healthCheckPath.EnsureStartsWith('/');
        var urls = configuration["ASPNETCORE_URLS"];
        if (string.IsNullOrWhiteSpace(urls))
        {
            return $"http://127.0.0.1:80{path}";
        }

        var firstUrl = urls.Split(';', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)[0];
        // Kestrel in Docker: http://+:80 → loopback for in-process collector
        var normalized = firstUrl
            .Replace("://+", "://127.0.0.1", StringComparison.Ordinal)
            .Replace("://*", "://127.0.0.1", StringComparison.Ordinal);

        return $"{normalized.TrimEnd('/')}{path}";
    }

    private static IServiceCollection ConfigureHealthCheckEndpoint(this IServiceCollection services, string path)
    {
        services.Configure<AbpEndpointRouterOptions>(options =>
        {
            options.EndpointConfigureActions.Add(endpointContext =>
            {
                endpointContext.Endpoints.MapHealthChecks(
                    new PathString(path.EnsureStartsWith('/')),
                    new HealthCheckOptions
                    {
                        Predicate = _ => true,
                        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                        AllowCachingResponses = false,
                    });
            });
        });

        return services;
    }

    private static IServiceCollection MapHealthChecksUiEndpoints(this IServiceCollection services, Action<global::HealthChecks.UI.Configuration.Options>? setupOption = null)
    {
        services.Configure<AbpEndpointRouterOptions>(routerOptions =>
        {
            routerOptions.EndpointConfigureActions.Add(endpointContext =>
            {
                endpointContext.Endpoints.MapHealthChecksUI(setupOption);
            });
        });

        return services;
    }
}
