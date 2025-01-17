using ClinicalTrials.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalTrials.API.Attributes;

internal sealed class FileValidationAttribute : TypeFilterAttribute
{
    internal FileValidationAttribute(string[] allowedExtensions, int maxFileSize)
        : base(typeof(FileValidationFilter))
    {
        Arguments = [allowedExtensions, maxFileSize];
    }
}