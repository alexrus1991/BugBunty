using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor_BugBunty;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Blazor_BugBunty.Interfaces;
using Blazor_BugBunty.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

//builder.Services.AddScoped<ICustomHttpClient, CustomHttpClient>();
builder.Services.AddScoped<BugBountyAuthenticationHeaderHandler>();
builder.Services.AddScoped<ApiService>();


builder.Services.AddScoped(sp => new HttpClient(sp.GetRequiredService<BugBountyAuthenticationHeaderHandler>())
{
    BaseAddress = new Uri("https://localhost:7174/api/")
});

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
    .Add("https://graph.microsoft.com/User.Read");
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.Authentication.PostLogoutRedirectUri = "/";
});
builder.Services.AddCascadingAuthenticationState();




await builder.Build().RunAsync();
//localhost:7174