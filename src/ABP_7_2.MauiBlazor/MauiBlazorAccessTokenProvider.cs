using ABP_7_2.MauiBlazor.Settings;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client.IdentityModel.MauiBlazor;

namespace ABP_7_2.MauiBlazor;

[ExposeServices(typeof(IAbpMauiAccessTokenProvider))]
public class MauiBlazorAccessTokenProvider : IAbpMauiAccessTokenProvider , ITransientDependency
{
    private readonly IABP_7_2ApplicationSettingService _aBP_7_2ApplicationSettingService;

    public MauiBlazorAccessTokenProvider(IABP_7_2ApplicationSettingService aBP_7_2ApplicationSettingService)
    {
        _aBP_7_2ApplicationSettingService = aBP_7_2ApplicationSettingService;
    }

    public async Task<string> GetAccessTokenAsync()
    {
        return await _aBP_7_2ApplicationSettingService.GetAccessTokenAsync();
    }
}
