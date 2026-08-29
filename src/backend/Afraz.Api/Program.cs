using Afraz.Api;
using Afraz.Application;
using Afraz.Application.Features.Foundation.GetStatus;
using Afraz.Infrastructure;
using MediatR;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext()
    .Enrich.WithProperty("Application", "Afraz.Api")
    .WriteTo.Console());

builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandler();
app.UseSerilogRequestLogging(options =>
    options.EnrichDiagnosticContext = (diagnosticContext, httpContext) =>
        diagnosticContext.Set("TraceId", httpContext.TraceIdentifier));
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapHealthChecks("/health");

var api = app.MapGroup("/api");
api.MapGet("/status", async (ISender sender, CancellationToken cancellationToken) =>
    Results.Ok(await sender.Send(new GetStatusQuery(), cancellationToken)));

app.Map("/api/{**path}", () => Results.Problem(
    statusCode: StatusCodes.Status404NotFound,
    title: "API endpoint not found"));
app.MapFallbackToFile("index.html");

app.Run();

public partial class Program;
