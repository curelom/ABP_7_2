using ABP_7_2.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace ABP_7_2.Web.Public.Pages;

/* Inherit your Page Model classes from this class.
 */
public abstract class ABP_7_2PublicPageModel : AbpPageModel
{
    protected ABP_7_2PublicPageModel()
    {
        LocalizationResourceType = typeof(ABP_7_2Resource);
    }
}
