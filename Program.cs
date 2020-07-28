using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Cmta.Clients.Spa.Api.Services;
using Cmta.Clients.Spa.Contexts.Error;
using Cmta.Clients.Spa.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;

namespace Cmta.Clients.Spa
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            
            builder.RootComponents.Add<App>("app");
            builder.Services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons()
                .AddSyncfusionBlazor();

            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOidcAuthentication(options =>
            {
                builder.Configuration.Bind("oidc", options.ProviderOptions);
            });

            builder.Services.AddHttpClient("cmta")
                .AddHttpMessageHandler(sp =>
                {
                    var handler = sp.GetService<AuthorizationMessageHandler>()
                    .ConfigureHandler(
                        authorizedUrls: new[] { "https://localhost:5003" },
                        scopes: new[] { "cmta.all" });

                    return handler;
                });

            builder.Services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();
            builder.Services.AddSingleton<IAppErrorContext, AppErrorContext>();
            builder.Services.AddScoped<ILocationApiService, LocationApiService>();
            builder.Services.AddScoped(sp => sp.GetService<IHttpClientFactory>().CreateClient("cmta"));

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
