using Simab.Domain.Common;

namespace Simab.Domain.Events;

/// <summary>
/// Domain event raised when a new evaluation is created
/// </summary>
public record EvaluationCreatedEvent(
    Guid EvaluationId,
    Guid PropertyId,
    Guid EvaluatorId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
