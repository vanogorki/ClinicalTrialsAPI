namespace ClinicalTrials.Application.Dtos;

public abstract class BaseEntityDto
{
    public long Id { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset? LastModifiedAt { get; init; }
}