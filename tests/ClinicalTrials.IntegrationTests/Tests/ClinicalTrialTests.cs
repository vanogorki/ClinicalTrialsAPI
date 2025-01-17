using System.ComponentModel.DataAnnotations;
using ClinicalTrials.Application.Commands.CreateClinicalTrial;
using ClinicalTrials.Application.Dtos.Models.Requests;
using ClinicalTrials.Application.Queries.GetClinicalTrialById;
using ClinicalTrials.Application.Queries.GetClinicalTrials;
using FluentAssertions;
using Microsoft.AspNetCore.Http;

namespace ClinicalTrials.IntegrationTests.Tests;

public sealed class ClinicalTrialTests(ClinicalTrialsIntegrationTestFactory factory) : BaseIntegrationTest(factory)
{
    [Fact]
    public async Task CreateClinicalTrialCommand_ShouldReturnClinicalTrialDto()
    {
        // Arrange
        var file = PrepareFile(0);
        var command = new CreateClinicalTrialCommand(file);

        // Act
        var result = await Sender.Send(command);

        // Assert
        result.Should().NotBeNull();
        result.TrialId.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public async Task GetClinicalTrialByIdQuery_ShouldReturnClinicalTrialDto()
    {
        // Arrange
        // First create a clinical trial and then get it by id
        var file = PrepareFile(1);
        var createCommand = new CreateClinicalTrialCommand(file);
        var createResult = await Sender.Send(createCommand);
        var query = new GetClinicalTrialByIdQuery(createResult.TrialId);
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.TrialId.Should().NotBeNullOrWhiteSpace();
    }
    
    [Fact]
    public async Task GetClinicalTrialsQuery_ShouldReturnListOfClinicalTrialDto()
    {
        // Arrange
        // First create a clinical trial and then get all clinical trials
        var file = PrepareFile(2);
        var createCommand = new CreateClinicalTrialCommand(file);
        await Sender.Send(createCommand);
        var query = new GetClinicalTrialsQuery(new ClinicalTrialsFilterRequest());
        
        // Act
        var result = await Sender.Send(query);
        
        // Assert
        result.Should().NotBeNull();
        result.ClinicalTrials.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task CreateClinicalTrialCommand_WithInvalidJson_ShouldThrowValidationException()
    {
        // Arrange
        var file = PrepareFile(3);
        var command = new CreateClinicalTrialCommand(file);
        
        // Act
        Func<Task> act = async () => await Sender.Send(command);
        
        // Assert
        await act.Should().ThrowAsync<ValidationException>();
    }

    private FormFile PrepareFile(int fileNumber)
    {
        var filePath = $"TestFiles/testFile_{fileNumber}.json";
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var fileInfo = new FileInfo(filePath);
        
        return new FormFile(fileStream, 0, fileStream.Length, "file", fileInfo.Name);
    }
}