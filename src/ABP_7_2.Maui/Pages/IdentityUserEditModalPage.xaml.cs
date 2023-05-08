using ABP_7_2.Maui.ViewModels;
using Volo.Abp.DependencyInjection;

namespace ABP_7_2.Maui.Pages;

public partial class IdentityUserEditModalPage : ContentPage, ITransientDependency
{
	public IdentityUserEditModalPage(IdentityUserEditViewModel vm)
	{
		BindingContext = vm;
		InitializeComponent();
	}
}