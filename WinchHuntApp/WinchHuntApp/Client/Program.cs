using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Majorsoft.Blazor.Components.Maps;
using Majorsoft.Blazor.Components.Common.JsInterop;
using WinchHuntApp.Client.Services;

namespace WinchHuntApp.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("WinchHuntApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddHttpClient("WinchHuntApp.PublicServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            builder.Services.AddHttpClient<PublicHttpClient>(client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("WinchHuntApp.ServerAPI"));

            builder.Services.AddJsInteropExtensions();
            builder.Services.AddSingleton<IMapService, MapService>();
            builder.Services.AddScoped<BrowserService>();

            builder.Services.AddApiAuthorization();
            builder.Services.AddMapExtensions();
            


            var host = builder.Build();

            var mapService = host.Services.GetRequiredService<IMapService>();
            await mapService.Initialize();

            await host.RunAsync();
        }

    }
}
