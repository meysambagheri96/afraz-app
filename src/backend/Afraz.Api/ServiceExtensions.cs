using System.Text;
using System.Threading.RateLimiting;
using Afraz.Api.Authentication;
using Afraz.Application.Features.Authentication;
using Afraz.Application.Features.Foundation.GetStatus;
using Afraz.Infrastructure.Authentication;
using Afraz.Infrastructure.Persistence;
using Autofac;
using Infra.Common.Decorators;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

namespace Afraz.Api;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services;
    }

    public static IServiceCollection AddProblemDetailsInternal(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = problemContext =>
            {
                problemContext.ProblemDetails.Extensions["traceId"] = problemContext.HttpContext.TraceIdentifier;
            };
        });

        services.AddExceptionHandler<GlobalExceptionHandler>();

        return services;
    }

    public static IServiceCollection AddControllersInternal(this IServiceCollection services)
    {
        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var problemDetails = new ValidationProblemDetails(context.ModelState)
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation failed.",
                };

                problemDetails.Extensions["traceId"] =
                    context.HttpContext.TraceIdentifier;

                return new BadRequestObjectResult(problemDetails);
            };
        });
        return services;
    }

    public static IServiceCollection AddSwaggerInternal(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Afraz Studio API",
                Version = "v1",
                Description = "HTTP API for Afraz Studio.",
            });
        });

        return services;
    }

    public static IServiceCollection AddDbContextInternal(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "ConnectionStrings:DefaultConnection is required.");

        services.AddDbContext<AfrazDbContext>(options => options.UseSqlServer(
            connectionString,
            sql => sql.MigrationsAssembly(typeof(AfrazDbContext).Assembly.FullName)));

        return services;
    }

    public static IServiceCollection AddAuthenticationInternal(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        var jwt = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>() ?? new JwtOptions();
        if (string.IsNullOrWhiteSpace(jwt.SigningKey))
        {
            throw new InvalidOperationException("Jwt:SigningKey configuration is required.");
        }

        var google = configuration.GetSection(GoogleOptions.SectionName).Get<GoogleOptions>()
            ?? new GoogleOptions();

        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(jwt));
        services.AddSingleton(Microsoft.Extensions.Options.Options.Create(google));
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUser, HttpCurrentUser>();
        services.AddScoped<IAuthRepository, AuthRepository>();
        services.AddSingleton<ISecretHasher, SecretHasher>();
        services.AddSingleton<IOtpCodeGenerator>(environment.IsDevelopment()
            ? new DevelopmentOtpCodeGenerator()
            : new SecureOtpCodeGenerator());
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddSingleton<IOtpSender, NoOpOtpSender>();
        services.AddHttpClient<IGoogleIdentityService, GoogleIdentityService>();

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwt.Issuer,
                ValidateAudience = true,
                ValidAudience = jwt.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),
            });
        services.AddAuthorization();
        services.AddRateLimiter(options => options.AddPolicy("auth", httpContext =>
            RateLimitPartition.GetFixedWindowLimiter(
                httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 10,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0,
                    AutoReplenishment = true,
                })));
        return services;
    }

    public static ContainerBuilder AddCommandQueryInternal(this ContainerBuilder container)
    {
        container.AddCommandQuery(scannedAssemblies: typeof(GetStatusQuery).Assembly);
     
        return container;
    }
}
