using Microsoft.AspNetCore.Mvc;
using MediatR;
using Simab.Application.Commands.ScheduleVisit;

namespace Simab.Api.Controllers;

/// <summary>
/// Controller for visit operations
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
public class VisitsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public VisitsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Schedules a property visit
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ScheduleVisit([FromBody] ScheduleVisitCommand command)
    {
        var visitId = await _mediator.Send(command);
        return CreatedAtAction(nameof(ScheduleVisit), new { id = visitId }, visitId);
    }
}
