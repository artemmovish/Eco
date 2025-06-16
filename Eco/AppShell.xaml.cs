using Eco.Views.Pages;

namespace Eco
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            // Регистрация всех маршрутов
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            // Авторизация
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));

            // Основные страницы
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(AddTransactionPage), typeof(AddTransactionPage));
            Routing.RegisterRoute(nameof(ChartsPage), typeof(ChartsPage));
        }
    }
}
