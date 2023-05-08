using System.Globalization;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.Options;
using ABP_7_2.Maui.Messages;
using ABP_7_2.Maui.Oidc;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Localization;

namespace ABP_7_2.Maui.ViewModels;

public class MainPageViewModel : ABP_7_2ViewModelBase, ITransientDependency
{
    private readonly ILoginService _loginService;
    private readonly AbpLocalizationOptions _localizationOptions;

    public string LoginOrLogoutBtnText => CurrentUser.IsAuthenticated ? L["SignOut"] : L["SignIn"];

    public ICommand LoginOrLogoutCommand { get; }

    public ICommand ChangeLanguageCommand { get; }

    public MainPageViewModel(
        ILoginService loginService,
        IOptions<AbpLocalizationOptions> localizationOptions)
    {
        _loginService = loginService;
        _localizationOptions = localizationOptions.Value;

        WeakReferenceMessenger.Default.Register<LoginMessage>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(LoginOrLogoutBtnText));
        });

        WeakReferenceMessenger.Default.Register<LogoutMessage>(this, (r, m) =>
        {
            OnPropertyChanged(nameof(LoginOrLogoutBtnText));
        });

        LoginOrLogoutCommand = new Command(LoginOrLogoutAsync);
        ChangeLanguageCommand = new Command(ChangeLanguageAsync);
    }

    private async void LoginOrLogoutAsync()
    {
        if(!CurrentUser.IsAuthenticated)
        {
            var loginResult = await _loginService.LoginAsync();

            if (loginResult.IsError)
            {
                await Shell.Current.DisplayAlert(L["Error"], loginResult.Error, L["Close"]);
            }
        }
        else
        {
            var logoutResult = await _loginService.LogoutAsync();

            if (logoutResult.IsError)
            {
                await Shell.Current.DisplayAlert(L["Error"], logoutResult.Error, L["Close"]);
            }
        }
    }

    private async void ChangeLanguageAsync()
    {
        var selectedLanguage = await Shell.Current.CurrentPage.DisplayActionSheet(
            null, null, null,
            _localizationOptions.Languages.Select(x => x.DisplayName).ToArray());

        if(selectedLanguage.IsNullOrWhiteSpace())
        {
            return;
        }

        var culture = _localizationOptions.Languages.FirstOrDefault(x => x.DisplayName == selectedLanguage);
        if(culture == null)
        {
            return;
        }

        L.CurrentCulture = new CultureInfo(culture.CultureName);

        OnPropertyChanged(nameof(LoginOrLogoutBtnText));
    }
}
