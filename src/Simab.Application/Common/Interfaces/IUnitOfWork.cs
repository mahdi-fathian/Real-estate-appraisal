namespace Simab.Application.Common.Interfaces;

/// <summary>
/// Unit of Work for transactional operations
/// </summary>
public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
