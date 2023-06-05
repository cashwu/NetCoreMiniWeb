using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

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
}