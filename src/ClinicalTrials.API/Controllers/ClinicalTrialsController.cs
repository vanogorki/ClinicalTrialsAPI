using System.Net;
using ClinicalTrials.API.Attributes;
using ClinicalTrials.Application.Commands.CreateClinicalTrial;
using ClinicalTrials.Application.Common;
using ClinicalTrials.Application.Dtos;
using ClinicalTrials.Application.Dtos.Models.Requests;
using ClinicalTrials.Application.Dtos.Models.Responses;
using ClinicalTrials.Application.Queries.GetClinicalTrialById;
using ClinicalTrials.Application.Queries.GetClinicalTrials;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicalTrials.API.Controllers;

[ApiController]
[Route("api/clinicaltrials")]
public sealed class ClinicalTrialsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(typeof(ClinicalTrialsFilterResponse), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(BaseApiResponse))]
    public async Task<IActionResult> Get([FromQuery] ClinicalTrialsFilterRequest model,
        CancellationToken cancellationToken)
    {
        var request = new GetClinicalTrialsQuery(model);
        var response = await mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(typeof(ClinicalTrialDto), (int)HttpStatusCode.OK)]
    [ProducesErrorResponseType(typeof(BaseApiResponse))]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var request = new GetClinicalTrialByIdQuery(id);
        var response = await mediator.Send(request, cancellationToken);

        return Ok(response);
    }

    [HttpPost]
    [FileValidation([".json"], 1048576)]
    [ProducesResponseType(typeof(ClinicalTrialDto), (int)HttpStatusCode.Created)]
    [ProducesErrorResponseType(typeof(BaseApiResponse))]
    public async Task<IActionResult> Post(IFormFile file, CancellationToken cancellationToken)
    {
        var request = new CreateClinicalTrialCommand(file);
        var response = await mediator.Send(request, cancellationToken);

        return StatusCode((int)HttpStatusCode.Created, response);
    }
}