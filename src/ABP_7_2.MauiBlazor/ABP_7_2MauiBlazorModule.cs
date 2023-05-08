﻿using ABP_7_2.MauiBlazor.OAuth;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Configuration;
using ABP_7_2.MauiBlazor.Navigation;
using ABP_7_2.MultiTenancy;
using Volo.Abp.Account.Pro.Admin.Blazor;
using Volo.Abp.AspNetCore.Components.MauiBlazor.LeptonXTheme;
using Volo.Abp.AspNetCore.Components.Web;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc.Client;
using Volo.Abp.AuditLogging.Blazor;
using Volo.Abp.Autofac;
using Volo.Abp.Identity.Pro.Blazor;
using Volo.Abp.LanguageManagement.Blazor;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Pro.Blazor;
using Volo.Abp.SettingManagement.Blazor;
using Volo.Abp.TextTemplateManagement.Blazor;
using Volo.Abp.UI.Navigation;
using Volo.Saas.Host.Blazor;
using Volo.Abp.Http.Client;
using Volo.Abp.MultiTenancy;
using Volo.Abp.UI;
using Localization.Resources.AbpUi;
using Volo.Abp.Account.Localization;
using Volo.Abp.Gdpr.Blazor;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Options;
using Volo.Abp.AutoMapper;
using Volo.Abp.LeptonX.Shared;
using Volo.Abp.Localization;
using Volo.CmsKit.Pro.Admin.Blazor;

namespace ABP_7_2.MauiBlazor;

    [DependsOn(
        typeof(AbpAspNetCoreMvcClientCommonModule),
        typeof(AbpUiModule),
        typeof(AbpAspNetCoreComponentsWebModule),
        typeof(AbpAccountAdminBlazorModule),
        typeof(AbpAutofacModule),
        typeof(AbpAspNetCoreComponentsMauiBlazorLeptonXThemeModule),
        typeof(AbpAuditLoggingBlazorModule),
        typeof(AbpIdentityProBlazorModule),
        typeof(AbpOpenIddictProBlazorModule),
        typeof(AbpSettingManagementBlazorModule),
        typeof(LanguageManagementBlazorModule),
        typeof(SaasHostBlazorModule),
        typeof(TextTemplateManagementBlazorModule),
        typeof(ABP_7_2HttpApiClientModule),
        typeof(CmsKitProAdminBlazorModule),
        typeof(AbpGdprBlazorModule)
        )]
public class ABP_7_2MauiBlazorModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        #if DEBUG
                PreConfigure<AbpHttpClientBuilderOptions>(options =>
                {
                    options.ProxyClientBuildActions.Add((_, clientBuilder) =>
                    {
                        clientBuilder.ConfigurePrimaryHttpMessageHandler(GetInsecureHandler);
                    });
                });
        #endif
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var configuration = context.Services.GetConfiguration();

        context.Services.AddMauiBlazorWebView();

        ConfigureMenu(context);
        ConfigureRouter(context);
        ConfigureBlazorise(context);
        ConfigureAuthentication(context, configuration);
        ConfigureMultiTenancy();
        ConfigureAutoMapper(context);
        ConfigureTheme();

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<AccountResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }

    private void ConfigureTheme()
    {
        Configure<LeptonXThemeOptions>(options =>
        {
            options.DefaultStyle = LeptonXStyleNames.System;
        });
    }

    private void ConfigureMenu(ServiceConfigurationContext context)
    {
        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new ABP_7_2MenuContributor(context.Services.GetConfiguration()));
        });
    }

    private void ConfigureRouter(ServiceConfigurationContext context)
    {
        Configure<AbpRouterOptions>(options => { options.AppAssembly = typeof(ABP_7_2MauiBlazorModule).Assembly; });
    }

    private void ConfigureBlazorise(ServiceConfigurationContext context)
    {
        context.Services.AddBlazorise(options =>
        {
            options.Debounce = true;
            options.DebounceInterval = 50;
        });
        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
    }

    private void ConfigureAuthentication(ServiceConfigurationContext context, IConfiguration configuration)
    {
        context.Services.AddAuthorizationCore();
        context.Services.AddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();
        context.Services.AddSingleton<ExternalAuthService>();
        Configure<OidcClientOptions>(configuration.GetSection("OAuthConfig"));

        context.Services.AddTransient<OidcClient>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<OidcClientOptions>>().Value;
            options.Browser = sp.GetRequiredService<MauiAuthenticationBrowser>();

#if DEBUG
            options.BackchannelHandler = GetInsecureHandler();
#endif

            return new OidcClient(options);
        });
    }

    private void ConfigureMultiTenancy()
    {
        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });
    }

    private void ConfigureAutoMapper(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<ABP_7_2MauiBlazorModule>();
        });
    }

    //https://docs.microsoft.com/en-us/xamarin/cross-platform/deploy-test/connect-to-local-web-services#bypass-the-certificate-security-check
    private static HttpMessageHandler GetInsecureHandler()
    {
#if ANDROID
        var handler = new HttpClientHandler()
        {
           UseCookies = false
        };
        handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
        {
            if (cert is { Issuer: "CN=localhost" })
            {
                return true;
            }

            return errors == System.Net.Security.SslPolicyErrors.None;
        };
        return handler;
#elif IOS
        var handler = new NSUrlSessionHandler
        {
            UseCookies = false,
            TrustOverrideForUrl = (sender, url, trust) => url.StartsWith("https://localhost")
        };
        return handler;
#elif WINDOWS || MACCATALYST
        return new HttpClientHandler()
        {
            UseCookies = false
        };
#else
         throw new PlatformNotSupportedException("Only Android, iOS, MacCatalyst, and Windows supported.");
#endif
    }
}
