using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ABP_7_2.Maui.Messages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace ABP_7_2.Maui.ViewModels;
public partial class IdentityUserCreateViewModel : ABP_7_2ViewModelBase, ITransientDependency
{
    public IdentityUserCreateDto User { get; } = new();

    [RelayCommand]
    async Task Cancel()
    {
        await Shell.Current.GoToAsync(".."); 
    }

    [RelayCommand]
    async Task Create()
    {
        WeakReferenceMessenger.Default.Send(new IdentityUserCreateMessage(User));
        await Shell.Current.GoToAsync(".."); 
    }
}
