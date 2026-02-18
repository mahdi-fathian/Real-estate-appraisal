using Simab.Domain.Entities;
using Simab.Domain.Enums;

namespace Simab.Application.Common.Interfaces;

/// <summary>
/// Repository interface for Evaluation entity
/// </summary>
public interface IEvaluationRepository : IRepository<Evaluation>
{
    Task<IEnumerable<Evaluation>> GetByEvaluatorIdAsync(Guid evaluatorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Evaluation>> GetByPropertyIdAsync(Guid propertyId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Evaluation>> GetByStatusAsync(EvaluationStatus status, CancellationToken cancellationToken = default);
    Task<IEnumerable<Evaluation>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken = default);
}
