using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace SSApp.IntegrationTests
{
    public class WebApplicationE2EFactory: WebApplicationFactory<SSApp.Startup>
    {
        protected override IWebHostBuilder CreateWebHostBuilder()
        {
            return WebHost.CreateDefaultBuilder()
                .UseStartup<SSApp.Startup>();
        }

        //protected override void ConfigureWebHost(IWebHostBuilder builder)
        //{
        //    builder.UseContentRoot(".");
        //    base.ConfigureWebHost(builder);
        //}
    }
}
