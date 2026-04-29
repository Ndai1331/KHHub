using System.Net.Sockets;
using KHHub.AppHost;
using KHHub.AppHost.OpenTelemetryCollector;

public static class AppHostExtensions
{
    public static DatabaseServers AddInfrastructures(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword,
        Dictionary<string, object> infrastructure)
    {
        var postgres = builder.AddPostgres("postgresql").WithPgAdmin(configureContainer: resourceBuilder =>
        {
            resourceBuilder.WithHostPort(8083);
        });
       
        var redis = builder.AddRedis("redis").WithRedisInsight(configureContainer: resourceBuilder =>
        {
            resourceBuilder.WithHostPort(5540);
        });

        var rabbitMq = builder.AddRabbitMQ("rabbitmq", userName: infrastructureDefaultUser, password: infrastructureDefaultUserPassword)
            .WithManagementPlugin(port: 15672);
        
        var elasticsearch = builder.AddElasticsearch("elasticsearch")
            .WithEnvironment("discovery.type", "single-node")
            .WithEnvironment("cluster.name", "docker-cluster")
            .WithEnvironment("node.name", "docker-node")
            .WithEnvironment("ES_JAVA_OPTS", "-Xms512m -Xmx512m")
            .WithEnvironment("xpack.security.enabled", "false")
            .WithEnvironment("xpack.security.http.ssl.enabled", "false")
            .WithEnvironment("xpack.security.transport.ssl.enabled", "false")
            .WithEnvironment("network.host", "0.0.0.0")
            .WithEnvironment("http.port", "9200")
            .WithEnvironment("transport.host", "localhost")
            .WithEnvironment("bootstrap.memory_lock", "true")
            .WithEnvironment("cluster.routing.allocation.disk.threshold_enabled", "false")
            .WithEndpoint(
                "http",
                e =>
                {
                    e.TargetPort = 9200;
                    e.Port = 9200;
                    e.IsProxied = true;
                    e.IsExternal = false;
                    e.UriScheme = "http";
                    e.Protocol = ProtocolType.Tcp;
                })
            .WithEndpoint(
                "internal",
                e =>
                {
                    e.TargetPort = 9300;
                    e.Port = 9300;
                    e.IsProxied = true;
                    e.IsExternal = false;
                    e.UriScheme = "http";
                    e.Protocol = ProtocolType.Tcp;
                });

        var kibana = builder.AddContainer("kibana", "docker.elastic.co/kibana/kibana", "8.10.4")
            .WithEnvironment("ELASTICSEARCH_HOSTS", elasticsearch.GetEndpoint("http"))
            .WithReference(elasticsearch)
            .WaitFor(elasticsearch)
            .WithEndpoint(port: 5601, targetPort: 5601, name: "http", isProxied: true, isExternal: true, scheme: "http");
        
        var prometheus = builder.AddContainer("prometheus", "prom/prometheus", "v2.47.2")
            .WithBindMount("../../../etc/docker/containers/prometheus/prometheus.yml", "/etc/prometheus/prometheus.yml")
            .WithArgs(
                "--config.file=/etc/prometheus/prometheus.yml",
                "--storage.tsdb.path=/prometheus",
                "--web.console.libraries=/usr/share/prometheus/console_libraries",
                "--web.console.templates=/usr/share/prometheus/consoles",
                "--web.enable-remote-write-receiver")
            .WithEndpoint(port: 9090, targetPort: 9090, name: "http", scheme:"http");
        
        var grafana = builder.AddContainer("grafana", "grafana/grafana")
            .WithEnvironment("GF_INSTALL_PLUGINS", "grafana-clock-panel,grafana-simple-json-datasource")
            .WithEnvironment("GF_FEATURE_TOGGLES_ENABLE", "traceqlEditor")
            .WithBindMount("../../../etc/docker/containers/grafana/config", "/etc/grafana", isReadOnly: true)
            .WithBindMount("../../../etc/docker/containers/grafana/dashboards", "/var/lib/grafana/dashboards", isReadOnly: true)
            .WithEnvironment("PROMETHEUS_ENDPOINT", prometheus.GetEndpoint("http"))
            .WithEndpoint(port: 3001, targetPort: 3000, name: "http", scheme:"http");
      
        var jaeger = builder.AddContainer("jaeger-all-in-one", "jaegertracing/all-in-one")
            .WithEndpoint(
                port: 6831,
                targetPort: 6831,
                name: "agent",
                protocol: ProtocolType.Udp,
                isProxied: true,
                isExternal: false)
            .WithEndpoint(port: 16686, targetPort: 16686, name: "http", isProxied: true, isExternal: true, scheme:"http")
            .WithEndpoint(port: 14268, targetPort: 14268, name: "collector", isProxied: true, isExternal: false)
            .WithEndpoint(port: 14317, targetPort: 4317, name: "otlp-grpc", isProxied: true, isExternal: false)
            .WithEndpoint(port: 14318, targetPort: 4318, name: "otlp-http", isProxied: true, isExternal: false);
        
        builder.AddOpenTelemetryCollector("otelcollector", "../../../etc/docker/containers/otel/otel-collector-config.yaml")
            .WithEnvironment("PROMETHEUS_ENDPOINT", $"{prometheus.GetEndpoint("http")}/api/v1/otlp")
            .WithEnvironment("ELASTICSEARCH_ENDPOINT", elasticsearch.GetEndpoint("http"))
            .WithEnvironment("JAEGER_ENDPOINT", jaeger.GetEndpoint("collector"));        

        infrastructure["Redis"] = redis;
        infrastructure["RabbitMq"] = rabbitMq;
        infrastructure["Elasticsearch"] = elasticsearch;

        return new DatabaseServers(Postgres: postgres);
    }

    public static DatabaseReferences AddDatabases(
        this IDistributedApplicationBuilder builder, 
        DatabaseServers databaseServers)
    {
        var administrationDb = databaseServers.Postgres.AddDatabase("AdministrationService", "KHHub_Administration");
        var identityDb = databaseServers.Postgres.AddDatabase("IdentityService", "KHHub_Identity");
        var blobStoringDb = databaseServers.Postgres.AddDatabase("AbpBlobStoring", "KHHub_BlobStoring");

        var auditLoggingDb = databaseServers.Postgres.AddDatabase("AuditLoggingService", "KHHub_AuditLogging");

        var gdprDb = databaseServers.Postgres.AddDatabase("GdprService", "KHHub_Gdpr");

        var aiManagementDb = databaseServers.Postgres.AddDatabase("AIManagementService", "KHHub_AIManagement");
        var languageManagementDb = databaseServers.Postgres.AddDatabase("LanguageService", "KHHub_Language");

        return new DatabaseReferences(
            AdministrationDb: administrationDb,
            IdentityDb: identityDb,
            BlobStoringDb: blobStoringDb,
            AuditLoggingDb: auditLoggingDb,
            GdprDb: gdprDb,
            AIManagementDb: aiManagementDb,
            LanguageManagementDb: languageManagementDb
        );
    }

    public static void AddMicroservices(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword,
        Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources,
        DatabaseReferences databases,
        Dictionary<string, object> infrastructure)
    {
        var redis = (IResourceBuilder<RedisResource>)infrastructure["Redis"];
        var rabbitMq = (IResourceBuilder<RabbitMQServerResource>)infrastructure["RabbitMq"];
        var elasticsearch = (IResourceBuilder<ElasticsearchResource>)infrastructure["Elasticsearch"];
        
        var administration = builder
            .AddProject<Projects.KHHub_AdministrationService>("administration", "KHHub.AdministrationService")
            .WaitFor(databases.AdministrationDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.BlobStoringDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch);
        applicationResources["Administration"] = administration;

        var identity = builder
            .AddProject<Projects.KHHub_IdentityService>("identity", "KHHub.IdentityService")
            .WaitFor(databases.AdministrationDb)
            .WaitFor(databases.IdentityDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.BlobStoringDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch);
        applicationResources["Identity"] = identity;

        var auditLogging = builder
            .AddProject<Projects.KHHub_AuditLoggingService>("auditlogging", "KHHub.AuditLoggingService")
            .WaitFor(databases.AdministrationDb)
            .WaitFor(databases.AuditLoggingDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.BlobStoringDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch);
        applicationResources["AuditLogging"] = auditLogging;

        var gdpr = builder
            .AddProject<Projects.KHHub_GdprService>("gdpr", "KHHub.GdprService")
            .WaitFor(databases.AdministrationDb)
            .WaitFor(databases.GdprDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.BlobStoringDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.GdprDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch);
        applicationResources["Gdpr"] = gdpr;

        var aiManagement = builder
            .AddProject<Projects.KHHub_AIManagementService>("aimanagement", "KHHub.AIManagementService")
            .WaitFor(databases.AdministrationDb)
            .WaitFor(databases.AIManagementDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.AIManagementDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch);
        applicationResources["AIManagement"] = aiManagement;

        var language = builder
            .AddProject<Projects.KHHub_LanguageService>("language", "KHHub.LanguageService")
            .WaitFor(databases.AdministrationDb)
            .WaitFor(databases.LanguageManagementDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.BlobStoringDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.GdprDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch);
        applicationResources["LanguageManagement"] = language;
    }

    public static void AddAuthServer(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword,
        Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources,
        DatabaseReferences databases,
        Dictionary<string, object> infrastructure)
    {
        var redis = (IResourceBuilder<RedisResource>)infrastructure["Redis"];
        var rabbitMq = (IResourceBuilder<RabbitMQServerResource>)infrastructure["RabbitMq"];
        var elasticsearch = (IResourceBuilder<ElasticsearchResource>)infrastructure["Elasticsearch"];
        
        var authServer = builder
            .AddProject<Projects.KHHub_AuthServer>("authserver", "KHHub.AuthServer")
            .WaitFor(databases.IdentityDb)
            .WaitFor(redis)
            .WaitFor(rabbitMq)
            .WithReference(databases.AdministrationDb)
            .WithReference(databases.IdentityDb)
            .WithReference(databases.BlobStoringDb)
            .WithReference(databases.AuditLoggingDb)
            .WithReference(databases.LanguageManagementDb)
            .ConfigureRabbitMq(rabbitMq, infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis(redis)
            .ConfigureElasticSearch(elasticsearch)
            .WithExternalHttpEndpoints();
        applicationResources["AuthServer"] = authServer;
    }
    
    public static void AddGateways(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword,
        Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources,
        DatabaseReferences databases,
        Dictionary<string, object> infrastructure)
    {
        var webGateway = builder.AddProject<Projects.KHHub_WebGateway>("webgateway", "KHHub.WebGateway")
            .WaitFor((IResourceBuilder<RedisResource>)infrastructure["Redis"])
            .WaitFor((IResourceBuilder<RabbitMQServerResource>)infrastructure["RabbitMq"])
            .WaitFor(applicationResources["Administration"])
            .WaitFor(applicationResources["Identity"])
            .WaitFor(applicationResources["AuditLogging"])
            .WaitFor(applicationResources["Gdpr"])
            .WaitFor(applicationResources["AIManagement"])
            .WaitFor(applicationResources["LanguageManagement"])
            .WaitFor(applicationResources["AuthServer"])
            .ConfigureRabbitMq((IResourceBuilder<ContainerResource>)infrastructure["RabbitMq"], infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis((IResourceBuilder<IResourceWithConnectionString>)infrastructure["Redis"])
            .ConfigureElasticSearch((IResourceBuilder<IResourceWithConnectionString>)infrastructure["Elasticsearch"])
            .WithReference(applicationResources["Administration"])
            .WithReference(applicationResources["Identity"])
            .WithReference(applicationResources["AuditLogging"])
            .WithReference(applicationResources["Gdpr"])
            .WithReference(applicationResources["AIManagement"])
            .WithReference(applicationResources["LanguageManagement"])
            .WithReference(applicationResources["AuthServer"]);
        applicationResources["WebGateway"] = webGateway;

        var mobileGateway = builder.AddProject<Projects.KHHub_MobileGateway>("mobilegateway", "KHHub.MobileGateway")
            .WaitFor((IResourceBuilder<RedisResource>)infrastructure["Redis"])
            .WaitFor((IResourceBuilder<RabbitMQServerResource>)infrastructure["RabbitMq"])
            .WaitFor(applicationResources["Administration"])
            .WaitFor(applicationResources["Identity"])
            .WaitFor(applicationResources["AuthServer"])
            .ConfigureRabbitMq((IResourceBuilder<ContainerResource>)infrastructure["RabbitMq"], infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis((IResourceBuilder<IResourceWithConnectionString>)infrastructure["Redis"])
            .ConfigureElasticSearch((IResourceBuilder<IResourceWithConnectionString>)infrastructure["Elasticsearch"])
            .WithReference(applicationResources["Administration"])
            .WithReference(applicationResources["Identity"])
            .WithReference(applicationResources["AuthServer"])
            .WithExternalHttpEndpoints();
        applicationResources["MobileGateway"] = mobileGateway;
    }
    
    public static void AddApplications(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword,
        Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources,
        DatabaseReferences databases,
        Dictionary<string, object> infrastructure)
    {
        var web = builder
            .AddProject<Projects.KHHub_Web>("web", "KHHub.Web")
            .WaitFor(applicationResources["WebGateway"])
            .WaitFor(applicationResources["AuthServer"])
            .WaitFor((IResourceBuilder<RedisResource>)infrastructure["Redis"])
            .WaitFor((IResourceBuilder<RabbitMQServerResource>)infrastructure["RabbitMq"])
            .WithReference(applicationResources["Administration"])
            .WithReference(applicationResources["Identity"])
            .WithReference(applicationResources["AuditLogging"])
            .WithReference(applicationResources["Gdpr"])
            .WithReference(applicationResources["AIManagement"])
            .WithReference(applicationResources["LanguageManagement"])
            .WithReference(applicationResources["WebGateway"])
            .WithReference(applicationResources["AuthServer"])
            .ConfigureRabbitMq((IResourceBuilder<ContainerResource>)infrastructure["RabbitMq"], infrastructureDefaultUser, infrastructureDefaultUserPassword)
            .ConfigureRedis((IResourceBuilder<IResourceWithConnectionString>)infrastructure["Redis"])
            .ConfigureElasticSearch((IResourceBuilder<IResourceWithConnectionString>)infrastructure["Elasticsearch"])
            .WithExternalHttpEndpoints();
        applicationResources["Web"] = web;
    }

    public static void ConfigureAllEnvironmentVariables(
        this IDistributedApplicationBuilder builder,
        Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources)
    {
        
        var endpoints = new EndpointConfiguration(
            applicationResources["AuthServer"],
            applicationResources["WebGateway"],
            applicationResources["Web"],
            applicationResources["MobileGateway"]
);
        var environmentConfig = new EnvironmentConfiguration(endpoints);

        environmentConfig.ConfigureMicroservices(applicationResources);
        environmentConfig.ConfigureWebApplication(applicationResources["Web"]);
        environmentConfig.ConfigureAuthServer(applicationResources["AuthServer"], applicationResources);
        environmentConfig.ConfigureWebGateway(applicationResources["WebGateway"]);
        environmentConfig.ConfigureMobileGateway(applicationResources["MobileGateway"]);
    }

    private static IResourceBuilder<ProjectResource> ConfigureRabbitMq(
        this IResourceBuilder<ProjectResource> builder,
        IResourceBuilder<ContainerResource> rabbitMq,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword)
    {
        return builder
            .WithEnvironment("RabbitMQ__Connections__Default__HostName", $"{rabbitMq.GetEndpoint("tcp").Property(EndpointProperty.Host)}")
            .WithEnvironment("RabbitMQ__Connections__Default__Port", $"{rabbitMq.GetEndpoint("tcp").Property(EndpointProperty.Port)}")
            .WithEnvironment("RabbitMQ__Connections__Default__Username", infrastructureDefaultUser)
            .WithEnvironment("RabbitMQ__Connections__Default__Password", infrastructureDefaultUserPassword);
    }

    private static IResourceBuilder<ProjectResource> ConfigureRedis(
        this IResourceBuilder<ProjectResource> builder,
        IResourceBuilder<IResourceWithConnectionString> redis)
    {
        return builder.WithEnvironment("Redis__Configuration", redis);
    }
    
    private static IResourceBuilder<ProjectResource> ConfigureElasticSearch(
        this IResourceBuilder<ProjectResource> builder,
        IResourceBuilder<IResourceWithConnectionString> elasticSearch)
    {
        return builder.WithEnvironment("ElasticSearch__Url", elasticSearch);
    }

    public static void AddAdditionalResources(
        this IDistributedApplicationBuilder builder,
        IResourceBuilder<ParameterResource> infrastructureDefaultUser,
        IResourceBuilder<ParameterResource> infrastructureDefaultUserPassword,
        Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources,
        DatabaseReferences databases,
        Dictionary<string, object> infrastructure)
    {
        var redis = (IResourceBuilder<RedisResource>)infrastructure["Redis"];
        var rabbitMq = (IResourceBuilder<RabbitMQServerResource>)infrastructure["RabbitMq"];
        var elasticsearch = (IResourceBuilder<ElasticsearchResource>)infrastructure["Elasticsearch"];
        
        // This method is intentionally left as a placeholder for dynamic service addition

    }
}

public record EndpointConfiguration(
    IResourceBuilder<ProjectResource> AuthServer,
    IResourceBuilder<ProjectResource> WebGateway,
    IResourceBuilder<IResourceWithServiceDiscovery> Web,
    IResourceBuilder<ProjectResource> MobileGateway
)
{
    public EndpointReference AuthServerEndpoint => AuthServer.GetEndpoint("http");
    
    public EndpointReference WebGatewayEndpoint => WebGateway.GetEndpoint("http");
    public EndpointReference WebEndpoint => Web.GetEndpoint("http");

    public EndpointReference MobileGatewayEndpoint => MobileGateway.GetEndpoint("http");
}

public class EnvironmentConfiguration
{
    private readonly EndpointConfiguration _endpoints;

    public EnvironmentConfiguration(EndpointConfiguration endpoints)
    {
        _endpoints = endpoints;
    }

    public void ConfigureMicroservices(Dictionary<string, IResourceBuilder<ProjectResource>> services)
    {
        var commonEndpointReferenceEnvironments = new Dictionary<string, EndpointReference>
        {
            ["App__CorsOrigins"] = _endpoints.WebGatewayEndpoint,
            ["AuthServer__Authority"] = _endpoints.AuthServerEndpoint,
            ["AuthServer__MetaAddress"] = _endpoints.AuthServerEndpoint
        };
        
        ApplyEnvironments(services["Administration"], commonEndpointReferenceEnvironments);
        ApplyEnvironments(services["Identity"], commonEndpointReferenceEnvironments);
        ApplyEnvironments(services["AuditLogging"], commonEndpointReferenceEnvironments);
        ApplyEnvironments(services["Gdpr"], commonEndpointReferenceEnvironments);
        ApplyEnvironments(services["AIManagement"], commonEndpointReferenceEnvironments);
        ApplyEnvironments(services["LanguageManagement"], commonEndpointReferenceEnvironments);

        services["Administration"].WithEnvironment("RemoteServices__AbpIdentity__BaseUrl", services["Identity"].GetEndpoint("http"));

        services["Identity"].WithEnvironment("App__CorsOrigins", "http://webgateway")
            .WithEnvironment("AuthServer__Authority", _endpoints.AuthServerEndpoint)
            .WithEnvironment("AuthServer__MetaAddress", _endpoints.AuthServerEndpoint)
            .WithEnvironment("OpenIddict__Applications__Web__RootUrl", _endpoints.WebEndpoint)
            .WithEnvironment("OpenIddict__Applications__WebGateway__RootUrl", _endpoints.WebGatewayEndpoint);
    }

    public void ConfigureWebApplication(IResourceBuilder<ProjectResource> web)
    {
        web.WithEnvironment("App__SelfUrl", _endpoints.WebEndpoint)
            .WithEnvironment("RemoteServices__Default__BaseUrl", "http://webgateway")
            .WithEnvironment("AuthServer__Authority", _endpoints.AuthServerEndpoint)
            .WithEnvironment("AuthServer__MetaAddress", _endpoints.AuthServerEndpoint)
            .WithEnvironment("AuthServer__IsOnK8s", "false");
    }     

    public void ConfigureAuthServer(IResourceBuilder<ProjectResource> authServer, Dictionary<string, IResourceBuilder<ProjectResource>> applicationResources)
    {
        var allowedUrls = ReferenceExpression.Create($"{_endpoints.WebEndpoint},{_endpoints.WebGatewayEndpoint},{applicationResources["Administration"].GetEndpoint("http")},{applicationResources["Identity"].GetEndpoint("http")},{_endpoints.MobileGatewayEndpoint},{applicationResources["AuditLogging"].GetEndpoint("http")},{applicationResources["Gdpr"].GetEndpoint("http")},{applicationResources["AIManagement"].GetEndpoint("http")},{applicationResources["LanguageManagement"].GetEndpoint("http")}");

        authServer.WithEnvironment("AuthServer__Authority", _endpoints.AuthServerEndpoint)
            .WithEnvironment("App__RedirectAllowedUrls", allowedUrls)
            .WithEnvironment("App__CorsOrigins", allowedUrls)
            .WithEnvironment("App__SelfUrl", _endpoints.AuthServerEndpoint);
    }

    public void ConfigureWebGateway(IResourceBuilder<ProjectResource> webGateway)
    {
        var reverseProxyConfigs = new Dictionary<string, string>
        {
            ["ReverseProxy__Clusters__AuthServer__Destinations__AuthServer__Address"] = "http://authserver",
            ["ReverseProxy__Clusters__Administration__Destinations__Administration__Address"] = "http://administration",
            ["ReverseProxy__Clusters__Identity__Destinations__Identity__Address"] = "http://identity"
        };

        reverseProxyConfigs.Add("ReverseProxy__Clusters__AuditLogging__Destinations__AuditLogging__Address", "http://auditlogging");
        reverseProxyConfigs.Add("ReverseProxy__Clusters__Gdpr__Destinations__Gdpr__Address", "http://gdpr");
        reverseProxyConfigs.Add("ReverseProxy__Clusters__AIManagement__Destinations__AIManagement__Address", "http://aimanagement");
        reverseProxyConfigs.Add("ReverseProxy__Clusters__Language__Destinations__Language__Address", "http://language");
        ApplyEnvironments(webGateway, reverseProxyConfigs);
        webGateway.WithEnvironment("App__CorsOrigins", $"http://web,{_endpoints.WebEndpoint}");
    }

    public void ConfigureMobileGateway(IResourceBuilder<ProjectResource> mobileGateway)
    {
        var reverseProxyConfigs = new Dictionary<string, string>
        {
            ["ReverseProxy__Clusters__AuthServer__Destinations__AuthServer__Address"] = "http://authserver",
            ["ReverseProxy__Clusters__Administration__Destinations__Administration__Address"] = "http://administration",
            ["ReverseProxy__Clusters__Identity__Destinations__Identity__Address"] = "http://identity"
        };

        ApplyEnvironments(mobileGateway, reverseProxyConfigs);
        mobileGateway.WithEnvironment("App__CorsOrigins", "khhub://");
    }

    private static void ApplyEnvironments(
        IResourceBuilder<ProjectResource> resource,
        Dictionary<string, EndpointReference> environments)
    {
        foreach (var (key, value) in environments)
        {
            resource.WithEnvironment(key, value);
        }
    }
    
    private static void ApplyEnvironments(
        IResourceBuilder<ProjectResource> resource,
        Dictionary<string, string> environments)
    {
        foreach (var (key, value) in environments)
        {
            resource.WithEnvironment(key, value);
        }
    }
}

public record DatabaseServers(
    IResourceBuilder<PostgresServerResource> Postgres
);

public record DatabaseReferences(
    IResourceBuilder<PostgresDatabaseResource> AdministrationDb,
    IResourceBuilder<PostgresDatabaseResource> IdentityDb,
    IResourceBuilder<PostgresDatabaseResource> BlobStoringDb,
    IResourceBuilder<PostgresDatabaseResource> AuditLoggingDb,    IResourceBuilder<PostgresDatabaseResource> GdprDb,    IResourceBuilder<PostgresDatabaseResource> AIManagementDb,    IResourceBuilder<PostgresDatabaseResource> LanguageManagementDb
);