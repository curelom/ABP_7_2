using CommunityToolkit.Mvvm.Messaging;
using ABP_7_2.Maui.Localization;
using ABP_7_2.Maui.Messages;
using ABP_7_2.Maui.Oidc;
using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Maui.ViewModels;

public class ShellViewModel : ABP_7_2ViewModelBase, ITransientDependency
{
    public bool IsIdentityUserPageVisible => CurrentUser.IsAuthenticated;

    public string CurrentUserName => L["Welcome"] + $" {CurrentUser.UserName}";

    public ShellViewModel(LocalizationResourceManager localizationManager)
    {
        WeakReferenceMessenger.Default.Register<LoginMessage>(this, (r, m) =>
        {
            UpdateProperties();
        });

        WeakReferenceMessenger.Default.Register<LogoutMessage>(this, (r, m) =>
        {
            UpdateProperties();
        });

        localizationManager.PropertyChanged += (_, _) =>
        {
            UpdateProperties();
        };
    }

    private void UpdateProperties()
    {
        OnPropertyChanged(nameof(IsIdentityUserPageVisible));
        OnPropertyChanged(nameof(CurrentUserName));
    }
}