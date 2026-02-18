using Simab.Domain.Common;
using Simab.Domain.ValueObjects;

namespace Simab.Domain.Events;

/// <summary>
/// Domain event raised when an evaluation is completed
/// </summary>
public record EvaluationCompletedEvent(
    Guid EvaluationId,
    Guid PropertyId,
    Money EstimatedValue) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}
