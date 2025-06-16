using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Eco.Enums;
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
    public partial class AddTransactionViewModel : BaseViewModel
    {
        private readonly TransactionService _transactionService;
        private readonly AccountService _accountService;

        public AddTransactionViewModel(TransactionService transactionService, AccountService accountService)
        {
            _transactionService = transactionService;
            _accountService = accountService;
            InitializeData();
        }

        // Инициализация данных (можно заменить на загрузку из API)
        public async Task InitializeData()
        {
            var accounts = await _accountService.GetAccounts();

            Accounts = new ObservableCollection<Account>(accounts);

            Categories = new ObservableCollection<string>
            {
                "Зарплата",
                "Фриланс",
                "Подарок",
                "Инвестиции",
                "Другое"
            };

            SelectedDate = DateTime.Now;
        }

        // Свойства для привязки
        [ObservableProperty]
        private ObservableCollection<Account> _accounts;

        [ObservableProperty]
        private Account _selectedAccount;

        [ObservableProperty]
        private ObservableCollection<string> _categories;

        [ObservableProperty]
        private string _selectedCategory;

        [ObservableProperty]
        private decimal _amount;

        [ObservableProperty]
        private DateTime _selectedDate;

        [ObservableProperty]
        private string _comment;

        [ObservableProperty]
        private bool _isIncome = false; // false = расход, true = доход

        // Команды
        [RelayCommand]
        private void ToggleTransactionType()
        {
            IsIncome = !IsIncome;
            // Можно обновить список категорий в зависимости от типа
            Categories = new ObservableCollection<string>(
                IsIncome
                    ? Enum.GetNames(typeof(IncomeCategories))
                    : Enum.GetNames(typeof(ExpenseCategories)));
        }

        [RelayCommand]
        private void SelectQuickDate(string daysAgo)
        {

            var d = Convert.ToInt32(daysAgo);

            SelectedDate = DateTime.Now.AddDays(-d);
        }

        [RelayCommand]
        private async Task SaveTransaction()
        {
            if (SelectedAccount == null || string.IsNullOrEmpty(SelectedCategory))
            {
                await Shell.Current.DisplayAlert("Ошибка", "Выберите счет и категорию", "OK");
                return;
            }

            try
            {
                IsBusy = true;

                var transaction = new Transaction
                {
                    AccountId = SelectedAccount.Id,
                    Amount = IsIncome ? Amount : -Amount, // Отрицательное значение для расходов
                    Date = SelectedDate,
                    Comment = Comment,
                    IncomeCategoryId = IsIncome ? Enum.Parse<IncomeCategories>(SelectedCategory) : null,
                    ExpenseCategoryId = !IsIncome ? Enum.Parse<ExpenseCategories>(SelectedCategory) : null,
                    Type = IsIncome ? "income" : "expense"
                };

                await _transactionService.CreateTransaction(SelectedAccount.Id, transaction);
                await Shell.Current.GoToAsync(".."); // Вернуться назад после сохранения
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
