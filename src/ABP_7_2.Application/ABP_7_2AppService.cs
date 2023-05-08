using ABP_7_2.Localization;
using Volo.Abp.Application.Services;

namespace ABP_7_2;

/* Inherit your application services from this class.
 */
public abstract class ABP_7_2AppService : ApplicationService
{
    protected ABP_7_2AppService()
    {
        LocalizationResource = typeof(ABP_7_2Resource);
    }
}
