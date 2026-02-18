using Microsoft.EntityFrameworkCore;
using Simab.Application.Common.Interfaces;
using Simab.Domain.Entities;
using Simab.Domain.Enums;
using Simab.Infrastructure.Persistence;

namespace Simab.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Evaluation entity
/// </summary>
public class EvaluationRepository : IEvaluationRepository
{
    private readonly SimabDbContext _context;
    
    public EvaluationRepository(SimabDbContext context)
    {
        _context = context;
    }
    
    public async Task<Evaluation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.Documents)
            .Include(e => e.Visits)
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Evaluation>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.Documents)
            .Include(e => e.Visits)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Evaluation> AddAsync(Evaluation entity, CancellationToken cancellationToken = default)
    {
        await _context.Evaluations.AddAsync(entity, cancellationToken);
        return entity;
    }
    
    public Task UpdateAsync(Evaluation entity, CancellationToken cancellationToken = default)
    {
        _context.Evaluations.Update(entity);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(Evaluation entity, CancellationToken cancellationToken = default)
    {
        _context.Evaluations.Remove(entity);
        return Task.CompletedTask;
    }
    
    public async Task<IEnumerable<Evaluation>> GetByEvaluatorIdAsync(Guid evaluatorId, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.Documents)
            .Include(e => e.Visits)
            .Where(e => e.EvaluatorId == evaluatorId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Evaluation>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.Documents)
            .Include(e => e.Visits)
            .Where(e => e.PropertyId == propertyId)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Evaluation>> GetByStatusAsync(EvaluationStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.Documents)
            .Include(e => e.Visits)
            .Where(e => e.Status == status)
            .ToListAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<Evaluation>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluations
            .Include(e => e.Documents)
            .Include(e => e.Visits)
            .Where(e => e.CustomerId == customerId)
            .ToListAsync(cancellationToken);
    }
}
