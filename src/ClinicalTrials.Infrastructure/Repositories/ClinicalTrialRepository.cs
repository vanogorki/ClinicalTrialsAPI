using ClinicalTrials.Domain.Entities;
using ClinicalTrials.Domain.Interfaces;
using ClinicalTrials.Infrastructure.Data;

namespace ClinicalTrials.Infrastructure.Repositories;

public sealed class ClinicalTrialRepository(DatabaseContext context)
    : Repository<ClinicalTrial>(context), IClinicalTrialRepository;