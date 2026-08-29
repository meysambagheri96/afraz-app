namespace Afraz.Api;

public static class AppExtensions
{
    public static void UseSwaggerInternal(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Afraz Studio API v1");
            options.DisplayRequestDuration();
        });
    }

    public static void UseEndpointsInternal(this IApplicationBuilder app)
    {
        app.UseEndpoints(endpoints => endpoints.MapControllers());
    }
}
