using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ClinicalTrials.IntegrationTests.Tests;

public abstract class BaseIntegrationTest : IClassFixture<ClinicalTrialsIntegrationTestFactory>
{
    protected readonly ISender Sender;
    
    protected BaseIntegrationTest(ClinicalTrialsIntegrationTestFactory factory)
    {
        var serviceScope = factory.Services.CreateScope();
        Sender = serviceScope.ServiceProvider.GetRequiredService<ISender>();
    }
}