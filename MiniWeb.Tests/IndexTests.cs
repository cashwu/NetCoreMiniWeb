using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MiniWeb.Tests;

public class IndexTests
{
    [Fact]
    public async Task Index()
    {
        await using var application = new WebApplicationFactory<Program>();

        using var client = application.CreateClient();

        var resp = await client.GetStringAsync("/");

        resp.Should().Be("Hello World");
    }
}