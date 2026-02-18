using Simab.Application.Common.Interfaces;
using Simab.Infrastructure.Persistence;

namespace Simab.Infrastructure.Persistence;

/// <summary>
/// Unit of Work implementation using DbContext
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private readonly SimabDbContext _context;
    
    public UnitOfWork(SimabDbContext context)
    {
        _context = context;
    }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
