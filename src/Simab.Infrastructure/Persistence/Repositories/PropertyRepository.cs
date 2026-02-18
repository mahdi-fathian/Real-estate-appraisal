using Microsoft.EntityFrameworkCore;
using Simab.Application.Common.Interfaces;
using Simab.Domain.Entities;
using Simab.Infrastructure.Persistence;

namespace Simab.Infrastructure.Persistence.Repositories;

/// <summary>
/// Repository implementation for Property entity
/// </summary>
public class PropertyRepository : IPropertyRepository
{
    private readonly SimabDbContext _context;
    
    public PropertyRepository(SimabDbContext context)
    {
        _context = context;
    }
    
    public async Task<Property?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Properties
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    
    public async Task<IEnumerable<Property>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Properties
            .ToListAsync(cancellationToken);
    }
    
    public async Task<Property> AddAsync(Property entity, CancellationToken cancellationToken = default)
    {
        await _context.Properties.AddAsync(entity, cancellationToken);
        return entity;
    }
    
    public Task UpdateAsync(Property entity, CancellationToken cancellationToken = default)
    {
        _context.Properties.Update(entity);
        return Task.CompletedTask;
    }
    
    public Task DeleteAsync(Property entity, CancellationToken cancellationToken = default)
    {
        _context.Properties.Remove(entity);
        return Task.CompletedTask;
    }
    
    public async Task<Property?> GetByPropertyNumberAsync(string propertyNumber, CancellationToken cancellationToken = default)
    {
        return await _context.Properties
            .FirstOrDefaultAsync(p => p.PropertyNumber == propertyNumber, cancellationToken);
    }
}
