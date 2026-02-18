namespace Simab.Domain.Enums;

/// <summary>
/// Status of a property visit
/// </summary>
public enum VisitStatus
{
    Scheduled = 0,
    InProgress = 1,
    Completed = 2,
    Cancelled = 3,
    Rescheduled = 4
}
