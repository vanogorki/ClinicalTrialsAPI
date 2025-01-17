using System.ComponentModel.DataAnnotations;
using AutoMapper;
using ClinicalTrials.Application.Common.Helpers;
using ClinicalTrials.Application.Dtos;
using ClinicalTrials.Domain.Entities;
using ClinicalTrials.Domain.Enums;
using ClinicalTrials.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace ClinicalTrials.Application.Commands.CreateClinicalTrial;

public sealed class CreateClinicalTrialCommandHandler(IMapper mapper, IClinicalTrialRepository repository)
    : IRequestHandler<CreateClinicalTrialCommand, ClinicalTrialDto>
{
    public async Task<ClinicalTrialDto> Handle(CreateClinicalTrialCommand command, CancellationToken cancellationToken)
    {
        var json = await ReadFileAsync(command.File);

        var clinicalTrial = JsonConvert.DeserializeObject<ClinicalTrial>(json, new JsonSerializerSettings
        {
            Converters = { new TrialStatusConverter() }
        });
        if (clinicalTrial is null) throw new Exception("Invalid JSON data");

        // if end date is not specified and trial status is 'Ongoing', set end date to 1 month from the start date
        if (clinicalTrial.EndDate == default && clinicalTrial.Status == TrialStatus.Ongoing)
            clinicalTrial.EndDate = clinicalTrial.StartDate.AddMonths(1);

        if (clinicalTrial.StartDate > clinicalTrial.EndDate)
            throw new ValidationException("Start date cannot be greater than end date");

        // Convert DateTimeOffset properties to UTC
        clinicalTrial.StartDate = clinicalTrial.StartDate.ToUniversalTime();
        clinicalTrial.EndDate = clinicalTrial.EndDate.ToUniversalTime();

        // calculate the duration of the trial in days
        clinicalTrial.DurationInDays = (clinicalTrial.EndDate - clinicalTrial.StartDate).Days;

        await repository.AddAsync(clinicalTrial, cancellationToken);
        await repository.SaveChangesAsync(cancellationToken);

        var result = mapper.Map<ClinicalTrialDto>(clinicalTrial);
        return result;
    }

    private async Task<string> ReadFileAsync(IFormFile file)
    {
        using var stream = new StreamReader(file.OpenReadStream());
        var json = await stream.ReadToEndAsync();
        var jsonObject = JObject.Parse(json);

        var jsonSchema = JSchema.Parse(await File.ReadAllTextAsync("Schemas/jsonSchema.json"));
        if (!jsonObject.IsValid(jsonSchema, out IList<string> errorMessages))
            throw new ValidationException("Invalid JSON data: " + string.Join(", ", errorMessages));

        return json;
    }
}