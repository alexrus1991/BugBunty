using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor_BugBunty;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Blazor_BugBunty.Interfaces;
using Blazor_BugBunty.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped<ICustomHttpClient, CustomHttpClient>();
//builder.Services.AddScoped<AuthenticationHeaderHandler>();
builder.Services.AddScoped<ApiService>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes
    .Add("https://graph.microsoft.com/User.Read");
    options.ProviderOptions.LoginMode = "redirect";
    options.ProviderOptions.Authentication.PostLogoutRedirectUri = "/";
});

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7174/api/")
});

//builder.Services.AddHttpClient<ICustomHttpClient, CustomHttpClient>("WithTokenCall", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7174/api/");
//    client.DefaultRequestHeaders.Add("Accept", "application/json");
//}).AddHttpMessageHandler<AuthenticationHeaderHandler>();

builder.Services.AddHttpClient("API", client =>
{
    client.BaseAddress = new Uri("https://localhost:7174/api/"); // URL de l'API .NET 8
})
.AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("API"));

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
//localhost:7174