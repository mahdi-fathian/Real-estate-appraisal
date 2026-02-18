using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simab.Domain.Entities;

namespace Simab.Infrastructure.Persistence.Configurations;

/// <summary>
/// EF Core configuration for VisitMedia entity
/// </summary>
public class VisitMediaConfiguration : IEntityTypeConfiguration<VisitMedia>
{
    public void Configure(EntityTypeBuilder<VisitMedia> builder)
    {
        builder.ToTable("VisitMedia");
        
        builder.HasKey(vm => vm.Id);
        
        builder.Property(vm => vm.VisitId)
            .IsRequired();
            
        builder.Property(vm => vm.Type)
            .IsRequired()
            .HasConversion<string>();
            
        builder.Property(vm => vm.FilePath)
            .IsRequired()
            .HasMaxLength(1000);
            
        builder.Property(vm => vm.FileName)
            .IsRequired()
            .HasMaxLength(255);
            
        builder.Property(vm => vm.FileSize)
            .IsRequired();
            
        builder.Property(vm => vm.MimeType)
            .HasMaxLength(100);
            
        builder.Property(vm => vm.CreatedAt)
            .IsRequired();
    }
}
