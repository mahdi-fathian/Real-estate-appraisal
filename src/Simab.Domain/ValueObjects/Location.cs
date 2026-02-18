using Simab.Domain.Common;

namespace Simab.Domain.ValueObjects;

/// <summary>
/// Value object representing geographical location
/// </summary>
public class Location : ValueObject
{
    public double Latitude { get; init; }
    public double Longitude { get; init; }
    public string? Address { get; init; }
    
    public Location(double latitude, double longitude, string? address = null)
    {
        if (latitude < -90 || latitude > 90)
            throw new ArgumentException("Latitude must be between -90 and 90", nameof(latitude));
            
        if (longitude < -180 || longitude > 180)
            throw new ArgumentException("Longitude must be between -180 and 180", nameof(longitude));
            
        Latitude = latitude;
        Longitude = longitude;
        Address = address;
    }
    
    protected override IEnumerable<object> GetAtomicValues()
    {
        yield return Latitude;
        yield return Longitude;
        yield return Address ?? string.Empty;
    }
    
    /// <summary>
    /// Calculates distance in kilometers using Haversine formula
    /// </summary>
    public double CalculateDistance(Location other)
    {
        const double earthRadiusKm = 6371.0;
        
        var dLat = ToRadians(other.Latitude - Latitude);
        var dLon = ToRadians(other.Longitude - Longitude);
        
        var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(ToRadians(Latitude)) * Math.Cos(ToRadians(other.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
                
        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
        
        return earthRadiusKm * c;
    }
    
    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }
}
