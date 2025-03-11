using Blazor_BugBunty.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor_BugBunty.Pages
{
    public partial class Profil : ComponentBase
    {
        private readonly ApiService _apiService;

        public Profil(ApiService apiService)
        {
            _apiService = apiService;
            
        }
        public Profil()
        {
            
        }
        List<string> Infos { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Infos = new List<string>();
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            foreach (var claims in user.Claims)
            {
                Infos.Add($"{claims.Subject.Name} - {claims.Type} - {claims.Value}");
            }

            await _apiService.GetDataFromApi();
        }

    }
}
