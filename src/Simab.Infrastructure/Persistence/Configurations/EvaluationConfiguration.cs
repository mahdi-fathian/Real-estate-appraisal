using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for Evaluation entity
/// </summary>
public class EvaluationConfiguration : IEntityTypeConfiguration<Evaluation>
{
    public void Configure(EntityTypeBuilder<Evaluation> builder)
    {
        builder.ToTable("Evaluations");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.PropertyId)
            .IsRequired();
            
        builder.Property(e => e.EvaluatorId)
            .IsRequired();
            
        builder.Property(e => e.Status)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(e => e.Type)
            .IsRequired()
            .HasConversion<string>();
            
        builder.OwnsOne(e => e.EstimatedValue, mv =>
        {
            mv.Property(m => m.Amount)
                .HasColumnName("EstimatedAmount")
                .IsRequired()
                .HasPrecision(18, 2);
                
            mv.Property(m => m.Currency)
                .HasColumnName("Currency")
                .IsRequired()
                .HasMaxLength(3);
        });
        
        builder.Property(e => e.CreatedAt)
            .IsRequired();
            
        builder.Property(e => e.Notes)
            .HasMaxLength(2000);
            
        builder.Property(e => e.DigitalSignature)
            .HasMaxLength(500);
            
        builder.HasMany(e => e.Documents)
            .WithOne()
            .HasForeignKey("EvaluationId")
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.HasMany(e => e.Visits)
            .WithOne()
            .HasForeignKey("EvaluationId")
            .OnDelete(DeleteBehavior.Cascade);
            
        builder.Ignore(e => e.DomainEvents);
    }
}
