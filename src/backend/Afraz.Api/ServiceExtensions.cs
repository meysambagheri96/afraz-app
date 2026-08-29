using Afraz.Application;
using Afraz.Application.Features.Foundation.GetStatus;
using Afraz.Infrastructure.Persistence;
using Autofac;
using FluentValidation;
using Infra.Commands;
using Infra.Common.Decorators;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using StackExchange.Redis;

namespace Afraz.Api;

public static class ServiceExtensions
{
    public static ContainerBuilder AddCommandQueryInternal(this ContainerBuilder container)
    {
        container.AddCommandQuery(scannedAssemblies: typeof(GetStatusQuery).Assembly);
        container.RegisterGeneric(typeof(FluentValidationCommandValidator<>))
            .As(typeof(ICommandValidator<>))
            .InstancePerLifetimeScope();

        return container;
    }

    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(GetStatusQuery).Assembly);

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

    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "ConnectionStrings:DefaultConnection is required.");

        services.AddDbContext<AfrazDbContext>(options => options.UseSqlServer(
            connectionString,
            sql => sql.MigrationsAssembly(typeof(AfrazDbContext).Assembly.FullName)));

        var redisConnection = configuration["Redis:ConnectionString"]
            ?? throw new InvalidOperationException("Redis:ConnectionString is required.");

        services.AddSingleton<IConnectionMultiplexer>(_ =>
            ConnectionMultiplexer.Connect(new ConfigurationOptions
            {
                AbortOnConnectFail = false,
                EndPoints = { redisConnection },
            }));

        return services;
    }
}
