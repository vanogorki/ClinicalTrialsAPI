using ClinicalTrials.Application.Common;

namespace ClinicalTrials.API.Modules;

internal static class MapperModule
{
    internal static void AddAutoMapperModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
    }
}