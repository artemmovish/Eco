using Eco.ViewModels;

namespace Eco.Views.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage(AuthViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}