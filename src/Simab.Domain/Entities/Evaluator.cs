using Simab.Domain.Common;
using Simab.Domain.ValueObjects;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents an evaluator (appraiser) in the system
/// </summary>
public class Evaluator : Entity
{
    public string NationalId { get; private set; } = null!;
    public string FirstName { get; private set; } = null!;
    public string LastName { get; private set; } = null!;
    public string? Email { get; private set; }
    public string? PhoneNumber { get; private set; }
    public Location? OfficeLocation { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime CreatedAt { get; private set; }
    
    private Evaluator() { }
    
    public Evaluator(
        string nationalId,
        string firstName,
        string lastName)
    {
        if (string.IsNullOrWhiteSpace(nationalId))
            throw new ArgumentException("National ID cannot be null or empty", nameof(nationalId));
            
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First name cannot be null or empty", nameof(firstName));
            
        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last name cannot be null or empty", nameof(lastName));
            
        NationalId = nationalId;
        FirstName = firstName;
        LastName = lastName;
        IsActive = true;
        CreatedAt = DateTime.UtcNow;
    }
    
    public string FullName => $"{FirstName} {LastName}";
    
    public void UpdateContactInfo(string? email, string? phoneNumber)
    {
        Email = email;
        PhoneNumber = phoneNumber;
    }
    
    public void UpdateOfficeLocation(Location? location)
    {
        OfficeLocation = location;
    }
    
    public void Activate()
    {
        IsActive = true;
    }
    
    public void Deactivate()
    {
        IsActive = false;
    }
    
    public double? CalculateDistanceToProperty(Location propertyLocation)
    {
        if (OfficeLocation == null)
            return null;
            
        return OfficeLocation.CalculateDistance(propertyLocation);
    }
}
