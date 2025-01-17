namespace ClinicalTrials.Application.Dtos.Models.Responses;

public sealed class ClinicalTrialsFilterResponse : PaginationFilterResponse
{
    public List<ClinicalTrialDto> ClinicalTrials { get; set; } = null!;
}