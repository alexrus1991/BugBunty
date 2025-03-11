using Blazor_BugBunty.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor_BugBunty.Pages
{
    public partial class Profil : ComponentBase
    {
        [Inject]
        private  ApiService _apiService { get; set; }

        

        List<string> Infos { get; set; }
        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Infos = new List<string>();
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine("✅ Utilisateur connecté !");
                Console.WriteLine($"👤 Nom : {user.Identity.Name}");
            }
            else
            {
                Console.WriteLine("❌ Utilisateur non authentifié !");
            }
            foreach (var claims in user.Claims)
            {
                Infos.Add($"{claims.Subject.Name} - {claims.Type} - {claims.Value}");
            }

            await _apiService.GetDataFromApi();
        }

    }
}
