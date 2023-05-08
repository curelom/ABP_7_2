using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Web.Public;

[Dependency(ReplaceServices = true)]
public class ABP_7_2BrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "ABP_7_2";
}
