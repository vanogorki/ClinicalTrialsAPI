using ClinicalTrials.API.Middlewares;
using ClinicalTrials.API.Modules;

namespace ClinicalTrials.API;

public sealed class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.AddApplicationModule();

        var app = builder.Build();

        app.MapControllers();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.ApplyMigrations();

        app.Run();
    }
}