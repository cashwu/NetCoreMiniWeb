using System.Net.Http.Json;
using FluentAssertions;

namespace MiniWeb.Tests;

public class IndexTests
{
    [Fact]
    public async Task Index()
    {
        await using var application = new TestingApplication();

        var resp = await application.Client.GetStringAsync("/");

        resp.Should().Be("Hello World - 123");
    }

    [Fact]
    public async Task People()
    {
        await using var application = new TestingApplication();

        application.DbOperator(db =>
        {
            db.People.Add(new People(11, "AA"));
            db.People.Add(new People(22, "BB"));
            db.People.Add(new People(3, "C"));
            db.SaveChanges();
        });

        var resp = await application.Client.GetFromJsonAsync<List<People>>("/People");

        resp.Count.Should().Be(3);

        var expect = new List<People>
        {
            new(11, "AA"),
            new(22, "BB"),
            new(3, "C"),
        };

        resp.Should().BeEquivalentTo(expect);
    }
}