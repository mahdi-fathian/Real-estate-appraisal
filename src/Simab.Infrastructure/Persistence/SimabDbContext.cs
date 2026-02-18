using Microsoft.EntityFrameworkCore;
using Simab.Domain.Entities;
using Simab.Infrastructure.Persistence.Configurations;

namespace Simab.Infrastructure.Persistence;

/// <summary>
/// EF Core DbContext for Simab system
/// </summary>
public class SimabDbContext : DbContext
{
    public SimabDbContext(DbContextOptions<SimabDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Evaluation> Evaluations => Set<Evaluation>();
    public DbSet<Property> Properties => Set<Property>();
    public DbSet<Evaluator> Evaluators => Set<Evaluator>();
    public DbSet<Visit> Visits => Set<Visit>();
    public DbSet<VisitMedia> VisitMedia => Set<VisitMedia>();
    public DbSet<Document> Documents => Set<Document>();
    public DbSet<Workflow> Workflows => Set<Workflow>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfiguration(new EvaluationConfiguration());
        modelBuilder.ApplyConfiguration(new PropertyConfiguration());
        modelBuilder.ApplyConfiguration(new EvaluatorConfiguration());
        modelBuilder.ApplyConfiguration(new VisitConfiguration());
        modelBuilder.ApplyConfiguration(new VisitMediaConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new WorkflowConfiguration());
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch domain events before saving
        var domainEvents = ChangeTracker
            .Entries<Domain.Common.Entity>()
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();
            
        var result = await base.SaveChangesAsync(cancellationToken);
        
        // Note: In a real implementation, you would dispatch these events here
        // For now, we'll just clear them
        
        foreach (var entry in ChangeTracker.Entries<Domain.Common.Entity>())
        {
            entry.Entity.ClearDomainEvents();
        }
        
        return result;
    }
}
