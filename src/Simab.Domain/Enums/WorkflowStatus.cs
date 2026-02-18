namespace Simab.Domain.Enums;

/// <summary>
/// Status of workflow assignment
/// </summary>
public enum WorkflowStatus
{
    Pending = 0,
    Assigned = 1,
    InProgress = 2,
    Referred = 3,
    Completed = 4,
    Cancelled = 5
}
