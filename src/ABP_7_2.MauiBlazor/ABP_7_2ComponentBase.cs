using ABP_7_2.Localization;
using Volo.Abp.AspNetCore.Components;

namespace ABP_7_2.MauiBlazor;

public abstract class ABP_7_2ComponentBase : AbpComponentBase
{
    protected ABP_7_2ComponentBase()
    {
        LocalizationResource = typeof(ABP_7_2Resource);
    }
}