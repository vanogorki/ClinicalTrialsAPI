using ClinicalTrials.Application.Dtos;
using MediatR;

namespace ClinicalTrials.Application.Queries.GetClinicalTrialById;

public sealed record GetClinicalTrialByIdQuery(string TrialId) : IRequest<ClinicalTrialDto>;