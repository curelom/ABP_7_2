using IdentityModel;
using IdentityModel.OidcClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ABP_7_2.Localization;
using ABP_7_2.Maui.Oidc;
using Volo.Abp.Account.Localization;
using Volo.Abp.Autofac;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.Security.Claims;

namespace ABP_7_2.Maui;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpHttpClientIdentityModelModule),
    typeof(ABP_7_2HttpApiClientModule)
)]
public class ABP_7_2MauiModule : AbpModule
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

        MapClaimTypes();
        ConfigureOidcClient(context, configuration);
        ConfigureLocalization();
    }

    private void ConfigureOidcClient(ServiceConfigurationContext context, IConfiguration configuration)
    {
        Configure<OidcClientOptions>(configuration.GetSection("Oidc:Options"));

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

    private void ConfigureLocalization()
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<ABP_7_2Resource>()
                .AddBaseTypes(typeof(IdentityResource))
                .AddBaseTypes(typeof(AccountResource));
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Languages.Add(new LanguageInfo("ar", "ar", "العربية", "ae"));
            options.Languages.Add(new LanguageInfo("cs", "cs", "Čeština"));
            options.Languages.Add(new LanguageInfo("en", "en", "English"));
            options.Languages.Add(new LanguageInfo("en-GB", "en-GB", "English (UK)"));
            options.Languages.Add(new LanguageInfo("hu", "hu", "Magyar"));
            options.Languages.Add(new LanguageInfo("fi", "fi", "Finnish", "fi"));
            options.Languages.Add(new LanguageInfo("fr", "fr", "Français", "fr"));
            options.Languages.Add(new LanguageInfo("hi", "hi", "Hindi", "in"));
            options.Languages.Add(new LanguageInfo("it", "it", "Italiano", "it"));
            options.Languages.Add(new LanguageInfo("pt-BR", "pt-BR", "Português"));
            options.Languages.Add(new LanguageInfo("ru", "ru", "Русский", "ru"));
            options.Languages.Add(new LanguageInfo("sk", "sk", "Slovak", "sk"));
            options.Languages.Add(new LanguageInfo("tr", "tr", "Türkçe"));
            options.Languages.Add(new LanguageInfo("zh-Hans", "zh-Hans", "简体中文"));
            options.Languages.Add(new LanguageInfo("zh-Hant", "zh-Hant", "繁體中文"));
            options.Languages.Add(new LanguageInfo("de-DE", "de-DE", "Deutsch", "de"));
            options.Languages.Add(new LanguageInfo("es", "es", "Español"));
        });
    }

    //https://docs.microsoft.com/en-us/xamarin/cross-platform/deploy-test/connect-to-local-web-services#bypass-the-certificate-security-check
    private static HttpMessageHandler GetInsecureHandler()
    {
#if ANDROID
        var handler = new HttpClientHandler();
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
            TrustOverrideForUrl = (sender, url, trust) => url.StartsWith("https://localhost")
        };
        return handler;
#elif WINDOWS || MACCATALYST
            return new HttpClientHandler();
#else
         throw new PlatformNotSupportedException("Only Android, iOS, MacCatalyst, and Windows supported.");
#endif
    }

    private static void MapClaimTypes()
    {
        AbpClaimTypes.UserName = JwtClaimTypes.PreferredUserName;
        AbpClaimTypes.Name = JwtClaimTypes.GivenName;
        AbpClaimTypes.SurName = JwtClaimTypes.FamilyName;
        AbpClaimTypes.UserId = JwtClaimTypes.Subject;
        AbpClaimTypes.Role = JwtClaimTypes.Role;
        AbpClaimTypes.Email = JwtClaimTypes.Email;
    }
}
