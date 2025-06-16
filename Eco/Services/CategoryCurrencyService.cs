using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Services
{
    using Eco.Models;

    public class CategoryCurrencyService
    {
        private readonly ApiService _apiService;
        private const string CategoriesEndpoint = "api/categories";
        private const string CurrenciesEndpoint = "api/currencies";

        public CategoryCurrencyService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<Categories> GetIncomeCategories()
        {
            return await _apiService.GetAsync<Categories>($"{CategoriesEndpoint}/income");
        }

        public async Task<Categories> GetExpenseCategories()
        {
            return await _apiService.GetAsync<Categories>($"{CategoriesEndpoint}/expense");
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            return await _apiService.GetAsync<List<Currency>>(CurrenciesEndpoint);
        }

        public async Task<Currency> GetCurrency(int id)
        {
            return await _apiService.GetAsync<Currency>($"{CurrenciesEndpoint}/{id}");
        }
    }
}
