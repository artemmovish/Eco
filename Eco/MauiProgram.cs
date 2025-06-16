using Eco.Services;
using Eco.ViewModels;
using Eco.Views.Pages;
using Microcharts.Maui;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;

namespace Eco;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMicrocharts()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

        // Регистрация HttpClient
        builder.Services.AddHttpClient("FinanceTracker", client =>
        {
            client.BaseAddress = new Uri("https://a5177277-833a-436a-ba46-0a87cec9f45d.tunnel4.com/");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

        builder.Services.AddTransient<AuthViewModel>();
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<AddTransactionViewModel>();
        builder.Services.AddTransient<ChartsViewModel>();

        // Регистрация сервисов
        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddSingleton<AuthService>();
        builder.Services.AddSingleton<AccountService>();
        builder.Services.AddSingleton<TransactionService>();
        builder.Services.AddSingleton<GoalService>();
        builder.Services.AddSingleton<CategoryCurrencyService>();

        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<AddTransactionPage>();
        builder.Services.AddTransient<ChartsPage>();

        return builder.Build();
	}
}
