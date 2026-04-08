using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;

namespace Vrittiy.Web.Services
{

    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContext;

        public ApiService(HttpClient httpClient, IConfiguration config, IHttpContextAccessor context)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(config["ApiSettings:BaseUrl"]);
            _httpContext = context;
        }

        private void AddToken()
        {
            var token = _httpContext.HttpContext.Session.GetString("JWToken");

            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<string> PostAsync(string url, object data)
        {
            AddToken();

            var content = new StringContent(
                JsonConvert.SerializeObject(data),
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(url, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> GetAsync(string url)
        {
            AddToken();

            var response = await _httpClient.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }

    }
}