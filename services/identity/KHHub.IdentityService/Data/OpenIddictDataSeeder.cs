using OpenIddict.Abstractions;
using OpenIddict.Core;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;
using Volo.Abp.MultiTenancy;
using Volo.Abp.OpenIddict;
using Volo.Abp.OpenIddict.Applications;
using Volo.Abp.OpenIddict.Scopes;
using Volo.Abp.Uow;

namespace KHHub.IdentityService.Data;

public class OpenIddictDataSeeder : OpenIddictDataSeedContributorBase, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly OpenIddictCoreOptions _openIddictCoreOptions;

    public OpenIddictDataSeeder(
        IConfiguration configuration,
        IOpenIddictApplicationRepository openIddictApplicationRepository,
        IAbpApplicationManager applicationManager,
        IOpenIddictScopeRepository openIddictScopeRepository,
        IOpenIddictScopeManager scopeManager,
        ICurrentTenant currentTenant,
        IOptions<OpenIddictCoreOptions> openIddictCoreOptions)
        : base(configuration, openIddictApplicationRepository, applicationManager, openIddictScopeRepository, scopeManager)
    {
        _currentTenant = currentTenant;
        _openIddictCoreOptions = openIddictCoreOptions.Value;
    }

    [UnitOfWork(isTransactional: true)]
    public virtual async Task SeedAsync()
    {
        using (_currentTenant.Change(null))
        {
            var oldCacheValue = _openIddictCoreOptions.DisableEntityCaching;
            _openIddictCoreOptions.DisableEntityCaching = true;
            try
            {
                await CreateApiScopesAsync();
                await CreateClientsAsync();
                await CreateSwaggerClientsAsync();
            }
            finally
            {
                _openIddictCoreOptions.DisableEntityCaching = oldCacheValue;
            }
        }
    }

    private async Task CreateApiScopesAsync()
    {
        await CreateScopesAsync("AuthServer");
        await CreateScopesAsync("IdentityService");
        await CreateScopesAsync("AdministrationService");
        await CreateScopesAsync("AuditLoggingService");
        await CreateScopesAsync("GdprService");
        await CreateScopesAsync("AIManagementService");
        await CreateScopesAsync("LanguageService");
    }

    private async Task CreateSwaggerClientsAsync()
    {
        await CreateSwaggerClientAsync("SwaggerTestUI", new[]
        {
            "AuthServer",
            "IdentityService",
            "AuditLoggingService",
            "GdprService",
            "AIManagementService",
            "LanguageService",
            "AdministrationService"
        });
    }

    private async Task CreateSwaggerClientAsync(string clientId, string[] scopes)
    {
        var commonScopes = new List<string>
        {
            OpenIddictConstants.Permissions.Scopes.Address,
            OpenIddictConstants.Permissions.Scopes.Email,
            OpenIddictConstants.Permissions.Scopes.Phone,
            OpenIddictConstants.Permissions.Scopes.Profile,
            OpenIddictConstants.Permissions.Scopes.Roles
        };

        // Swagger Client
        //TODO: Ensure that all root urls are configured in the appsettings.json file properly
        var webGatewaySwaggerRootUrl = Configuration["OpenIddict:Applications:WebGateway:RootUrl"]!.TrimEnd('/'); 
        var mobileGatewaySwaggerRootUrl = Configuration["OpenIddict:Applications:MobileGateway:RootUrl"]!.TrimEnd('/');
        var authServerRootUrl = Configuration["OpenIddict:Resources:AuthServer:RootUrl"]!.TrimEnd('/');
        var identityServiceRootUrl = Configuration["OpenIddict:Resources:IdentityService:RootUrl"]!.TrimEnd('/');
        var administrationServiceRootUrl = Configuration["OpenIddict:Resources:AdministrationService:RootUrl"]!.TrimEnd('/');
        var auditLoggingServiceServiceRootUrl = Configuration["OpenIddict:Resources:AuditLoggingService:RootUrl"]!.TrimEnd('/'); 
        var gdprServiceServiceRootUrl = Configuration["OpenIddict:Resources:GdprService:RootUrl"]!.TrimEnd('/'); 
        var languageServiceServiceRootUrl = Configuration["OpenIddict:Resources:LanguageService:RootUrl"]!.TrimEnd('/'); 
        var aiManagementServiceServiceRootUrl = Configuration["OpenIddict:Resources:AIManagementService:RootUrl"]!.TrimEnd('/'); 

        await CreateOrUpdateApplicationAsync(
            applicationType: OpenIddictConstants.ApplicationTypes.Web,
            name: clientId,
            type:  OpenIddictConstants.ClientTypes.Public,
            consentType: OpenIddictConstants.ConsentTypes.Implicit,
            displayName: "Swagger Test Client",
            secret: null,
            grantTypes: new List<string>
            {
                OpenIddictConstants.GrantTypes.AuthorizationCode,
            },
            scopes: commonScopes.Union(scopes).ToList(),
            redirectUris: new List<string> {
                $"{webGatewaySwaggerRootUrl}/swagger/oauth2-redirect.html",
                $"{mobileGatewaySwaggerRootUrl}/swagger/oauth2-redirect.html",
                $"{authServerRootUrl}/swagger/oauth2-redirect.html",
                $"{identityServiceRootUrl}/swagger/oauth2-redirect.html",
                $"{auditLoggingServiceServiceRootUrl}/swagger/oauth2-redirect.html",
                $"{gdprServiceServiceRootUrl}/swagger/oauth2-redirect.html",
                $"{languageServiceServiceRootUrl}/swagger/oauth2-redirect.html",
                $"{aiManagementServiceServiceRootUrl}/swagger/oauth2-redirect.html",
                $"{administrationServiceRootUrl}/swagger/oauth2-redirect.html"
            },
            clientUri: webGatewaySwaggerRootUrl,
            logoUri: "/images/clients/swagger.svg"
        );
    }

    private async Task CreateScopesAsync(string name)
    {
        await CreateScopesAsync(new OpenIddictScopeDescriptor 
        {
            Name = name,
            DisplayName = name + " API",
            Resources =
            {
                name
            }
        });
    }

    private async Task CreateClientsAsync()
    {
        var commonScopes = new List<string>
        {
            OpenIddictConstants.Permissions.Scopes.Address,
            OpenIddictConstants.Permissions.Scopes.Email,
            OpenIddictConstants.Permissions.Scopes.Phone,
            OpenIddictConstants.Permissions.Scopes.Profile,
            OpenIddictConstants.Permissions.Scopes.Roles
        };

        //Web Client
        var webClientRootUrl = Configuration["OpenIddict:Applications:Web:RootUrl"]!.EnsureEndsWith('/');
        await CreateOrUpdateApplicationAsync(
            applicationType: OpenIddictConstants.ApplicationTypes.Web,
            name: "Web",
            type: OpenIddictConstants.ClientTypes.Confidential,
            consentType: OpenIddictConstants.ConsentTypes.Implicit,
            displayName: "Web Client",
            secret: "1q2w3e*",
            grantTypes: new List<string> //Hybrid flow
            {
                OpenIddictConstants.GrantTypes.AuthorizationCode,
                OpenIddictConstants.GrantTypes.Implicit
            },
            scopes: commonScopes.Union(new[]
            {
                "AuthServer", 
                "IdentityService",
                "AuditLoggingService",
                "GdprService",
                "LanguageService",
                "AIManagementService",
                "AdministrationService"
            }).ToList(),
            redirectUris: new List<string> { $"{webClientRootUrl}signin-oidc" },
            postLogoutRedirectUris: new List<string>() { $"{webClientRootUrl}signout-callback-oidc" },
            clientUri: webClientRootUrl,
            logoUri: "/images/clients/aspnetcore.svg"
        );


        // ReactNative Mobile Client
        var reactNativeRootUrl = Configuration["OpenIddict:Applications:ReactNative:RootUrl"]!.EnsureEndsWith('/');
        if (!reactNativeRootUrl.IsNullOrWhiteSpace())
        {
            await CreateOrUpdateApplicationAsync(
                applicationType: OpenIddictConstants.ApplicationTypes.Native,
                name: "ReactNative",
                type: OpenIddictConstants.ClientTypes.Public,
                consentType: OpenIddictConstants.ConsentTypes.Implicit,
                displayName: "ReactNative Mobile Application",
                secret: null,
                grantTypes: new List<string> {
                    OpenIddictConstants.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.GrantTypes.Password,
                    OpenIddictConstants.GrantTypes.ClientCredentials,
                    OpenIddictConstants.GrantTypes.RefreshToken
                },
                scopes: commonScopes.Union(new[]
                {
                    "AuthServer",
                    "IdentityService",
                    "AuditLoggingService",
                    "GdprService",
                    "LanguageService",
                    "AIManagementService",
                    "AdministrationService"
                }).ToList(),
                redirectUris: new List<string> { reactNativeRootUrl },
                postLogoutRedirectUris: new List<string> { reactNativeRootUrl }
            );
        }
    }
}
