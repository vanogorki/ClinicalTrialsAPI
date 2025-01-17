using ClinicalTrials.Application.Common;

namespace ClinicalTrials.API.Modules;

internal static class MediatrModule
{
    internal static void AddMediatrModule(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(BaseApiResponse).Assembly));
    }
}