using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Services
{
    using Eco.Models;

    public class GoalService
    {
        private readonly ApiService _apiService;
        private const string BaseEndpoint = "api/goals";

        public GoalService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Goal>> GetGoals()
        {
            return await _apiService.GetAsync<List<Goal>>(BaseEndpoint);
        }

        public async Task<Goal> GetGoal(int id)
        {
            return await _apiService.GetAsync<Goal>($"{BaseEndpoint}/{id}");
        }

        public async Task<Goal> CreateGoal(Goal goal)
        {
            return await _apiService.PostAsync<Goal, Goal>(BaseEndpoint, goal);
        }

        public async Task AddToGoal(int goalId, float amount, int accountId)
        {
            var request = new { amount, account_id = accountId };
            await _apiService.PostAsync<object, object>($"{BaseEndpoint}/{goalId}/add", request);
        }

        public async Task WithdrawFromGoal(int goalId, float amount, int accountId)
        {
            var request = new { amount, account_id = accountId };
            await _apiService.PostAsync<object, object>($"{BaseEndpoint}/{goalId}/withdraw", request);
        }

        public async Task<List<GoalHistory>> GetGoalHistory(int goalId)
        {
            return await _apiService.GetAsync<List<GoalHistory>>($"{BaseEndpoint}/{goalId}/history");
        }

        public async Task DeleteGoal(int id)
        {
            await _apiService.DeleteAsync($"{BaseEndpoint}/{id}");
        }
    }
}
