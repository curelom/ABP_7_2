using ABP_7_2.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Maui;

public partial class MainPage : ContentPage, ITransientDependency
{
    public MainPage(
		MainPageViewModel vm)
	{
        BindingContext = vm;
        InitializeComponent();
    }
}