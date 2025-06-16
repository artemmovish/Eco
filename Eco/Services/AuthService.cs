using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eco.Services
{
    using Eco.Models;

    public class AuthService
    {
        private readonly ApiService _apiService;
        private const string LoginEndpoint = "api/login";
        private const string RegisterEndpoint = "api/register";
        private const string LogoutEndpoint = "api/logout";
        private const string UserEndpoint = "api/user";

        public AuthService(ApiService apiService)
        {
            _apiService = apiService;
        }
        
        public async Task<User> Login(string email, string password)
        {
            var request = new { email, password };
            var user = await _apiService.PostAsync<object, User>(LoginEndpoint, request);
            _apiService.SetAuthToken(user.Token); // Предполагается, что токен возвращается в ответе
            return user;
        }

        public async Task<User> Register(string login, string email, string password, int currencyId)
        {
            var request = new { login, email, password, currency_id = currencyId };
            return await _apiService.PostAsync<object, User>(RegisterEndpoint, request);
        }

        public async Task Logout()
        {
            await _apiService.PostAsync<object, object>(LogoutEndpoint, null);
            _apiService.SetAuthToken(null);
        }

        public async Task<User> GetUserProfile()
        {
            return await _apiService.GetAsync<User>(UserEndpoint);
        }
    }
}
