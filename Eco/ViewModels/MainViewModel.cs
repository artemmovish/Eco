using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eco.Models;
using Eco.Services;
using Eco.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.ViewModels
{
    public partial class MainViewModel : BaseViewModel
    {
        private readonly AccountService _accountService;
        private readonly TransactionService _transactionService;
        private readonly AuthService _authService;
        private readonly ApiService _apiService;

        [ObservableProperty]
        private decimal totalBalance;
        
        [ObservableProperty]
        private decimal monthlyIncome;
        
        [ObservableProperty]
        private decimal monthlyExpenses;

        public ObservableCollection<Account> Accounts { get; } = new();
        public ObservableCollection<Transaction> RecentTransactions { get; } = new();

        public MainViewModel(AccountService accountService, TransactionService transactionService, AuthService authService, ApiService apiService)
        {
            _accountService = accountService;
            _authService = authService;
            _transactionService = transactionService;
            LoadDataCommand = new AsyncRelayCommand(LoadDataAsync);
            LogoutCommand = new AsyncRelayCommand(LogoutAsync);
            _apiService = apiService;
        }

        public IAsyncRelayCommand LoadDataCommand { get; }
        public IAsyncRelayCommand LogoutCommand { get; }

        private async Task LoadDataAsync()
        {
            if (IsBusy) return;

            try
            {
                IsBusy = true;

                // Загрузка счетов
                var accounts = await _accountService.GetAccounts();
                Accounts.Clear();

                List<Transaction>? transactions = new();

                TotalBalance = 0;

                foreach (var account in accounts)
                {
                    Accounts.Add(account);

                    TotalBalance += account.Balance;

                    var temp = await _transactionService.GetTransactions(account.Id);

                    foreach (var item in temp)
                    {
                        transactions.Add(item);
                    }
                }

                // Загрузка транзакций
                RecentTransactions.Clear();
                foreach (var transaction in transactions)
                {
                    RecentTransactions.Add(transaction);
                }
                
                // Загрузка сводной информации
                var summary = await _apiService.GetAsync<Summary>("api/transactions/summary");

                MonthlyIncome = summary.TotalIncome;
                MonthlyExpenses = summary.TotalExpense; 
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LogoutAsync()
        {
            try
            {
                await _authService.Logout();
                await Shell.Current.GoToAsync("//LoginPage");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
