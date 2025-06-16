using Eco.ViewModels;

namespace Eco.Views.Pages;

public partial class AddTransactionPage : ContentPage
{
    public AddTransactionPage(AddTransactionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((AddTransactionViewModel)BindingContext).InitializeData();
    }
}