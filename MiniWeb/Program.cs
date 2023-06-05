using Microsoft.EntityFrameworkCore;
using MiniWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPeopleService, PeopleService>();

builder.Services.AddDbContext<AppDbContext>(optionsBuilder =>
{
    optionsBuilder.UseInMemoryDatabase("InMemoryDB");
});

var app = builder.Build();

app.MapGet("/", (IPeopleService peopleService) => $"Hello World - {peopleService.GetAge()}");
app.MapGet("/people", async (AppDbContext dbContext) =>
{
    var peoples = await dbContext.People.ToListAsync();

    return peoples;
});

using var serviceScope = app.Services.CreateScope();
var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
appDbContext.Database.EnsureDeleted();
appDbContext.Database.EnsureCreated();
appDbContext.People.Add(new People(1, "AA"));
appDbContext.People.Add(new People(2, "BB"));
appDbContext.People.Add(new People(3, "CC"));
await appDbContext.SaveChangesAsync();

app.Run();

public partial class Program
{
}

public class PeopleService : IPeopleService
{
    public int GetAge()
    {
        return 11;
    }
}