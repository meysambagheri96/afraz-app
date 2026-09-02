using Autofac;
using Autofac.Extensions.DependencyInjection;
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
            {
                container.AddCommandQueryInternal();
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices((context, services) =>
                {
                    services.AddProblemDetailsInternal();
                    services.AddControllersInternal();
                    services.AddHealthChecks();
                    services.AddSwaggerInternal();
                    services.AddMemoryCache();
                    services.AddDistributedMemoryCache();
                    services.AddApplication();
                    services.AddDbContextInternal(context.Configuration);
                });
                webBuilder.Configure((context, app) =>
                {
                    app.UseExceptionHandler();
                    app.UseSerilogInternal();
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
