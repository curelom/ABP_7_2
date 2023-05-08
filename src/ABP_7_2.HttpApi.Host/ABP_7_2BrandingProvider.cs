using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace ABP_7_2;

[Dependency(ReplaceServices = true)]
public class ABP_7_2BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ABP_7_2";
}
