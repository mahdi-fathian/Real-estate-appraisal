using Simab.Domain.Common;
using Simab.Domain.Enums;
using Simab.Domain.ValueObjects;

namespace Simab.Domain.Entities;

/// <summary>
/// Represents a property (real estate) in the system
/// </summary>
public class Property : Entity
{
    public string PropertyNumber { get; private set; } = null!;
    public PropertyType Type { get; private set; }
    public Location Location { get; private set; } = null!;
    public decimal Area { get; private set; }
    public int? Floor { get; private set; }
    public int? TotalFloors { get; private set; }
    public int? YearBuilt { get; private set; }
    public string? Description { get; private set; }
    public string? MunicipalityInfo { get; private set; }
    public bool IsInDeprecatedArea { get; private set; }
    public bool IsInInefficientUrbanArea { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    
    private Property() { }
    
    public Property(
        string propertyNumber,
        PropertyType type,
        Location location,
        decimal area)
    {
        if (string.IsNullOrWhiteSpace(propertyNumber))
            throw new ArgumentException("Property number cannot be null or empty", nameof(propertyNumber));
            
        if (location == null)
            throw new ArgumentNullException(nameof(location));
            
        if (area <= 0)
            throw new ArgumentException("Area must be greater than zero", nameof(area));
            
        PropertyNumber = propertyNumber;
        Type = type;
        Location = location;
        Area = area;
        CreatedAt = DateTime.UtcNow;
    }
    
    public void UpdateLocation(Location newLocation)
    {
        if (newLocation == null)
            throw new ArgumentNullException(nameof(newLocation));
            
        Location = newLocation;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateArea(decimal newArea)
    {
        if (newArea <= 0)
            throw new ArgumentException("Area must be greater than zero", nameof(newArea));
            
        Area = newArea;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateDescription(string? description)
    {
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void SetMunicipalityInfo(string municipalityInfo)
    {
        MunicipalityInfo = municipalityInfo;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void MarkAsDeprecatedArea(bool isDeprecated)
    {
        IsInDeprecatedArea = isDeprecated;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void MarkAsInefficientUrbanArea(bool isInefficient)
    {
        IsInInefficientUrbanArea = isInefficient;
        UpdatedAt = DateTime.UtcNow;
    }
    
    public void UpdateBuildingInfo(int? floor, int? totalFloors, int? yearBuilt)
    {
        Floor = floor;
        TotalFloors = totalFloors;
        YearBuilt = yearBuilt;
        UpdatedAt = DateTime.UtcNow;
    }
}
