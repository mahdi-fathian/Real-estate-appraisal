using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for Workflow entity
/// </summary>
public class WorkflowConfiguration : IEntityTypeConfiguration<Workflow>
{
    public void Configure(EntityTypeBuilder<Workflow> builder)
    {
        builder.ToTable("Workflows");
        
        builder.HasKey(w => w.Id);
        
        builder.Property(w => w.EvaluationId)
            .IsRequired();
            
        builder.HasIndex(w => w.EvaluationId)
            .IsUnique();
            
        builder.Property(w => w.Status)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(w => w.Priority)
            .IsRequired();
            
        builder.Property(w => w.Notes)
            .HasMaxLength(2000);
            
        builder.Property(w => w.AllowedLocation)
            .HasMaxLength(500);
            
        builder.Property(w => w.CreatedAt)
            .IsRequired();
    }
}
