using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Blazor_BugBunty.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IAccessTokenProvider _tokenProvider;

        public ApiService(HttpClient httpClient, IAccessTokenProvider tokenProvider)
        {
            _httpClient = httpClient;
            _tokenProvider = tokenProvider;
        }

        public async Task<string> GetDataFromApi()
        {
            return await _httpClient.GetFromJsonAsync<string>("User/GetTokenInfo");
            
        }
    }
}
