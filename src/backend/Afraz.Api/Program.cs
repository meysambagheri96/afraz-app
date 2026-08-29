using System.Net;
using Afraz.Api.Contracts;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Afraz.Api;

public sealed class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .UseSerilog((context, services, configuration) => configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", "Afraz.Api")
                .WriteTo.Console())
            .ConfigureContainer<ContainerBuilder>((_, container) =>
                container.AddCommandQueryInternal())
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                    services.AddExceptionHandler<GlobalExceptionHandler>();
                    services.AddExceptionHandler(options =>
                        options.ExceptionHandler = _ => Task.CompletedTask);
                    services.AddControllers().ConfigureApiBehaviorOptions(options =>
                    {
                        options.InvalidModelStateResponseFactory = context =>
                        {
                            var errors = context.ModelState
                                .Where(entry => entry.Value?.Errors.Count > 0)
                                .Select(entry => new ApiErrorEntry(
                                    entry.Key,
                                    (int)HttpStatusCode.BadRequest,
                                    entry.Value!.Errors
                                        .Select(error => error.ErrorMessage)
                                        .ToArray()))
                                .ToArray();

                            return new BadRequestObjectResult(
                                Envelop<object?>.HandledError(
                                    HttpStatusCode.BadRequest,
                                    errors,
                                    "Validation failed."));
                        };
                    });
                    services.AddSwaggerInternal();
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddApplication();
                    services.AddInfrastructure(context.Configuration);
                });
                webBuilder.Configure((context, app) =>
                {
                    app.UseExceptionHandler();
                    app.UseSerilogRequestLogging(options =>
                        options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
                            diagnosticContext.Set("TraceId", httpContext.TraceIdentifier));
                    app.UseHttpsRedirection();
                    app.UseDefaultFiles();
                    app.UseStaticFiles();
                    app.UseSwaggerInternal();
                    app.UseRouting();
                    app.UseEndpointsInternal();
                });
                webBuilder.CaptureStartupErrors(true);
            });
}
