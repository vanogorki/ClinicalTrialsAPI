using ClinicalTrials.API.Middlewares;
using ClinicalTrials.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ClinicalTrials.API.Modules;

internal static class ApplicationModule
{
    internal static void AddApplicationModule(this WebApplicationBuilder builder)
    {
        builder.AddMediatrModule();
        builder.AddInfrastructureModule();
        builder.AddAutoMapperModule();

        builder.Services.AddLogging(options => { options.AddConsole(); });
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddScoped<ExceptionMiddleware>();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc(
                "v1",
                new OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
        });
    }
    
    internal static void ApplyMigrations(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<DatabaseContext>();
        context.Database.Migrate();
    }
}