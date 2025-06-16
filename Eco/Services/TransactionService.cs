using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Services
{
    using Eco.Models;
    using Eco.Models.DTO;

    public class TransactionService
    {
        private readonly ApiService _apiService;
        private const string BaseEndpoint = "api/accounts";

        public TransactionService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Transaction>> GetTransactions(int accountId)
        {
            var list = await _apiService.GetAsync<List<Transaction>>($"{BaseEndpoint}/{accountId}/transactions");

            foreach (var item in list)
            {
                if (item.IncomeCategoryId != null)
                {
                    item.Category = item.IncomeCategoryId.ToString();
                }
                else
                {
                    item.Category = item.ExpenseCategoryId.ToString();
                }
            }

            return list;
        }

        public async Task<Transaction> GetTransaction(int accountId, int transactionId)
        {
            return await _apiService.GetAsync<Transaction>($"{BaseEndpoint}/{accountId}/transactions/{transactionId}");
        }

        public async Task<Transaction> CreateTransaction(int accountId, Transaction transaction)
        {
            if(transaction.Type == "income")
            {
                return await _apiService.PostAsync<TransactionIncome, Transaction>(
                $"{BaseEndpoint}/{accountId}/transactions", new TransactionIncome(transaction));
            }
            else
            {
                return await _apiService.PostAsync<TransactionExpense, Transaction>(
                $"{BaseEndpoint}/{accountId}/transactions", new TransactionExpense(transaction));
            }
            
        }

        public async Task<Transaction> UpdateTransaction(int accountId, int transactionId, Transaction transaction)
        {
            return await _apiService.PutAsync<Transaction, Transaction>(
                $"{BaseEndpoint}/{accountId}/transactions/{transactionId}", transaction);
        }

        public async Task DeleteTransaction(int accountId, int transactionId)
        {
            await _apiService.DeleteAsync($"{BaseEndpoint}/{accountId}/transactions/{transactionId}");
        }

        public async Task<Summary> GetSummary()
        {
            return await _apiService.GetAsync<Summary>("api/transactions/summary");
        }

        public async Task<object> GetReport(string type)
        {
            return await _apiService.GetAsync<object>($"api/transactions/report?type={type}");
        }
    }
}
