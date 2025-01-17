using ClinicalTrials.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace ClinicalTrials.Application.Commands.CreateClinicalTrial;

public sealed record CreateClinicalTrialCommand(IFormFile File) : IRequest<ClinicalTrialDto>;