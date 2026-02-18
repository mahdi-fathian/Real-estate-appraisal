using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for Document entity
/// </summary>
public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable("Documents");
        
        builder.HasKey(d => d.Id);
        
        builder.Property(d => d.EvaluationId)
            .IsRequired();
            
        builder.Property(d => d.Type)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(d => d.FileName)
            .IsRequired()
            .HasMaxLength(255);
            
        builder.Property(d => d.FilePath)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(d => d.FileSize)
            .IsRequired();
            
        builder.Property(d => d.MimeType)
            .HasMaxLength(100);
            
        builder.Property(d => d.Description)
            .HasMaxLength(500);
            
        builder.Property(d => d.CreatedAt)
            .IsRequired();
    }
}
