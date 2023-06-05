using System.Net.Http.Json;
using FluentAssertions;

namespace MiniWeb.Tests;

public class IndexTests
{
    [Fact]
    public async Task Index()
    {
        await using var application = new TestingApplication();

        using var client = application.CreateClient();

        var resp = await client.GetStringAsync("/");

        resp.Should().Be("Hello World - 123");
    }

    [Fact]
    public async Task People()
    {
        await using var application = new TestingApplication();

        using var client = application.CreateClient();

        var resp = await client.GetFromJsonAsync<List<People>>("/People");

        resp.Count.Should().Be(3);

        var expect = new List<People>
        {
            new(11, "AA"),
            new(22, "BB"),
            new(33, "CC"),
        };

        resp.Should().BeEquivalentTo(expect);
    }
}