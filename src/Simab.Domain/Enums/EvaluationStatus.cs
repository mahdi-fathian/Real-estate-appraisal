namespace Simab.Domain.Enums;

/// <summary>
/// Status of an evaluation
/// </summary>
public enum EvaluationStatus
{
    Pending = 0,
    InProgress = 1,
    UnderReview = 2,
    Completed = 3,
    Cancelled = 4,
    Rejected = 5
}
