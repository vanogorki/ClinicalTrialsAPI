using ClinicalTrials.Domain.Enums;

namespace ClinicalTrials.Application.Dtos;

public sealed class ClinicalTrialDto : BaseEntityDto
{
    public string TrialId { get; init; } = null!;
    public string Title { get; init; } = null!;
    public DateTimeOffset StartDate { get; init; }
    public DateTimeOffset EndDate { get; init; }
    public int Participants { get; init; }
    public TrialStatus Status { get; init; }
    public int DurationInDays { get; init; }
}