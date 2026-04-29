var builder = DistributedApplication.CreateBuilder(args);

var infrastructureDefaultUser = builder.AddParameter("infrastructure-default-user", "guest", secret: true);
var infrastructureDefaultUserPassword = builder.AddParameter("infrastructure-default-password", "guest", secret: true);

var applicationResources = new Dictionary<string, IResourceBuilder<ProjectResource>>();
var infrastructure = new Dictionary<string, object>();

var databaseServers = builder.AddInfrastructures(infrastructureDefaultUser, infrastructureDefaultUserPassword, infrastructure);
var dbRefs = builder.AddDatabases(databaseServers);

builder.AddMicroservices(infrastructureDefaultUser, infrastructureDefaultUserPassword, applicationResources, dbRefs, infrastructure);

builder.AddAuthServer(infrastructureDefaultUser, infrastructureDefaultUserPassword, applicationResources, dbRefs, infrastructure);

builder.AddGateways(infrastructureDefaultUser, infrastructureDefaultUserPassword, applicationResources, dbRefs, infrastructure);

builder.AddApplications(infrastructureDefaultUser, infrastructureDefaultUserPassword, applicationResources, dbRefs, infrastructure);

builder.AddAdditionalResources(infrastructureDefaultUser, infrastructureDefaultUserPassword, applicationResources, dbRefs, infrastructure);

builder.ConfigureAllEnvironmentVariables(applicationResources);

builder.Build().Run();
