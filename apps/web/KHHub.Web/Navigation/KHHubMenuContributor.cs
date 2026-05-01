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
        var md = context.GetLocalizer<MasterDataServiceResource>();
        var masterDataCatalogMenu = new ApplicationMenuItem(KHHubMenus.MasterDataCatalogGroup, md["Menu:MasterDataCatalogGroup"], url: "#", icon: "fa fa-map", order: 6);
        masterDataCatalogMenu.AddItem(new ApplicationMenuItem(KHHubMenus.Provinces, md["Menu:Provinces"], url: "/Provinces", icon: "fa fa-map-marker", requiredPermissionName: MasterDataServicePermissions.Provinces.Default));
        masterDataCatalogMenu.AddItem(new ApplicationMenuItem(KHHubMenus.Wards, md["Menu:Wards"], url: "/Wards", icon: "fa fa-map-marker", requiredPermissionName: MasterDataServicePermissions.Wards.Default));
        context.Menu.AddItem(masterDataCatalogMenu);
        var articleContentMenu = new ApplicationMenuItem(KHHubMenus.ArticleContentGroup, md["Menu:ArticleContentGroup"], url: "#", icon: "fa fa-newspaper", order: 7);
        articleContentMenu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleCategories, md["Menu:ArticleCategories"], url: "/ArticleCategories", icon: "fa fa-list-alt", requiredPermissionName: MasterDataServicePermissions.ArticleCategories.Default));
        articleContentMenu.AddItem(new ApplicationMenuItem(KHHubMenus.Articles, md["Menu:Articles"], url: "/Articles", icon: "fa fa-file-text", requiredPermissionName: MasterDataServicePermissions.Articles.Default));
        articleContentMenu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleTags, md["Menu:ArticleTags"], url: "/ArticleTags", icon: "fa fa-tag", requiredPermissionName: MasterDataServicePermissions.ArticleTags.Default));
        articleContentMenu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleTagMappings, md["Menu:ArticleTagMappings"], url: "/ArticleTagMappings", icon: "fa fa-link", requiredPermissionName: MasterDataServicePermissions.ArticleTagMappings.Default));
        articleContentMenu.AddItem(new ApplicationMenuItem(KHHubMenus.ArticleViews, md["Menu:ArticleViews"], url: "/ArticleViews", icon: "fa fa-eye", requiredPermissionName: MasterDataServicePermissions.ArticleViews.Default));
        context.Menu.AddItem(articleContentMenu);
        var placesMenu = new ApplicationMenuItem(KHHubMenus.PlacesGroup, md["Menu:PlacesGroup"], url: "#", icon: "fa fa-map-marker-alt", order: 8);
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.PlaceCategories, md["Menu:PlaceCategories"], url: "/PlaceCategories", icon: "fa fa-sitemap", requiredPermissionName: MasterDataServicePermissions.PlaceCategories.Default));
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.PlaceTags, md["Menu:PlaceTags"], url: "/PlaceTags", icon: "fa fa-tags", requiredPermissionName: MasterDataServicePermissions.PlaceTags.Default));
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.Places, md["Menu:Places"], url: "/Places", icon: "fa fa-map-pin", requiredPermissionName: MasterDataServicePermissions.Places.Default));
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.PlaceTagMappings, md["Menu:PlaceTagMappings"], url: "/PlaceTagMappings", icon: "fa fa-link", requiredPermissionName: MasterDataServicePermissions.PlaceTagMappings.Default));
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.PlaceReviews, md["Menu:PlaceReviews"], url: "/PlaceReviews", icon: "fa fa-star", requiredPermissionName: MasterDataServicePermissions.PlaceReviews.Default));
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.PlaceFavorites, md["Menu:PlaceFavorites"], url: "/PlaceFavorites", icon: "fa fa-heart", requiredPermissionName: MasterDataServicePermissions.PlaceFavorites.Default));
        placesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.PlaceViews, md["Menu:PlaceViews"], url: "/PlaceViews", icon: "fa fa-eye", requiredPermissionName: MasterDataServicePermissions.PlaceViews.Default));
        context.Menu.AddItem(placesMenu);


        var mediaFilesMenu = new ApplicationMenuItem(KHHubMenus.MediaFilesGroup, md["Menu:MediaFilesGroup"], url: "#", icon: "fa fa-folder-open", order: 9);
        mediaFilesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.MediaFiles, md["Menu:MediaFilesFiles"], url: "/MediaFiles", icon: "fa fa-file", requiredPermissionName: MasterDataServicePermissions.MediaFiles.Default));
        mediaFilesMenu.AddItem(new ApplicationMenuItem(KHHubMenus.EntityFiles, md["Menu:EntityFiles"], url: "/EntityFiles", icon: "fa fa-paperclip", requiredPermissionName: MasterDataServicePermissions.EntityFiles.Default));
        context.Menu.AddItem(mediaFilesMenu);
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