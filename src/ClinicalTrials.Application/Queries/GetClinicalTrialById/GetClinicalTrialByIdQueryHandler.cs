using AutoMapper;
using ClinicalTrials.Application.Dtos;
using ClinicalTrials.Domain.Interfaces;
using MediatR;

namespace ClinicalTrials.Application.Queries.GetClinicalTrialById;

public class GetClinicalTrialByIdQueryHandler(IMapper mapper, IClinicalTrialRepository repository)
    : IRequestHandler<GetClinicalTrialByIdQuery, ClinicalTrialDto>
{
    public async Task<ClinicalTrialDto> Handle(GetClinicalTrialByIdQuery query, CancellationToken cancellationToken)
    {
        var entity = await repository.GetAsync(e => e.TrialId == query.TrialId, cancellationToken);
        if (entity is null) throw new KeyNotFoundException($"Clinical trial with Id {query.TrialId} not found");

        var result = mapper.Map<ClinicalTrialDto>(entity);
        return result;
    }
}