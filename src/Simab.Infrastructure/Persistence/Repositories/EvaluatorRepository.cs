using Microsoft.EntityFrameworkCore;
using Simab.Application.Common.Interfaces;
using Simab.Domain.Entities;
using Simab.Infrastructure.Persistence;

namespace Simab.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Evaluator entity
/// </summary>
public class EvaluatorRepository : IEvaluatorRepository
{
    private readonly SimabDbContext _context;
    
    public EvaluatorRepository(SimabDbContext context)
    {
        _context = context;
    }
    
    public async Task<Evaluator?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluators
            .FirstOrDefaultAsync(e => e.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Evaluator>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Evaluators
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Evaluator> AddAsync(Evaluator entity, CancellationToken cancellationToken = default)
    {
        await _context.Evaluators.AddAsync(entity, cancellationToken);
        return entity;
    }
    
    public Task UpdateAsync(Evaluator entity, CancellationToken cancellationToken = default)
    {
        _context.Evaluators.Update(entity);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(Evaluator entity, CancellationToken cancellationToken = default)
    {
        _context.Evaluators.Remove(entity);
        return Task.CompletedTask;
    }
    
    public async Task<Evaluator?> GetByNationalIdAsync(string nationalId, CancellationToken cancellationToken = default)
    {
        return await _context.Evaluators
            .FirstOrDefaultAsync(e => e.NationalId == nationalId, cancellationToken);
    }
    
    public async Task<IEnumerable<Evaluator>> GetActiveEvaluatorsAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Evaluators
            .Where(e => e.IsActive)
            .ToListAsync(cancellationToken);
    }
}
