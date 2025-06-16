using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Services
{
    using Eco.Models;

    public class AccountService
    {
        private readonly ApiService _apiService;
        private const string BaseEndpoint = "api/accounts";

        public AccountService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Account>> GetAccounts()
        {
            return await _apiService.GetAsync<List<Account>>(BaseEndpoint);
        }

        public async Task<Account> GetAccount(int id)
        {
            return await _apiService.GetAsync<Account>($"{BaseEndpoint}/{id}");
        }

        public async Task<Account> CreateAccount(string name, float balance, int currencyId)
        {
            var request = new { name, balance, currency_id = currencyId };
            return await _apiService.PostAsync<object, Account>(BaseEndpoint, request);
        }

        public async Task<Account> UpdateAccount(int id, string name, float balance, int currencyId)
        {
            var request = new { name, balance, currency_id = currencyId };
            return await _apiService.PutAsync<object, Account>($"{BaseEndpoint}/{id}", request);
        }

        public async Task DeleteAccount(int id)
        {
            await _apiService.DeleteAsync($"{BaseEndpoint}/{id}");
        }
    }
}
