using Eco.Models;
using Eco.Services;
using Eco.ViewModels.Base;
using Eco.Views.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Eco.ViewModels
{
    public class AuthViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public AuthViewModel(AuthService authService)
        {
            _authService = authService;

            LoginCommand = new Command(async () => await LogIn());
            RegisterCommand = new Command(async () => await Register());
            GoToRegisterCommand = new Command(async () => await GoToRegister());
            GoToLoginCommand = new Command(async () => await GoToLogin());

            // Загрузка валют (можно кэшировать)
            LoadCurrencies();
        }

        // Свойства для авторизации
        public string Email { get; set; } = "user@example.com";
        public string Password { get; set; } = "password";

        // Свойства для регистрации
        public string Login { get; set; }
        public string ConfirmPassword { get; set; }
        public Currency SelectedCurrency { get; set; }
        public ObservableCollection<Currency> Currencies { get; } = new();

        // Команды
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand GoToRegisterCommand { get; }
        public ICommand GoToLoginCommand { get; }

        private async Task LogIn()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Заполните все поля";
                HasError = true;
                return;
            }

            try
            {
                IsBusy = true;
                var user = await _authService.Login(Email, Password);

                // Успешная авторизация - переходим на главную страницу
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Ошибка авторизации: " + ex.Message;
                HasError = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Register()
        {
            if (string.IsNullOrWhiteSpace(Login) ||
                string.IsNullOrWhiteSpace(Email) ||
                string.IsNullOrWhiteSpace(Password) ||
                string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                ErrorMessage = "Заполните все поля";
                HasError = true;
                return;
            }

            if (Password != ConfirmPassword)
            {
                ErrorMessage = "Пароли не совпадают";
                HasError = true;
                return;
            }

            if (SelectedCurrency == null)
            {
                ErrorMessage = "Выберите валюту";
                HasError = true;
                return;
            }

            try
            {
                IsBusy = true;
                var user = await _authService.Register(Login, Email, Password, SelectedCurrency.Id);

                // Успешная регистрация - переходим на главную страницу
                await Shell.Current.GoToAsync("//MainPage");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Ошибка регистрации: " + ex.Message;
                HasError = true;
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GoToRegister()
        {
            await Shell.Current.GoToAsync("//RegisterPage");
        }

        private async Task GoToLogin()
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
        
        private async void LoadCurrencies()
        {
            try
            {
                // Здесь должна быть загрузка валют из API
                // Временно используем моковые данные
                Currencies.Add(new Currency { Id = 1, Name = "Рубль (RUB)", Code = "RUB" });
                Currencies.Add(new Currency { Id = 2, Name = "Доллар (USD)", Code = "USD" });
                Currencies.Add(new Currency { Id = 3, Name = "Евро (EUR)", Code = "EUR" });
            }
            catch (Exception ex)
            {
                ErrorMessage = "Не удалось загрузить список валют";
                HasError = true;
            }
        }
    }
}
