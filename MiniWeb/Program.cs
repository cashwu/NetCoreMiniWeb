using MiniWeb;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IPeopleService, PeopleService>();

var app = builder.Build();

app.MapGet("/", (IPeopleService peopleService) => $"Hello World - {peopleService.GetAge()}");

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