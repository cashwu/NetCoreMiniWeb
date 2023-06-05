using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("InMemoryDb");
            });

            using var serviceScope = services.BuildServiceProvider().CreateScope();
            var serviceProvider = serviceScope.ServiceProvider;
            var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();

            appDbContext.Database.EnsureDeleted();
            appDbContext.Database.EnsureCreated();

            appDbContext.People.Add(new People(11, "AA"));
            appDbContext.People.Add(new People(22, "BB"));
            appDbContext.People.Add(new People(33, "CC"));

            appDbContext.SaveChanges();
        });
    }
}