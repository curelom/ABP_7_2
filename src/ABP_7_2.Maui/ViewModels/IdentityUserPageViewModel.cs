using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ABP_7_2.Maui.Messages;
using ABP_7_2.Maui.Pages;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Identity;

namespace ABP_7_2.Maui.ViewModels;
public partial class IdentityUserPageViewModel : ABP_7_2ViewModelBase,
    IRecipient<IdentityUserCreateMessage>, //create
    IRecipient<IdentityUserEditMessage>, //edit
    IOnAppearing,
    ITransientDependency
{
    protected IIdentityUserAppService IdentityUserAppService { get; }

    public GetIdentityUsersInput? Input { get; } = new();

    public ObservableCollection<IdentityUserDto> Items { get; } = new();

    private bool _isBusy;
    public bool IsBusy { get => _isBusy; set => SetProperty(ref _isBusy, value, nameof(IsBusy)); }

    public IdentityUserPageViewModel(IIdentityUserAppService identityUserAppService)
    {
        IdentityUserAppService = identityUserAppService;

        Input = new();
        Items = new();

        WeakReferenceMessenger.Default.Register<IdentityUserCreateMessage>(this);
        WeakReferenceMessenger.Default.Register<IdentityUserEditMessage>(this);
    }


    [RelayCommand]
    async Task OpenCreateModal()
    {
        await Shell.Current.GoToAsync(nameof(IdentityUserCreateModalPage));
    }

    [RelayCommand]
    async Task OpenEditModal(Guid userId)
    {
        await Shell.Current.GoToAsync($"{nameof(IdentityUserEditModalPage)}?UserId={userId}");
    }

    [RelayCommand]
    async Task Refresh()
    {
        await GetUsersAsync();
    }

    [RelayCommand]
    async Task Delete(IdentityUserDto user)
    {
        if (Application.Current is { MainPage: { } })
        {
            var action = await Application.Current.MainPage.DisplayActionSheet(
                string.Format(L["UserDeletionConfirmationMessage"].Value, user.UserName),
                L["Cancel"].Value,
                L["Delete"].Value);

            if(action == L["Cancel"].Value)
            {
                return;
            }

            try
            {
                await IdentityUserAppService.DeleteAsync(user.Id);
            }
            catch (AbpRemoteCallException remoteException)
            {
                await Application.Current.MainPage.DisplayAlert(L["Error"].Value, remoteException.Message, L["OK"].Value);
            }

            await GetUsersAsync();
        }

    }

    private async Task GetUsersAsync()
    {
        if (IsBusy)
        {
            return;
        }

        IsBusy = true;

        Items.Clear();

        try
        {
            var result = await IdentityUserAppService.GetListAsync(Input);

			foreach (var user in result.Items)
			{
				Items.Add(user);
			}
		}
        catch (AbpRemoteCallException remoteException)
        {
            if(Application.Current is { MainPage: { } })
            {
                await Application.Current.MainPage.DisplayAlert(L["Error"].Value, remoteException.Message, L["OK"].Value);
            }
        }

        IsBusy = false;
    }

    public void Receive(IdentityUserCreateMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                await IdentityUserAppService.CreateAsync(message.Value);
                await GetUsersAsync();
            }
            catch (Exception ex)
            {
                if(Application.Current is { MainPage: { } })
                {
                    await Application.Current.MainPage.DisplayAlert("", ex.Message, "OK");
                }
            }
        });
    }

    public void Receive(IdentityUserEditMessage message)
    {
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            try
            {
                var args = message.Value;

                await IdentityUserAppService.UpdateAsync(args.UserId, args.User);
                await GetUsersAsync();
            }
            catch (Exception ex)
            {
                if (Application.Current is { MainPage: { } })
                {
                    await Application.Current.MainPage.DisplayAlert(string.Empty, ex.Message, "OK");
                }
            }
        });
    }

    public async Task OnAppearingAsync()
    {
        await GetUsersAsync();
    }
}
