using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ClinicalTrials.API.Filters;

internal sealed class FileValidationFilter(string[] allowedExtensions, int maxFileSize) : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var file = context.ActionArguments.Values.OfType<IFormFile>().FirstOrDefault();
        if (file != null)
        {
            var extension = Path.GetExtension(file.FileName).ToLower();
            if (!allowedExtensions.Contains(extension))
            {
                context.Result = new BadRequestObjectResult($"File extension {extension} is not allowed!");
                return;
            }

            if (file.Length > maxFileSize)
            {
                context.Result = new BadRequestObjectResult($"Maximum allowed file size is {maxFileSize / (1024 * 1024)} MB.");
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}