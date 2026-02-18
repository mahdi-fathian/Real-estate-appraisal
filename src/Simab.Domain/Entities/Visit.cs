using Simab.Domain.Common;
using Simab.Domain.Enums;
using Simab.Domain.ValueObjects;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents a property visit by an evaluator
/// </summary>
public class Visit : Entity
{
    public Guid EvaluationId { get; private set; }
    public Guid EvaluatorId { get; private set; }
    public DateTime ScheduledDate { get; private set; }
    public DateTime? ActualDate { get; private set; }
    public VisitStatus Status { get; private set; }
    public Location? ActualLocation { get; private set; }
    public bool IsLocationVerified { get; private set; }
    public string? CustomerFeedback { get; private set; }
    public int? CustomerRating { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private readonly List<VisitMedia> _media = new();
    public IReadOnlyCollection<VisitMedia> Media => _media.AsReadOnly();
    
    private Visit() { }
    
    public Visit(
        Guid evaluationId,
        Guid evaluatorId,
        DateTime scheduledDate)
    {
        EvaluationId = evaluationId;
        EvaluatorId = evaluatorId;
        ScheduledDate = scheduledDate;
        Status = VisitStatus.Scheduled;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void Schedule(DateTime newDate)
    {
        if (newDate < DateTime.UtcNow)
            throw new ArgumentException("Scheduled date cannot be in the past", nameof(newDate));
            
        ScheduledDate = newDate;
        Status = VisitStatus.Scheduled;
    }
    
    public void Complete(Location actualLocation, string? notes = null)
    {
        if (actualLocation == null)
            throw new ArgumentNullException(nameof(actualLocation));
            
        ActualDate = DateTime.UtcNow;
        ActualLocation = actualLocation;
        Status = VisitStatus.Completed;
        Notes = notes;
    }
    
    public void Cancel(string? reason = null)
    {
        Status = VisitStatus.Cancelled;
        Notes = reason;
    }
    
    public void VerifyLocation(Location propertyLocation)
    {
        if (ActualLocation == null)
            throw new InvalidOperationException("Cannot verify location before visit completion");
            
        if (propertyLocation == null)
            throw new ArgumentNullException(nameof(propertyLocation));
            
        // Verify if actual location is within acceptable distance (e.g., 100 meters)
        const double acceptableDistanceKm = 0.1;
        var distance = ActualLocation.CalculateDistance(propertyLocation);
        
        IsLocationVerified = distance <= acceptableDistanceKm;
    }
    
    public void AddCustomerFeedback(string feedback, int rating)
    {
        if (string.IsNullOrWhiteSpace(feedback))
            throw new ArgumentException("Feedback cannot be null or empty", nameof(feedback));
            
        if (rating < 1 || rating > 5)
            throw new ArgumentException("Rating must be between 1 and 5", nameof(rating));
            
        CustomerFeedback = feedback;
        CustomerRating = rating;
    }
    
    public void AddMedia(VisitMedia media)
    {
        if (media == null)
            throw new ArgumentNullException(nameof(media));
            
        _media.Add(media);
    }
}
