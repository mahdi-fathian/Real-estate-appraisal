using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for Visit entity
/// </summary>
public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
        builder.ToTable("Visits");
        
        builder.HasKey(v => v.Id);
        
        builder.Property(v => v.EvaluationId)
            .IsRequired();
            
        builder.Property(v => v.EvaluatorId)
            .IsRequired();
            
        builder.Property(v => v.ScheduledDate)
            .IsRequired();
            
        builder.Property(v => v.Status)
            .IsRequired()
            .HasConversion<string>();
            
        builder.OwnsOne(v => v.ActualLocation, loc =>
        {
            loc.Property(l => l.Latitude)
                .HasColumnName("ActualLatitude");
                
            loc.Property(l => l.Longitude)
                .HasColumnName("ActualLongitude");
                
            loc.Property(l => l.Address)
                .HasColumnName("ActualAddress")
                .HasMaxLength(500);
        });
        
        builder.Property(v => v.CustomerFeedback)
            .HasMaxLength(1000);
            
        builder.Property(v => v.Notes)
            .HasMaxLength(2000);
            
        builder.Property(v => v.CreatedAt)
            .IsRequired();
            
        builder.HasMany(v => v.Media)
            .WithOne()
            .HasForeignKey("VisitId")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
