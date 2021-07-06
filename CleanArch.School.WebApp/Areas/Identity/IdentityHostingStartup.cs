using CleanArch.School.WebApp.Areas.Identity;
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace CleanArch.School.WebApp.Areas.Identity
{
    using Microsoft.AspNetCore.Hosting;

    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}