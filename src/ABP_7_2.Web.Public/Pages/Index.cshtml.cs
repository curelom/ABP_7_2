using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace ABP_7_2.Web.Public.Pages;

public class IndexModel : ABP_7_2PublicPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
