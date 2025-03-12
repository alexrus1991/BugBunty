using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace Blazor_BugBunty.Interfaces
{
    public class BugBountyAuthenticationHeaderHandler : AuthorizationMessageHandler
    {
        public BugBountyAuthenticationHeaderHandler(IAccessTokenProvider provider, NavigationManager navigation) : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:7174/api/" },
                scopes: new[] { "api://0862cd26-6dd6-426d-aff8-e2fa9767c2e1/full" });

            InnerHandler = new HttpClientHandler();
        }
    }
}
