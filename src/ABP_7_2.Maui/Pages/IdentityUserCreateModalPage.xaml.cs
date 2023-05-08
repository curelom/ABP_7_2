using CommunityToolkit.Mvvm.Messaging;
using ABP_7_2.Maui.Messages;
using ABP_7_2.Maui.ViewModels;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace ABP_7_2.Maui.Pages;

public partial class IdentityUserCreateModalPage : ContentPage, ITransientDependency
{
    public IdentityUserCreateModalPage(IdentityUserCreateViewModel vm)
    {
        BindingContext = vm;
        InitializeComponent();
    }
}