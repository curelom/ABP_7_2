using ABP_7_2.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Maui.Pages;

public partial class IdentityUserPage : ContentPage, ITransientDependency
{
	public IdentityUserPage(IdentityUserPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        if(BindingContext is IOnAppearing vm)
        {
            await vm.OnAppearingAsync();
        }
    }
}