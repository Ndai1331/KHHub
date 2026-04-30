using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Localization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Localization.Resources.AbpUi;
using Microsoft.Extensions.Options;
using KHHub.AdministrationService.Permissions;
using Volo.Abp.Account.AuthorityDelegation;
using Volo.Abp.Account.Localization;
using Volo.Abp.UI.Navigation;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using KHHub.Web.Components.Toolbar.Impersonation;
using Volo.Abp.Users;
using Volo.Abp.OpenIddict.Pro.Web.Menus;
using Volo.Abp.AuditLogging.Web.Navigation;
using Volo.AIManagement.Web.Menus;
using Volo.Abp.LanguageManagement.Navigation;
using KHHub.LanguageService.Localization;
using Volo.Abp.TextTemplateManagement.Web.Navigation;

namespace KHHub.Web.Navigation;

public class KHHubMenuContributor : IMenuContributor
{
    private readonly IConfiguration _configuration;

    public KHHubMenuContributor(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
        else if (context.Menu.Name == StandardMenus.User)
        {
            await ConfigureUserMenuAsync(context);
        }
    }

    private static async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<LanguageServiceResource>();
        //Home
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.Home, l["Menu:Home"], "~/", icon: "fa fa-home", order: 0));
        //HostDashboard
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.HostDashboard, l["Menu:Dashboard"], "~/HostDashboard", icon: "fa fa-line-chart", order: 2).RequirePermissions(AdministrationServicePermissions.Dashboard.Host));
        //Administration
        var administration = context.Menu.GetAdministration();
        administration.Order = 5;
        //Administration->Identity
        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        //Administration->OpenIddict
        administration.SetSubItemOrder(OpenIddictProMenus.GroupName, 3);
        //Administration->Language Management
        administration.SetSubItemOrder(LanguageManagementMenuNames.GroupName, 4);
        //Administration->AI Management
        administration.SetSubItemOrder(AIManagementMenus.GroupName, 5);
        //Administration->Text Template Management
        administration.SetSubItemOrder(TextTemplateManagementMainMenuNames.GroupName, 6);
        //Administration->Audit Logs
        administration.SetSubItemOrder(AbpAuditLoggingMainMenuNames.GroupName, 7);
        //Administration->Settings
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 8);
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.Provinces, context.GetLocalizer<MasterDataServiceResource>()["Menu:Provinces"], url: "/Provinces", icon: "fa fa-file-alt", requiredPermissionName: MasterDataServicePermissions.Provinces.Default));
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.Wards, context.GetLocalizer<MasterDataServiceResource>()["Menu:Wards"], url: "/Wards", icon: "fa fa-file-alt", requiredPermissionName: MasterDataServicePermissions.Wards.Default));
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleCategories, context.GetLocalizer<MasterDataServiceResource>()["Menu:ArticleCategories"], url: "/ArticleCategories", icon: "fa fa-list-alt", requiredPermissionName: MasterDataServicePermissions.ArticleCategories.Default));
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleTags, context.GetLocalizer<MasterDataServiceResource>()["Menu:ArticleTags"], url: "/ArticleTags", icon: "fa fa-tag", requiredPermissionName: MasterDataServicePermissions.ArticleTags.Default));
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.Articles, context.GetLocalizer<MasterDataServiceResource>()["Menu:Articles"], url: "/Articles", icon: "fa fa-newspaper", requiredPermissionName: MasterDataServicePermissions.Articles.Default));
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleTagMappings, context.GetLocalizer<MasterDataServiceResource>()["Menu:ArticleTagMappings"], url: "/ArticleTagMappings", icon: "fa fa-link", requiredPermissionName: MasterDataServicePermissions.ArticleTagMappings.Default));
        context.Menu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleViews, context.GetLocalizer<MasterDataServiceResource>()["Menu:ArticleViews"], url: "/ArticleViews", icon: "fa fa-eye", requiredPermissionName: MasterDataServicePermissions.ArticleViews.Default));
    }

    private Task ConfigureUserMenuAsync(MenuConfigurationContext context)
    {
        var currentUser = context.ServiceProvider.GetRequiredService<ICurrentUser>();
        var options = context.ServiceProvider.GetRequiredService<IOptions<AbpAccountAuthorityDelegationOptions>>();
        var uiResource = context.GetLocalizer<AbpUiResource>();
        var accountResource = context.GetLocalizer<AccountResource>();
        var authServerUrl = _configuration["AuthServer:Authority"] ?? "~";
        var returnUrl = _configuration["App:SelfUrl"] ?? "";
        context.Menu.AddItem(new ApplicationMenuItem("Account.LinkedAccounts", accountResource["LinkedAccounts"], url: "javascript:void(0)", icon: "fa fa-link"));
        if (currentUser.FindImpersonatorUserId() == null && options.Value.EnableDelegatedImpersonation)
        {
            context.Menu.AddItem(new ApplicationMenuItem("Account.AuthorityDelegation", accountResource["AuthorityDelegation"], url: "javascript:void(0)", icon: "fa fa-users"));
        }

        context.Menu.AddItem(new ApplicationMenuItem("Account.Manage", accountResource["MyAccount"], $"{authServerUrl.EnsureEndsWith('/')}Account/Manage", icon: "fa fa-cog", order: 1000, target: "_blank").RequireAuthenticated());
        context.Menu.AddItem(new ApplicationMenuItem("Account.SecurityLogs", accountResource["MySecurityLogs"], $"{authServerUrl.EnsureEndsWith('/')}Account/SecurityLogs", icon: "fa fa-user-shield", target: "_blank").RequireAuthenticated());
        context.Menu.AddItem(new ApplicationMenuItem("Account.Sessions", accountResource["Sessions"], url: $"{authServerUrl.EnsureEndsWith('/')}Account/Sessions", icon: "fa fa-clock", target: "_blank").RequireAuthenticated());
        if (currentUser.FindImpersonatorUserId() != null)
        {
            context.Menu.AddItem(new ApplicationMenuItem("Account.BackToImpersonator", accountResource["BackToImpersonator"], url: "javascript:void(0)", icon: "fa fa-undo", order: int.MaxValue - 1000).UseComponent<ImpersonationViewComponent>());
        }

        context.Menu.AddItem(new ApplicationMenuItem("Account.Logout", uiResource["Logout"], url: "~/Account/Logout", icon: "fa fa-power-off", order: int.MaxValue - 1000).RequireAuthenticated());
        return Task.CompletedTask;
    }
}