using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for Evaluator entity
/// </summary>
public class EvaluatorConfiguration : IEntityTypeConfiguration<Evaluator>
{
    public void Configure(EntityTypeBuilder<Evaluator> builder)
    {
        builder.ToTable("Evaluators");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.NationalId)
            .IsRequired()
            .HasMaxLength(20);
            
        builder.HasIndex(e => e.NationalId)
            .IsUnique();
            
        builder.Property(e => e.FirstName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(e => e.LastName)
            .IsRequired()
            .HasMaxLength(100);
            
        builder.Property(e => e.Email)
            .HasMaxLength(255);
            
        builder.Property(e => e.PhoneNumber)
            .HasMaxLength(20);
            
        builder.OwnsOne(e => e.OfficeLocation, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("OfficeLatitude");
                
            loc.Property(l => l.Longitude)
                .HasColumnName("OfficeLongitude");
                
            loc.Property(l => l.Address)
                .HasColumnName("OfficeAddress")
                .HasMaxLength(500);
        });
        
        builder.Property(e => e.IsActive)
            .IsRequired();
            
        builder.Property(e => e.CreatedAt)
            .IsRequired();
    }
}
