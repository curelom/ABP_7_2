using ABP_7_2.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace ABP_7_2.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class ABP_7_2Controller : AbpControllerBase
{
    protected ABP_7_2Controller()
    {
        LocalizationResource = typeof(ABP_7_2Resource);
    }
}
