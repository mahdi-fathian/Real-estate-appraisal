using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for Property entity
/// </summary>
public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.ToTable("Properties");
        
        builder.HasKey(p => p.Id);
        
        builder.Property(p => p.PropertyNumber)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.HasIndex(p => p.PropertyNumber)
            .IsUnique();
            
        builder.Property(p => p.Type)
            .IsRequired()
            .HasConversion<string>();
            
        builder.OwnsOne(p => p.Location, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("Latitude")
                .IsRequired();
                
            loc.Property(l => l.Longitude)
                .HasColumnName("Longitude")
                .IsRequired();
                
            loc.Property(l => l.Address)
                .HasColumnName("Address")
                .HasMaxLength(500);
        });
        
        builder.Property(p => p.Area)
            .IsRequired()
            .HasPrecision(18, 2);
            
        builder.Property(p => p.Description)
            .HasMaxLength(2000);
            
        builder.Property(p => p.MunicipalityInfo)
            .HasMaxLength(1000);
            
        builder.Property(p => p.CreatedAt)
            .IsRequired();
    }
}
