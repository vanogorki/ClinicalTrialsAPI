using ClinicalTrials.Application.Dtos.Models.Requests;
using ClinicalTrials.Application.Dtos.Models.Responses;
using MediatR;

namespace ClinicalTrials.Application.Queries.GetClinicalTrials;

public sealed record GetClinicalTrialsQuery(ClinicalTrialsFilterRequest FilterRequest)
    : IRequest<ClinicalTrialsFilterResponse>;