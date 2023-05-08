using Microsoft.AspNetCore.Components;
using ABP_7_2.MauiBlazor.OAuth;

namespace ABP_7_2.MauiBlazor.Pages.Account;

public partial class RedirectToLogout
{
    [Inject]
    public ExternalAuthService ExternalAuthService { get; set; } = default!;

    [Inject]
    public NavigationManager NavigationManager { get; set; } = default!;

    protected async override Task OnInitializedAsync()
    {
        if (CurrentUser.IsAuthenticated)
        {
            await ExternalAuthService.SignOutAsync();
            NavigationManager.NavigateTo(NavigationManager.Uri, true);
        }
    }
}
