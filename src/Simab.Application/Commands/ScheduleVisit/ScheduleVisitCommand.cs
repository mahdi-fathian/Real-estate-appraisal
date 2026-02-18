using MediatR;

namespace Simab.Application.Commands.ScheduleVisit;

/// <summary>
/// Command to schedule a property visit
/// </summary>
public record ScheduleVisitCommand(
    Guid EvaluationId,
    Guid EvaluatorId,
    DateTime ScheduledDate) : IRequest<Guid>;
