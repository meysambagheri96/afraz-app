using System.Net;
using System.Net.Http.Json;
using Afraz.Api;
using Afraz.Api.Contracts;
using Afraz.Application.Features.Foundation.GetStatus;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Afraz.IntegrationTests;

public sealed class ApiRoutingTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiRoutingTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.WithWebHostBuilder(builder => builder.UseSetting("https_port", null)).CreateClient();
    }

    [Fact]
    public async Task StatusEndpoint_ShouldReturnSuccess()
    {
        var response = await _client.GetAsync("/api/status", TestContext.Current.CancellationToken);
        var envelop = await response.Content.ReadFromJsonAsync<Envelop<GetStatusResponse>>(
            TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();
        envelop.Should().NotBeNull();
        envelop!.Meta.Code.Should().Be(HttpStatusCode.OK);
        envelop.Data.Should().BeEquivalentTo(new GetStatusResponse("Afraz.Api", "ready"));
    }

    [Fact]
    public async Task UnknownApiEndpoint_ShouldNotReturnSpa()
    {
        var response = await _client.GetAsync(
            "/api/does-not-exist",
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        response.Content.Headers.ContentType?.MediaType.Should().Be("application/problem+json");

        var problemDetails = await response.Content.ReadFromJsonAsync<ProblemDetails>(
            TestContext.Current.CancellationToken);
        problemDetails.Should().NotBeNull();
        problemDetails!.Status.Should().Be((int)HttpStatusCode.NotFound);
        problemDetails.Title.Should().Be("API endpoint not found.");
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/orders")]
    [InlineData("/booking")]
    [InlineData("/profile")]
    [InlineData("/store")]
    public async Task FrontendRoute_ShouldReturnVueIndex(string route)
    {
        var response = await _client.GetAsync(route, TestContext.Current.CancellationToken);
        var content = await response.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.MediaType.Should().Be("text/html");
        content.Should().Contain("<div id=\"app\"></div>");
    }
}
