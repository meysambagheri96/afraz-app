using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Afraz.Api;
using Afraz.Api.Contracts;
using Afraz.Application.Features.Authentication;
using Afraz.Application.Features.Authentication.Logout;
using Afraz.Application.Features.Authentication.Otp;
using Afraz.Infrastructure.Persistence;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;

namespace Afraz.IntegrationTests;

public sealed class AuthenticationApiTests : IClassFixture<AuthenticationApiFactory>
{
    private readonly HttpClient _client;
    private readonly CapturingOtpSender _otpSender;
    private readonly AuthenticationApiFactory _factory;

    public AuthenticationApiTests(AuthenticationApiFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
        _otpSender = factory.Services.GetRequiredService<CapturingOtpSender>();
    }

    [Fact]
    public async Task RegisterLoginRefreshLogout_ShouldRotateAndRevokeTokens()
    {
        var phone = $"912{Random.Shared.Next(1000000, 9999999)}";
        var register = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            phone,
            password = "ValidPassword!42",
            firstName = "Meysam",
            lastName = "Bagheri",
        }, TestContext.Current.CancellationToken);
        var registered = await register.Content.ReadFromJsonAsync<Envelop<AuthTokensResponse>>(
            TestContext.Current.CancellationToken);

        register.StatusCode.Should().Be(HttpStatusCode.OK);
        registered!.Data!.AccessToken.Should().NotBeNullOrWhiteSpace();
        registered.Data.RefreshToken.Should().NotBeNullOrWhiteSpace();

        var login = await _client.PostAsJsonAsync("/api/auth/login", new
        {
            phone,
            password = "ValidPassword!42",
        }, TestContext.Current.CancellationToken);
        var loggedIn = await login.Content.ReadFromJsonAsync<Envelop<AuthTokensResponse>>(
            TestContext.Current.CancellationToken);
        login.StatusCode.Should().Be(HttpStatusCode.OK);

        var refresh = await _client.PostAsJsonAsync("/api/auth/refresh", new
        {
            refreshToken = loggedIn!.Data!.RefreshToken,
        }, TestContext.Current.CancellationToken);
        var refreshed = await refresh.Content.ReadFromJsonAsync<Envelop<AuthTokensResponse>>(
            TestContext.Current.CancellationToken);
        refresh.StatusCode.Should().Be(HttpStatusCode.OK);
        refreshed!.Data!.RefreshToken.Should().NotBe(loggedIn.Data.RefreshToken);

        var replay = await _client.PostAsJsonAsync("/api/auth/refresh", new
        {
            refreshToken = loggedIn.Data.RefreshToken,
        }, TestContext.Current.CancellationToken);
        replay.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var logout = await _client.PostAsJsonAsync("/api/auth/logout", new
        {
            refreshToken = refreshed.Data.RefreshToken,
        }, TestContext.Current.CancellationToken);
        var logoutResult = await logout.Content.ReadFromJsonAsync<Envelop<LogoutResponse>>(
            TestContext.Current.CancellationToken);
        logoutResult!.Data!.Revoked.Should().BeTrue();

        var revoked = await _client.PostAsJsonAsync("/api/auth/refresh", new
        {
            refreshToken = refreshed.Data.RefreshToken,
        }, TestContext.Current.CancellationToken);
        revoked.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task OtpVerification_ShouldActivateUserAndReturnTokens()
    {
        var phone = $"913{Random.Shared.Next(1000000, 9999999)}";
        var request = await _client.PostAsJsonAsync("/api/auth/otp/request", new { phone },
            TestContext.Current.CancellationToken);
        request.StatusCode.Should().Be(HttpStatusCode.OK,
            await request.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));
        _otpSender.LastCode.Should().MatchRegex("^\\d{6}$");
        using (var scope = _factory.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<AfrazDbContext>();
            var user = await db.Users.Include(x => x.Otps).SingleAsync(
                x => x.Phone == phone,
                TestContext.Current.CancellationToken);
            var otp = user.Otps.Single();
            otp.CanVerify(DateTime.UtcNow).Should().BeTrue();
            scope.ServiceProvider.GetRequiredService<ISecretHasher>()
                .Verify(_otpSender.LastCode, otp.CodeHash).Should().BeTrue();
        }

        var verify = await _client.PostAsJsonAsync("/api/auth/otp/verify", new
        {
            phone,
            code = _otpSender.LastCode,
        }, TestContext.Current.CancellationToken);
        verify.StatusCode.Should().Be(HttpStatusCode.OK,
            await verify.Content.ReadAsStringAsync(TestContext.Current.CancellationToken));
        var verified = await verify.Content.ReadFromJsonAsync<Envelop<AuthTokensResponse>>(
            TestContext.Current.CancellationToken);
        verified!.Data!.User.IsActive.Should().BeTrue();
    }

    [Fact]
    public async Task CurrentUser_ShouldRequireAndAcceptBearerToken()
    {
        var unauthorized = await _client.GetAsync("/api/users/me", TestContext.Current.CancellationToken);
        unauthorized.StatusCode.Should().Be(HttpStatusCode.Unauthorized);

        var phone = $"914{Random.Shared.Next(1000000, 9999999)}";
        var register = await _client.PostAsJsonAsync("/api/auth/register", new
        {
            phone,
            password = "ValidPassword!42",
            firstName = "Afraz",
            lastName = "Customer",
        }, TestContext.Current.CancellationToken);
        var auth = await register.Content.ReadFromJsonAsync<Envelop<AuthTokensResponse>>(
            TestContext.Current.CancellationToken);
        using var request = new HttpRequestMessage(HttpMethod.Get, "/api/users/me");
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth!.Data!.AccessToken);
        var response = await _client.SendAsync(request, TestContext.Current.CancellationToken);
        response.StatusCode.Should().Be(HttpStatusCode.OK,
            string.Join("; ", response.Headers.WwwAuthenticate.Select(value => value.ToString())));

        using var update = new HttpRequestMessage(HttpMethod.Put, "/api/users/me")
        {
            Content = JsonContent.Create(new
            {
                firstName = "Updated",
                lastName = "Customer",
                email = "updated@example.com",
            }),
        };
        update.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Data.AccessToken);
        var updated = await _client.SendAsync(update, TestContext.Current.CancellationToken);
        updated.StatusCode.Should().Be(HttpStatusCode.OK);

        using var delete = new HttpRequestMessage(HttpMethod.Delete, "/api/users/me");
        delete.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Data.AccessToken);
        var deleted = await _client.SendAsync(delete, TestContext.Current.CancellationToken);
        deleted.StatusCode.Should().Be(HttpStatusCode.OK);

        using var afterDelete = new HttpRequestMessage(HttpMethod.Get, "/api/users/me");
        afterDelete.Headers.Authorization = new AuthenticationHeaderValue("Bearer", auth.Data.AccessToken);
        var deactivated = await _client.SendAsync(afterDelete, TestContext.Current.CancellationToken);
        deactivated.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GoogleLogin_ShouldCreateUserAndReturnTokens()
    {
        var response = await _client.PostAsJsonAsync("/api/auth/google", new
        {
            authorizationCode = "verified-google-code",
        }, TestContext.Current.CancellationToken);
        var auth = await response.Content.ReadFromJsonAsync<Envelop<AuthTokensResponse>>(
            TestContext.Current.CancellationToken);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        auth!.Data!.AccessToken.Should().NotBeNullOrWhiteSpace();
        auth.Data.User.Email.Should().Be("customer@example.com");
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AfrazDbContext>();
        var user = await db.Users.SingleAsync(
            user => user.GoogleSubject == "google-subject-123",
            TestContext.Current.CancellationToken);
        user.Email.Should().Be("customer@example.com");
    }
}

public sealed class AuthenticationApiFactory : WebApplicationFactory<Program>
{
    private readonly string _databaseName = $"afraz-auth-tests-{Guid.NewGuid():N}";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Development");
        builder.UseSetting("https_port", null);
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<AfrazDbContext>>();
            services.RemoveAll<IDbContextOptionsConfiguration<AfrazDbContext>>();
            services.RemoveAll<AfrazDbContext>();
            services.AddDbContext<AfrazDbContext>(options =>
                options.UseInMemoryDatabase(_databaseName));
            services.RemoveAll<IOtpSender>();
            services.AddSingleton<CapturingOtpSender>();
            services.AddSingleton<IOtpSender>(provider => provider.GetRequiredService<CapturingOtpSender>());
            services.RemoveAll<IGoogleIdentityService>();
            services.AddSingleton<IGoogleIdentityService, FakeGoogleIdentityService>();
        });
    }
}

public sealed class FakeGoogleIdentityService : IGoogleIdentityService
{
    public Task<GoogleIdentity> GetIdentityAsync(string authorizationCode, CancellationToken cancellationToken) =>
        Task.FromResult(new GoogleIdentity(
            "google-subject-123",
            "customer@example.com",
            "Google",
            "Customer",
            null));
}

public sealed class CapturingOtpSender : IOtpSender
{
    public string LastCode { get; private set; } = string.Empty;

    public Task SendAsync(string dialingCode, string phone, string code, CancellationToken cancellationToken)
    {
        LastCode = code;
        return Task.CompletedTask;
    }
}
