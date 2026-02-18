using Microsoft.AspNetCore.Mvc;
using MediatR;
using Simab.Application.Commands.CreateEvaluation;
using Simab.Application.Queries.GetEvaluation;

namespace Simab.Api.Controllers;

/// <summary>
/// Controller for evaluation operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class EvaluationsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public EvaluationsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Creates a new evaluation
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateEvaluation([FromBody] CreateEvaluationCommand command)
    {
        var evaluationId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetEvaluation), new { id = evaluationId }, evaluationId);
    }
    
    /// <summary>
    /// Gets an evaluation by ID
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Simab.Application.Common.Dtos.EvaluationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEvaluation(Guid id)
    {
        var query = new GetEvaluationQuery(id);
        var evaluation = await _mediator.Send(query);
        
        if (evaluation == null)
            return NotFound();
            
        return Ok(evaluation);
    }
}
