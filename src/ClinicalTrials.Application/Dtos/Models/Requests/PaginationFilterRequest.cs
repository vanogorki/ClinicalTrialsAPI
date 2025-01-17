namespace ClinicalTrials.Application.Dtos.Models.Requests;

public abstract class PaginationFilterRequest
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}