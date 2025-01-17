using ClinicalTrials.Domain.Interfaces;
using ClinicalTrials.Infrastructure.Data;
using ClinicalTrials.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ClinicalTrials.API.Modules;

internal static class InfrastructureModule
{
    internal static void AddInfrastructureModule(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");
        builder.Services.AddDbContext<DatabaseContext>(options =>
            options.UseNpgsql(connectionString));

        builder.Services.AddScoped<IClinicalTrialRepository, ClinicalTrialRepository>();
    }
}