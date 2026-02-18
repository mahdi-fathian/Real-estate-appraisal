using Simab.Domain.Common;
using Simab.Domain.Enums;
using Simab.Domain.ValueObjects;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents a property evaluation in the system
/// </summary>
public class Evaluation : Entity
{
    private readonly List<Document> _documents = new();
    private readonly List<Visit> _visits = new();
    
    public Guid PropertyId { get; private set; }
    public Guid EvaluatorId { get; private set; }
    public Guid? CustomerId { get; private set; }
    public EvaluationStatus Status { get; private set; }
    public EvaluationType Type { get; private set; }
    public Money EstimatedValue { get; private set; } = null!;
    public DateTime CreatedAt { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public string? Notes { get; private set; }
    public bool IsEncrypted { get; private set; }
    public string? DigitalSignature { get; private set; }
    
    public IReadOnlyCollection<Document> Documents => _documents.AsReadOnly();
    public IReadOnlyCollection<Visit> Visits => _visits.AsReadOnly();
    
    private Evaluation() { }
    
    public Evaluation(
        Guid propertyId,
        Guid evaluatorId,
        EvaluationType type,
        Money estimatedValue,
        Guid? customerId = null)
    {
        if (estimatedValue == null)
            throw new ArgumentNullException(nameof(estimatedValue));
            
        PropertyId = propertyId;
        EvaluatorId = evaluatorId;
        CustomerId = customerId;
        Type = type;
        EstimatedValue = estimatedValue;
        Status = EvaluationStatus.Pending;
        CreatedAt = DateTime.UtcNow;
        IsEncrypted = false;
        
        // Domain event will be added through base class
    }
    
    public void AddDocument(Document document)
    {
        if (document == null)
            throw new ArgumentNullException(nameof(document));
            
        _documents.Add(document);
    }
    
    public void AddVisit(Visit visit)
    {
        if (visit == null)
            throw new ArgumentNullException(nameof(visit));
            
        _visits.Add(visit);
    }
    
    public void UpdateEstimatedValue(Money newValue)
    {
        if (newValue == null)
            throw new ArgumentNullException(nameof(newValue));
            
        if (Status == EvaluationStatus.Completed)
            throw new InvalidOperationException("Cannot update completed evaluation");
            
        EstimatedValue = newValue;
    }
    
    public void UpdateStatus(EvaluationStatus newStatus)
    {
        if (Status == EvaluationStatus.Completed && newStatus != EvaluationStatus.Completed)
            throw new InvalidOperationException("Cannot change status of completed evaluation");
            
        Status = newStatus;
        
        if (newStatus == EvaluationStatus.Completed)
        {
            CompletedAt = DateTime.UtcNow;
            // Domain event will be handled by infrastructure
        }
    }
    
    public void AddNotes(string notes)
    {
        if (string.IsNullOrWhiteSpace(notes))
            throw new ArgumentException("Notes cannot be null or empty", nameof(notes));
            
        Notes = notes;
    }
    
    public void Encrypt()
    {
        if (IsEncrypted)
            throw new InvalidOperationException("Evaluation is already encrypted");
            
        IsEncrypted = true;
    }
    
    public void SignDigitally(string signature)
    {
        if (string.IsNullOrWhiteSpace(signature))
            throw new ArgumentException("Signature cannot be null or empty", nameof(signature));
            
        if (Status != EvaluationStatus.Completed)
            throw new InvalidOperationException("Can only sign completed evaluations");
            
        DigitalSignature = signature;
    }
    
}
