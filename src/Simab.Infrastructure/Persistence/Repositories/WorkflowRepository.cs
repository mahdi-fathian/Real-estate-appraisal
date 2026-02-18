using Microsoft.EntityFrameworkCore;
using Simab.Application.Common.Interfaces;
using Simab.Domain.Entities;
using Simab.Domain.Enums;
using Simab.Infrastructure.Persistence;

namespace Simab.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Workflow entity
/// </summary>
public class WorkflowRepository : IWorkflowRepository
{
    private readonly SimabDbContext _context;
    
    public WorkflowRepository(SimabDbContext context)
    {
        _context = context;
    }
    
    public async Task<Workflow?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Workflows
            .FirstOrDefaultAsync(w => w.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Workflow>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Workflows
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Workflow> AddAsync(Workflow entity, CancellationToken cancellationToken = default)
    {
        await _context.Workflows.AddAsync(entity, cancellationToken);
        return entity;
    }
    
    public Task UpdateAsync(Workflow entity, CancellationToken cancellationToken = default)
    {
        _context.Workflows.Update(entity);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(Workflow entity, CancellationToken cancellationToken = default)
    {
        _context.Workflows.Remove(entity);
        return Task.CompletedTask;
    }
    
    public async Task<IEnumerable<Workflow>> GetByEvaluatorIdAsync(Guid evaluatorId, CancellationToken cancellationToken = default)
    {
        return await _context.Workflows
            .Where(w => w.AssignedToId == evaluatorId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Workflow>> GetByStatusAsync(WorkflowStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Workflows
            .Where(w => w.Status == status)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Workflow?> GetByEvaluationIdAsync(Guid evaluationId, CancellationToken cancellationToken = default)
    {
        return await _context.Workflows
            .FirstOrDefaultAsync(w => w.EvaluationId == evaluationId, cancellationToken);
    }
}
