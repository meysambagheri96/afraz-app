using Afraz.Application;
using Afraz.Application.Features.Foundation.GetStatus;
using Afraz.Infrastructure.Persistence;
using Autofac;
using FluentValidation;
using Infra.Commands;
using Infra.Common.Decorators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using StackExchange.Redis;

namespace Afraz.Api;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(GetStatusQuery).Assembly);

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

    public static ContainerBuilder AddCommandQueryInternal(this ContainerBuilder container)
    {
        container.AddCommandQuery(scannedAssemblies: typeof(GetStatusQuery).Assembly);
     
        return container;
    }
}
