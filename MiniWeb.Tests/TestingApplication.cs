using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace MiniWeb.Tests;

public class TestingApplication : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.AddScoped<IPeopleService, TestPeopleService>();
        });

        builder.ConfigureServices(services =>
        {
            services.AddDbContext<AppDbContext>();
        });

        base.ConfigureWebHost(builder);
    }
}