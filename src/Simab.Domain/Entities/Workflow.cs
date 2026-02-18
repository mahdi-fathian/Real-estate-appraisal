using Simab.Domain.Common;
using Simab.Domain.Enums;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents workflow assignment and referral in the system
/// </summary>
public class Workflow : Entity
{
    public Guid EvaluationId { get; private set; }
    public Guid? AssignedToId { get; private set; }
    public Guid? ReferredFromId { get; private set; }
    public WorkflowStatus Status { get; private set; }
    public int Priority { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? AssignedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? Notes { get; private set; }
    
    // Workflow constraints
    public int? MaxReferralCount { get; private set; }
    public int CurrentReferralCount { get; private set; }
    public DateTime? Deadline { get; private set; }
    public string? AllowedLocation { get; private set; }
    
    private Workflow() { }
    
    public Workflow(
        Guid evaluationId,
        Guid? assignedToId = null,
        int priority = 0)
    {
        EvaluationId = evaluationId;
        AssignedToId = assignedToId;
        Priority = priority;
        Status = WorkflowStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        CurrentReferralCount = 0;
    }
    
    public void AssignTo(Guid evaluatorId)
    {
        AssignedToId = evaluatorId;
        AssignedAt = DateTime.UtcNow;
        Status = WorkflowStatus.Assigned;
    }
    
    public void ReferTo(Guid newEvaluatorId, string? reason = null)
    {
        if (MaxReferralCount.HasValue && CurrentReferralCount >= MaxReferralCount.Value)
            throw new InvalidOperationException($"Maximum referral count ({MaxReferralCount.Value}) has been reached");
            
        CurrentReferralCount++;
        ReferredFromId = AssignedToId;
        AssignedToId = newEvaluatorId;
        AssignedAt = DateTime.UtcNow;
        Status = WorkflowStatus.Referred;
        Notes = reason;
    }
    
    public void Complete()
    {
        if (Status == WorkflowStatus.Completed)
            throw new InvalidOperationException("Workflow is already completed");
            
        Status = WorkflowStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }
    
    public void SetConstraints(
        int? maxReferralCount = null,
        DateTime? deadline = null,
        string? allowedLocation = null)
    {
        MaxReferralCount = maxReferralCount;
        Deadline = deadline;
        AllowedLocation = allowedLocation;
    }
    
    public bool CanBeReferred()
    {
        if (!MaxReferralCount.HasValue)
            return true;
            
        return CurrentReferralCount < MaxReferralCount.Value;
    }
    
    public bool IsDeadlinePassed()
    {
        return Deadline.HasValue && DateTime.UtcNow > Deadline.Value;
    }
}
