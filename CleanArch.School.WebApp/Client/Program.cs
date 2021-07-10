namespace CleanArch.School.WebApp.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Blazored.Modal;
    using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddHttpClient("CleanArch.School.WebApp.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
                .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CleanArch.School.WebApp.ServerAPI"));

            builder.Services.AddHttpClient("CleanArch.School.API", client => client.BaseAddress = new Uri("https://localhost:5003/"));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("CleanArch.School.API"));

            builder.Services.AddApiAuthorization();

            builder.Services.AddBlazoredModal();

            await builder.Build().RunAsync();
        }
    }
}