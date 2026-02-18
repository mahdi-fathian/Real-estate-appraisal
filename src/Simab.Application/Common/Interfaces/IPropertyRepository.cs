using Simab.Domain.Entities;

namespace Simab.Application.Common.Interfaces;

/// <summary>
/// Repository interface for Property entity
/// </summary>
public interface IPropertyRepository : IRepository<Property>
{
    Task<Property?> GetByPropertyNumberAsync(string propertyNumber, CancellationToken cancellationToken = default);
}
