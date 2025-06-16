using Eco.ViewModels;

namespace Eco.Views.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage(AuthViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}