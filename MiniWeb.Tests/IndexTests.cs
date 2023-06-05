using System.Net.Http.Json;
using FluentAssertions;

namespace MiniWeb.Tests;

public class IndexTests : TestingApplication
{
    [Fact]
    public async Task Index()
    {
        var resp = await Client.GetStringAsync("/");

        resp.Should().Be("Hello World - 123");
    }

    [Fact]
    public async Task People()
    {
        DbOperator(db =>
        {
            db.People.Add(new People(11, "AA"));
            db.People.Add(new People(22, "BB"));
            db.People.Add(new People(3, "C"));
            db.SaveChanges();
        });

        var resp = await Client.GetFromJsonAsync<List<People>>("/People");

        resp.Count.Should().Be(3);

        var expect = new List<People>
        {
            new(11, "AA"),
            new(22, "BB"),
            new(3, "C"),
        };

        resp.Should().BeEquivalentTo(expect);
        
        DbOperator(db =>
        {
            db.People.Count().Should().Be(3);
        });
    }
}