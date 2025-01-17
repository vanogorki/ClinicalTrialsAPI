namespace ClinicalTrials.Application.Common;

public sealed class BaseApiResponse(bool isSuccess = false, string? message = null, object? data = null)
{
    public bool IsSuccess { get; set; } = isSuccess;
    public string? Message { get; set; } = message;
    public object? Data { get; set; } = data;
    public string? TraceIdentifier { get; set; }
}