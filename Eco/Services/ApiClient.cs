
namespace Eco.Services
{
    using System.Net.Http.Headers;
    using System.Text;
    using System.Text.Json;

    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;
        private string _authToken;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));

            if (_httpClient.BaseAddress == null)
            {
                _httpClient.BaseAddress = new Uri("https://dbf2f757-a986-43ad-8813-f71c27410368.tunnel4.com/");
            }

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        public void SetAuthToken(string token)
        {
            _authToken = token;
            _httpClient.DefaultRequestHeaders.Authorization =
                !string.IsNullOrEmpty(_authToken)
                    ? new AuthenticationHeaderValue("Bearer", _authToken)
                    : null;
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            var uri = BuildUri(endpoint);
            var response = await _httpClient.GetAsync(uri);
            return await HandleResponse<T>(response);
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var uri = BuildUri(endpoint);
            var content = PrepareContent(data);
            var response = await _httpClient.PostAsync(uri, content);
            return await HandleResponse<TResponse>(response);
        }

        public async Task<TResponse> PutAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            var uri = BuildUri(endpoint);
            var content = PrepareContent(data);
            var response = await _httpClient.PutAsync(uri, content);
            return await HandleResponse<TResponse>(response);
        }

        public async Task DeleteAsync(string endpoint)
        {
            var uri = BuildUri(endpoint);
            await _httpClient.DeleteAsync(uri);
        }

        private Uri BuildUri(string endpoint)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentException("Endpoint cannot be empty", nameof(endpoint));
            }

            // Если endpoint уже абсолютный URL
            if (Uri.TryCreate(endpoint, UriKind.Absolute, out var absoluteUri))
            {
                return absoluteUri;
            }

            // Для относительных путей
            if (!endpoint.StartsWith("/"))
            {
                endpoint = "/" + endpoint;
            }

            return new Uri(_httpClient.BaseAddress, endpoint);
        }

        private HttpContent PrepareContent<T>(T data)
        {
            return new StringContent(
                JsonSerializer.Serialize(data, _jsonOptions),
                Encoding.UTF8,
                "application/json");
        }

        private async Task<T> HandleResponse<T>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"HTTP request failed with status code {response.StatusCode}: {content}");
            }

            try
            {
                return JsonSerializer.Deserialize<T>(content) ??
                       throw new JsonException("Deserialization returned null");
            }
            catch (JsonException ex)
            {
                throw new JsonException($"Failed to deserialize response: {content}", ex);
            }
        }
    }
}
